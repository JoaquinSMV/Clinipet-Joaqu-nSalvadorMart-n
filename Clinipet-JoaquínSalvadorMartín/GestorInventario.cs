using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    /// <summary>
    /// Clase para gestionar el inventario de medicamentos y suministros
    /// </summary>
    public class GestorInventario
    {
        private ConexionBD conexion;

        public GestorInventario()
        {
            conexion = new ConexionBD();
        }

        /// <summary>
        /// Obtiene todos los medicamentos en inventario
        /// </summary>
        public DataTable ObtenerMedicamentos()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    id_medicamento, nombre_medicamento, descripcion, 
                    cantidad_stock, cantidad_minima, precio_unitario,
                    fecha_vencimiento, proveedor, activo
                    FROM medicamentos
                    WHERE activo = 1
                    ORDER BY nombre_medicamento";

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
        /// Obtiene medicamentos con stock bajo
        /// </summary>
        public DataTable ObtenerMedicamentosStockBajo()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    id_medicamento, nombre_medicamento, cantidad_stock, 
                    cantidad_minima, proveedor
                    FROM medicamentos
                    WHERE activo = 1 AND cantidad_stock <= cantidad_minima
                    ORDER BY cantidad_stock ASC";

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
        /// Obtiene medicamentos próximos a vencer
        /// </summary>
        public DataTable ObtenerMedicamentosProximosAVencer(int dias = 30)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    id_medicamento, nombre_medicamento, cantidad_stock,
                    fecha_vencimiento, proveedor
                    FROM medicamentos
                    WHERE activo = 1 
                    AND fecha_vencimiento BETWEEN GETDATE() AND DATEADD(DAY, @dias, GETDATE())
                    ORDER BY fecha_vencimiento ASC";

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
        /// Agrega un nuevo medicamento al inventario
        /// </summary>
        public bool AgregarMedicamento(string nombre, string descripcion, int cantidad, 
            int cantidadMinima, decimal precioUnitario, DateTime fechaVencimiento, string proveedor)
        {
            try
            {
                conexion.Abrir();

                string query = @"INSERT INTO medicamentos 
                    (nombre_medicamento, descripcion, cantidad_stock, cantidad_minima, 
                     precio_unitario, fecha_vencimiento, proveedor, activo, fecha_creacion)
                    VALUES (@nombre, @descripcion, @cantidad, @cantidadMinima, 
                            @precio, @fechaVencimiento, @proveedor, 1, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion ?? "");
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@cantidadMinima", cantidadMinima);
                cmd.Parameters.AddWithValue("@precio", precioUnitario);
                cmd.Parameters.AddWithValue("@fechaVencimiento", fechaVencimiento);
                cmd.Parameters.AddWithValue("@proveedor", proveedor ?? "");

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
        /// Actualiza la cantidad de un medicamento
        /// </summary>
        public bool ActualizarStock(int idMedicamento, int nuevaCantidad, string motivo = "")
        {
            try
            {
                conexion.Abrir();

                string query = @"UPDATE medicamentos 
                    SET cantidad_stock = @cantidad,
                        fecha_actualizacion = GETDATE()
                    WHERE id_medicamento = @idMedicamento";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                cmd.Parameters.AddWithValue("@cantidad", nuevaCantidad);

                int resultado = cmd.ExecuteNonQuery();

                // Registrar movimiento
                if (resultado > 0 && !string.IsNullOrEmpty(motivo))
                {
                    RegistrarMovimientoInventario(idMedicamento, nuevaCantidad, motivo);
                }

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
        /// Registra un movimiento de inventario
        /// </summary>
        private void RegistrarMovimientoInventario(int idMedicamento, int cantidad, string motivo)
        {
            try
            {
                conexion.Abrir();

                string query = @"INSERT INTO movimientos_inventario 
                    (id_medicamento, cantidad, motivo, fecha_movimiento)
                    VALUES (@idMedicamento, @cantidad, @motivo, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@motivo", motivo);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar movimiento: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Obtiene el historial de movimientos de un medicamento
        /// </summary>
        public DataTable ObtenerHistorialMovimientos(int idMedicamento)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    id_movimiento, cantidad, motivo, fecha_movimiento
                    FROM movimientos_inventario
                    WHERE id_medicamento = @idMedicamento
                    ORDER BY fecha_movimiento DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);

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
        /// Obtiene valor total del inventario
        /// </summary>
        public DataTable ObtenerValorInventario()
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    COUNT(*) as total_medicamentos,
                    SUM(cantidad_stock) as cantidad_total,
                    SUM(cantidad_stock * precio_unitario) as valor_total
                    FROM medicamentos
                    WHERE activo = 1";

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
        /// Obtiene medicamentos más utilizados
        /// </summary>
        public DataTable ObtenerMedicamentosMasUtilizados(int limite = 10)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT TOP (@limite)
                    m.id_medicamento, m.nombre_medicamento,
                    COUNT(cit.id_cita) as veces_utilizado,
                    m.cantidad_stock, m.precio_unitario
                    FROM medicamentos m
                    LEFT JOIN citas cit ON m.nombre_medicamento LIKE '%' + cit.medicamentos + '%'
                    WHERE m.activo = 1
                    GROUP BY m.id_medicamento, m.nombre_medicamento, m.cantidad_stock, m.precio_unitario
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
