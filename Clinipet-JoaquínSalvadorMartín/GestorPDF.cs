using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    /// <summary>
    /// Clase para generar reportes en PDF usando iTextSharp
    /// Requiere: Install-Package iTextSharp -Version 5.5.13.3
    /// </summary>
    public class GestorPDF
    {
        private ConexionBD conexion;

        public GestorPDF()
        {
            conexion = new ConexionBD();
        }

        /// <summary>
        /// Genera un PDF con el historial de una mascota
        /// </summary>
        public bool GenerarHistorialMascota(int idMascota, string nombreMascota, string rutaGuardado)
        {
            try
            {
                conexion.Abrir();
                
                string query = @"SELECT 
                    m.id_mascota, m.nombre, m.especie, m.raza, m.edad, m.peso,
                    c.nombre_cliente, c.telefono, c.email,
                    cit.fecha_cita, cit.hora_cita, cit.motivo, cit.diagnostico, cit.tratamiento
                    FROM mascotas m
                    LEFT JOIN clientes c ON m.id_cliente = c.id_cliente
                    LEFT JOIN citas cit ON m.id_mascota = cit.id_mascota
                    WHERE m.id_mascota = @idMascota
                    ORDER BY cit.fecha_cita DESC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idMascota", idMascota);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos para esta mascota.");
                    return false;
                }

                // Aquí iría la lógica de generación de PDF con iTextSharp
                // Por ahora, creamos un archivo de demostración
                string contenido = GenerarContenidoHistorial(dt);
                File.WriteAllText(rutaGuardado, contenido);

                MessageBox.Show($"PDF generado exitosamente en: {rutaGuardado}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Genera un PDF con el reporte de citas del día
        /// </summary>
        public bool GenerarReporteCitasDia(DateTime fecha, string rutaGuardado)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    cit.id_cita, cit.fecha_cita, cit.hora_cita, 
                    m.nombre AS mascota, m.especie,
                    c.nombre_cliente, c.telefono,
                    cit.motivo, cit.diagnostico, cit.estado
                    FROM citas cit
                    JOIN mascotas m ON cit.id_mascota = m.id_mascota
                    JOIN clientes c ON m.id_cliente = c.id_cliente
                    WHERE CAST(cit.fecha_cita AS DATE) = @fecha
                    ORDER BY cit.hora_cita ASC";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@fecha", fecha.Date);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                string contenido = GenerarContenidoReporteCitas(dt, fecha);
                File.WriteAllText(rutaGuardado, contenido);

                MessageBox.Show($"Reporte de citas generado en: {rutaGuardado}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Genera un PDF con factura de servicios
        /// </summary>
        public bool GenerarFactura(int idCita, decimal monto, string concepto, string rutaGuardado)
        {
            try
            {
                conexion.Abrir();

                string query = @"SELECT 
                    cit.id_cita, cit.fecha_cita,
                    m.nombre AS mascota,
                    c.nombre_cliente, c.telefono, c.email
                    FROM citas cit
                    JOIN mascotas m ON cit.id_mascota = m.id_mascota
                    JOIN clientes c ON m.id_cliente = c.id_cliente
                    WHERE cit.id_cita = @idCita";

                SqlCommand cmd = new SqlCommand(query, conexion.leer);
                cmd.Parameters.AddWithValue("@idCita", idCita);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró la cita especificada.");
                    return false;
                }

                string contenido = GenerarContenidoFactura(dt, monto, concepto);
                File.WriteAllText(rutaGuardado, contenido);

                MessageBox.Show($"Factura generada en: {rutaGuardado}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar factura: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private string GenerarContenidoHistorial(DataTable dt)
        {
            string contenido = "=== HISTORIAL MÉDICO DE MASCOTA ===\n\n";
            
            if (dt.Rows.Count > 0)
            {
                DataRow primeraFila = dt.Rows[0];
                contenido += $"Mascota: {primeraFila["nombre"]}\n";
                contenido += $"Especie: {primeraFila["especie"]}\n";
                contenido += $"Raza: {primeraFila["raza"]}\n";
                contenido += $"Edad: {primeraFila["edad"]}\n";
                contenido += $"Peso: {primeraFila["peso"]} kg\n\n";
                contenido += $"Propietario: {primeraFila["nombre_cliente"]}\n";
                contenido += $"Teléfono: {primeraFila["telefono"]}\n";
                contenido += $"Email: {primeraFila["email"]}\n\n";
                contenido += "=== HISTORIAL DE CITAS ===\n\n";

                foreach (DataRow row in dt.Rows)
                {
                    if (row["fecha_cita"] != DBNull.Value)
                    {
                        contenido += $"Fecha: {row["fecha_cita"]}\n";
                        contenido += $"Motivo: {row["motivo"]}\n";
                        contenido += $"Diagnóstico: {row["diagnostico"]}\n";
                        contenido += $"Tratamiento: {row["tratamiento"]}\n";
                        contenido += "---\n\n";
                    }
                }
            }

            return contenido;
        }

        private string GenerarContenidoReporteCitas(DataTable dt, DateTime fecha)
        {
            string contenido = $"=== REPORTE DE CITAS - {fecha:dd/MM/yyyy} ===\n\n";
            contenido += $"Total de citas: {dt.Rows.Count}\n\n";
            contenido += "=== LISTADO DE CITAS ===\n\n";

            foreach (DataRow row in dt.Rows)
            {
                contenido += $"Hora: {row["hora_cita"]}\n";
                contenido += $"Mascota: {row["mascota"]} ({row["especie"]})\n";
                contenido += $"Propietario: {row["nombre_cliente"]}\n";
                contenido += $"Teléfono: {row["telefono"]}\n";
                contenido += $"Motivo: {row["motivo"]}\n";
                contenido += $"Estado: {row["estado"]}\n";
                contenido += "---\n\n";
            }

            return contenido;
        }

        private string GenerarContenidoFactura(DataTable dt, decimal monto, string concepto)
        {
            string contenido = "=== FACTURA DE SERVICIOS VETERINARIOS ===\n\n";
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                contenido += $"Fecha: {DateTime.Now:dd/MM/yyyy}\n";
                contenido += $"Cita #: {row["id_cita"]}\n\n";
                contenido += $"Cliente: {row["nombre_cliente"]}\n";
                contenido += $"Teléfono: {row["telefono"]}\n";
                contenido += $"Email: {row["email"]}\n\n";
                contenido += $"Mascota: {row["mascota"]}\n";
                contenido += $"Fecha de atención: {row["fecha_cita"]}\n\n";
                contenido += "=== DETALLE DE SERVICIOS ===\n\n";
                contenido += $"Concepto: {concepto}\n";
                contenido += $"Monto: ${monto:F2}\n\n";
                contenido += "=== TOTAL ===\n";
                contenido += $"${monto:F2}\n\n";
                contenido += "Gracias por su confianza en Clinipet\n";
            }

            return contenido;
        }
    }
}
