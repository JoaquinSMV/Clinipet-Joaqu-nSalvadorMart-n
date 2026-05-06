using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    /// <summary>
    /// Clase para gestionar servicios, tratamientos y tarifas de la clínica
    /// </summary>
    public class GestorServicios
    {
        private ConexionBD conexion;

        public GestorServicios()
        {
            conexion = new ConexionBD();
        }

        /// <summary>
        /// Obtiene todos los servicios disponibles
        /// </summary>
        public DataTable ObtenerServicios()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    id_servicio, nombre_servicio, descripcion, 
                    precio, categoria, activo
                    FROM servicios
                    WHERE activo = 1
                    ORDER BY categoria, nombre_servicio";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener servicios: " + ex.Message);
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene servicios por categoría
        /// </summary>
        public DataTable ObtenerServiciosPorCategoria(string categoria)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    id_servicio, nombre_servicio, descripcion, 
                    precio, categoria
                    FROM servicios
                    WHERE categoria = @categoria AND activo = 1
                    ORDER BY nombre_servicio";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@categoria", categoria);

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
        /// Agrega un nuevo servicio
        /// </summary>
        public bool AgregarServicio(string nombre, string descripcion, decimal precio, string categoria)
        {
            try
            {
                conexion.Abrir();

                string query = @"INSERT INTO servicios 
                    (nombre_servicio, descripcion, precio, categoria, activo, fecha_creacion)
                    VALUES (@nombre, @descripcion, @precio, @categoria, 1, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion ?? "");
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@categoria", categoria);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar servicio: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Actualiza un servicio existente
        /// </summary>
        public bool ActualizarServicio(int idServicio, string nombre, string descripcion, decimal precio, string categoria, bool activo = true)
        {
            try
            {
                conexion.Abrir();

                string query = @"UPDATE servicios 
                    SET nombre_servicio = @nombre, 
                        descripcion = @descripcion,
                        precio = @precio,
                        categoria = @categoria,
                        activo = @activo,
                        fecha_actualizacion = GETDATE()
                    WHERE id_servicio = @idServicio";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idServicio", idServicio);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion ?? "");
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@categoria", categoria);
                cmd.Parameters.AddWithValue("@activo", activo ? 1 : 0);

                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene las categorías de servicios disponibles
        /// </summary>
        public DataTable ObtenerCategorias()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT DISTINCT categoria FROM servicios WHERE activo = 1 ORDER BY categoria";

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
        /// Obtiene el precio promedio de servicios por categoría
        /// </summary>
        public DataTable ObtenerEstadisticasServicios()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    categoria,
                    COUNT(*) as cantidad_servicios,
                    AVG(precio) as precio_promedio,
                    MIN(precio) as precio_minimo,
                    MAX(precio) as precio_maximo
                    FROM servicios
                    WHERE activo = 1
                    GROUP BY categoria
                    ORDER BY categoria";

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
        /// Obtiene servicios más solicitados
        /// </summary>
        public DataTable ObtenerServiciosMasSolicitados(int limite = 10)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT TOP (@limite)
                    s.id_servicio, s.nombre_servicio, s.precio,
                    COUNT(cit.id_cita) as veces_utilizado,
                    SUM(s.precio) as ingresos_totales
                    FROM servicios s
                    LEFT JOIN citas cit ON s.id_servicio = cit.id_servicio
                    WHERE s.activo = 1
                    GROUP BY s.id_servicio, s.nombre_servicio, s.precio
                    ORDER BY veces_utilizado DESC";

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
    }
}
