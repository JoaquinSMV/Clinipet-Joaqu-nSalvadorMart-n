using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    /// <summary>
    /// Clase para generar estadísticas y reportes de la clínica
    /// </summary>
    public class GestorEstadisticas
    {
        private ConexionBD conexion;

        public GestorEstadisticas()
        {
            conexion = new ConexionBD();
        }

        /// <summary>
        /// Obtiene estadísticas generales de la clínica
        /// </summary>
        public DataTable ObtenerEstadisticasGenerales()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    (SELECT COUNT(*) FROM clientes) as total_clientes,
                    (SELECT COUNT(*) FROM mascotas) as total_mascotas,
                    (SELECT COUNT(*) FROM citas) as total_citas,
                    (SELECT COUNT(*) FROM citas WHERE estado = 'Completada') as citas_completadas,
                    (SELECT COUNT(*) FROM citas WHERE estado = 'Pendiente') as citas_pendientes,
                    (SELECT COUNT(*) FROM citas WHERE CAST(fecha_cita AS DATE) = CAST(GETDATE() AS DATE)) as citas_hoy";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
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
        /// Obtiene citas por mes (últimos 12 meses)
        /// </summary>
        public DataTable ObtenerCitasPorMes()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    YEAR(fecha_cita) as año,
                    MONTH(fecha_cita) as mes,
                    DATENAME(MONTH, fecha_cita) as nombre_mes,
                    COUNT(*) as total_citas,
                    SUM(CASE WHEN estado = 'Completada' THEN 1 ELSE 0 END) as completadas
                    FROM citas
                    WHERE fecha_cita >= DATEADD(MONTH, -12, GETDATE())
                    GROUP BY YEAR(fecha_cita), MONTH(fecha_cita), DATENAME(MONTH, fecha_cita)
                    ORDER BY año DESC, mes DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
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
        /// Obtiene distribución de mascotas por especie
        /// </summary>
        public DataTable ObtenerMascotasPorEspecie()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    especie,
                    COUNT(*) as cantidad,
                    CAST(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM mascotas) AS DECIMAL(5,2)) as porcentaje
                    FROM mascotas
                    GROUP BY especie
                    ORDER BY cantidad DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
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
        /// Obtiene clientes más activos
        /// </summary>
        public DataTable ObtenerClientesMasActivos(int limite = 10)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT TOP (@limite)
                    c.id_cliente, c.nombre_cliente, c.telefono,
                    COUNT(cit.id_cita) as total_citas,
                    COUNT(DISTINCT m.id_mascota) as total_mascotas,
                    MAX(cit.fecha_cita) as ultima_cita
                    FROM clientes c
                    LEFT JOIN mascotas m ON c.id_cliente = m.id_cliente
                    LEFT JOIN citas cit ON m.id_mascota = cit.id_mascota
                    GROUP BY c.id_cliente, c.nombre_cliente, c.telefono
                    ORDER BY total_citas DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@limite", limite);

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
        /// Obtiene citas próximas (próximos 7 días)
        /// </summary>
        public DataTable ObtenerCitasProximas(int dias = 7)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    cit.id_cita, cit.fecha_cita, cit.hora_cita,
                    m.nombre as mascota, m.especie,
                    c.nombre_cliente, c.telefono,
                    cit.motivo, cit.estado
                    FROM citas cit
                    JOIN mascotas m ON cit.id_mascota = m.id_mascota
                    JOIN clientes c ON m.id_cliente = c.id_cliente
                    WHERE cit.fecha_cita BETWEEN GETDATE() AND DATEADD(DAY, @dias, GETDATE())
                    AND cit.estado = 'Pendiente'
                    ORDER BY cit.fecha_cita ASC, cit.hora_cita ASC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
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
        /// Obtiene motivos de consulta más frecuentes
        /// </summary>
        public DataTable ObtenerMotivosConsultaFrecuentes(int limite = 10)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT TOP (@limite)
                    motivo,
                    COUNT(*) as frecuencia,
                    CAST(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM citas) AS DECIMAL(5,2)) as porcentaje
                    FROM citas
                    WHERE motivo IS NOT NULL AND motivo != ''
                    GROUP BY motivo
                    ORDER BY frecuencia DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@limite", limite);

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
        /// Obtiene ingresos por período
        /// </summary>
        public DataTable ObtenerIngresosPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    CAST(cit.fecha_cita AS DATE) as fecha,
                    COUNT(*) as cantidad_citas,
                    SUM(CAST(s.precio AS DECIMAL(10,2))) as ingresos_totales
                    FROM citas cit
                    LEFT JOIN servicios s ON cit.id_servicio = s.id_servicio
                    WHERE cit.fecha_cita BETWEEN @fechaInicio AND @fechaFin
                    AND cit.estado = 'Completada'
                    GROUP BY CAST(cit.fecha_cita AS DATE)
                    ORDER BY fecha DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

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
        /// Obtiene tasa de no-presentismo (citas no asistidas)
        /// </summary>
        public DataTable ObtenerTasaNoAsistencia()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    COUNT(*) as total_citas,
                    SUM(CASE WHEN estado = 'No asistida' THEN 1 ELSE 0 END) as no_asistidas,
                    SUM(CASE WHEN estado = 'Completada' THEN 1 ELSE 0 END) as completadas,
                    CAST(SUM(CASE WHEN estado = 'No asistida' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2)) as porcentaje_no_asistencia
                    FROM citas";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
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
