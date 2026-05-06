using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    /// <summary>
    /// Clase para gestionar el historial médico completo de las mascotas
    /// Incluye diagnósticos, tratamientos y medicamentos
    /// </summary>
    public class HistorialMedico
    {
        private ConexionBD conexion;

        public HistorialMedico()
        {
            conexion = new ConexionBD();
        }

        /// <summary>
        /// Registra un nuevo diagnóstico y tratamiento para una cita
        /// </summary>
        public bool RegistrarDiagnostico(int idCita, string diagnostico, string tratamiento, string medicamentos, string observaciones)
        {
            try
            {
                conexion.Abrir();

                string query = @"UPDATE citas 
                    SET diagnostico = @diagnostico, 
                        tratamiento = @tratamiento,
                        medicamentos = @medicamentos,
                        observaciones = @observaciones,
                        fecha_actualizacion = GETDATE()
                    WHERE id_cita = @idCita";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idCita", idCita);
                cmd.Parameters.AddWithValue("@diagnostico", diagnostico ?? "");
                cmd.Parameters.AddWithValue("@tratamiento", tratamiento ?? "");
                cmd.Parameters.AddWithValue("@medicamentos", medicamentos ?? "");
                cmd.Parameters.AddWithValue("@observaciones", observaciones ?? "");

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar diagnóstico: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene el historial completo de una mascota
        /// </summary>
        public DataTable ObtenerHistorialMascota(int idMascota)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    cit.id_cita, cit.fecha_cita, cit.hora_cita, 
                    cit.motivo, cit.diagnostico, cit.tratamiento, 
                    cit.medicamentos, cit.observaciones,
                    cit.estado, cit.fecha_actualizacion
                    FROM citas
                    WHERE id_mascota = @idMascota
                    ORDER BY cit.fecha_cita DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idMascota", idMascota);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener historial: " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene las últimas citas de una mascota (últimos 30 días)
        /// </summary>
        public DataTable ObtenerUltimasCitas(int idMascota, int dias = 30)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    cit.id_cita, cit.fecha_cita, cit.motivo, 
                    cit.diagnostico, cit.tratamiento
                    FROM citas
                    WHERE id_mascota = @idMascota 
                    AND cit.fecha_cita >= DATEADD(DAY, -@dias, GETDATE())
                    ORDER BY cit.fecha_cita DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idMascota", idMascota);
                cmd.Parameters.AddWithValue("@dias", dias);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene estadísticas de salud de una mascota
        /// </summary>
        public DataTable ObtenerEstadisticasMascota(int idMascota)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    COUNT(*) as total_citas,
                    SUM(CASE WHEN estado = 'Completada' THEN 1 ELSE 0 END) as citas_completadas,
                    SUM(CASE WHEN estado = 'Pendiente' THEN 1 ELSE 0 END) as citas_pendientes,
                    MAX(fecha_cita) as ultima_cita
                    FROM citas
                    WHERE id_mascota = @idMascota";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idMascota", idMascota);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Busca mascotas por síntoma o diagnóstico
        /// </summary>
        public DataTable BuscarPorDiagnostico(string diagnostico)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT DISTINCT
                    m.id_mascota, m.nombre, m.especie, m.raza,
                    c.nombre_cliente, c.telefono,
                    COUNT(cit.id_cita) as total_citas_con_diagnostico
                    FROM mascotas m
                    JOIN clientes c ON m.id_cliente = c.id_cliente
                    LEFT JOIN citas cit ON m.id_mascota = cit.id_mascota 
                        AND cit.diagnostico LIKE @diagnostico
                    WHERE cit.diagnostico LIKE @diagnostico
                    GROUP BY m.id_mascota, m.nombre, m.especie, m.raza, 
                             c.nombre_cliente, c.telefono
                    ORDER BY total_citas_con_diagnostico DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@diagnostico", "%" + diagnostico + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
