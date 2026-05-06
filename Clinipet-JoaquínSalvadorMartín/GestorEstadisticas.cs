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

                string query = @"
                    SELECT 
                        FORMAT(FechaHora, 'MMMM', 'es-ES') as nombre_mes,
                        COUNT(*) as total_citas,
                        MONTH(FechaHora) as mes_num
                    FROM Citas
                    WHERE FechaHora >= DATEADD(MONTH, -6, GETDATE())
                    GROUP BY FORMAT(FechaHora, 'MMMM', 'es-ES'), MONTH(FechaHora)
                    ORDER BY mes_num";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener citas por mes: " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
