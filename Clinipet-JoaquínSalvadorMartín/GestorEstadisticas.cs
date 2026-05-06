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

                string query = @"
                    SELECT 
                        (SELECT COUNT(*) FROM Clientes WHERE activo = 1) as total_clientes,
                        (SELECT COUNT(*) FROM Mascotas WHERE activo = 1) as total_mascotas,
                        (SELECT COUNT(*) FROM Citas) as total_citas,
                        (SELECT COUNT(*) FROM Citas WHERE CAST(FechaHora AS DATE) = CAST(GETDATE() AS DATE)) as citas_hoy,
                        ISNULL((SELECT SUM(CAST(precio AS DECIMAL(10,2))) FROM Servicios WHERE activo = 1), 0) as total_recaudado";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en estadísticas generales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        ISNULL(FORMAT(FechaHora, 'MMMM', 'es-ES'), 'Sin datos') as nombre_mes,
                        COUNT(*) as total_citas,
                        ISNULL(MONTH(FechaHora), 0) as mes_num
                    FROM Citas
                    WHERE FechaHora >= DATEADD(MONTH, -6, GETDATE())
                    GROUP BY FORMAT(FechaHora, 'MMMM', 'es-ES'), MONTH(FechaHora)
                    ORDER BY mes_num DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Si no hay datos, añadir una fila de ejemplo
                if (dt.Rows.Count == 0)
                {
                    dt.Columns.Add("nombre_mes", typeof(string));
                    dt.Columns.Add("total_citas", typeof(int));
                    dt.Columns.Add("mes_num", typeof(int));
                    dt.Rows.Add("Sin datos", 0, 0);
                }

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener citas por mes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
