using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public class GestorEstadisticas
    {
        private ConexionBD conexion;

        public GestorEstadisticas()
        {
            conexion = new ConexionBD();
        }

        public DataTable ObtenerEstadisticasGenerales()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    (SELECT COUNT(*) FROM Clientes) as total_clientes,
                    (SELECT COUNT(*) FROM Mascotas) as total_mascotas,
                    (SELECT COUNT(*) FROM Citas) as total_citas,
                    (SELECT COUNT(*) FROM Citas WHERE Observaciones LIKE '%Completada%') as citas_completadas,
                    (SELECT COUNT(*) FROM Citas WHERE Observaciones NOT LIKE '%Completada%' OR Observaciones IS NULL) as citas_pendientes,
                    (SELECT COUNT(*) FROM Citas WHERE CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE)) as citas_hoy,
                    ISNULL((SELECT SUM(s.precio) FROM citas c JOIN servicios s ON c.id_servicio = s.id_servicio), 0) as total_recaudado";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en estadísticas: " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public DataTable ObtenerCitasPorMes()
        {
            try
            {
                conexion.Abrir();
                string query = @"SELECT 
                    YEAR(FechaHora) as año,
                    MONTH(FechaHora) as mes,
                    DATENAME(MONTH, FechaHora) as nombre_mes,
                    COUNT(*) as total_citas
                    FROM Citas
                    WHERE FechaHora >= DATEADD(MONTH, -12, GETDATE())
                    GROUP BY YEAR(FechaHora), MONTH(FechaHora), DATENAME(MONTH, FechaHora)
                    ORDER BY año DESC, mes DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch { return null; }
            finally { conexion.Cerrar(); }
        }

        public DataTable ObtenerMascotasPorEspecie()
        {
            try
            {
                conexion.Abrir();
                string query = @"SELECT Especie, COUNT(*) as cantidad FROM Mascotas GROUP BY Especie ORDER BY cantidad DESC";
                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch { return null; }
            finally { conexion.Cerrar(); }
        }

        public DataTable ObtenerClientesMasActivos(int limite = 10)
        {
            try
            {
                conexion.Abrir();
                string query = $@"SELECT TOP ({limite}) c.Nombre, c.Apellidos, COUNT(cit.CitaID) as total_citas
                    FROM Clientes c
                    LEFT JOIN Citas cit ON c.ClienteID = cit.ClienteID
                    GROUP BY c.ClienteID, c.Nombre, c.Apellidos
                    ORDER BY total_citas DESC";
                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch { return null; }
            finally { conexion.Cerrar(); }
        }
    }
}
