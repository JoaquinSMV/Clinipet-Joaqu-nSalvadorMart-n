using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public class GestorPDF
    {
        private ConexionBD conexion;

        public GestorPDF()
        {
            conexion = new ConexionBD();
        }

        public string GenerarReporteCita(int citaId, string mascota, string fecha, string motivo, string diagnostico, string tratamiento)
        {
            try
            {
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Clinipet_PDFs");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string fileName = $"Cita_{citaId}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string path = Path.Combine(folder, fileName);

                string contenido = $@"
==================================================
        CLINIPET - REPORTE MÉDICO
==================================================
ID CITA: {citaId}
FECHA: {fecha}
MASCOTA: {mascota}
--------------------------------------------------
MOTIVO DE CONSULTA:
{motivo}

DIAGNÓSTICO:
{diagnostico}

TRATAMIENTO:
{tratamiento}
--------------------------------------------------
Generado el: {DateTime.Now}
Gracias por confiar en Clinipet.
==================================================";

                File.WriteAllText(path, contenido);
                return path;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public bool GenerarHistorialMascota(int idMascota, string nombreMascota, string rutaGuardado)
        {
            // Método mantenido para compatibilidad
            return true;
        }

        public string ExportarTablaATexto(DataTable dt, string titulo)
        {
            try
            {
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Clinipet_Reportes");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string fileName = $"{titulo.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string path = Path.Combine(folder, fileName);

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("==================================================");
                    sw.WriteLine($"        CLINIPET - {titulo.ToUpper()}");
                    sw.WriteLine("==================================================");
                    sw.WriteLine($"Fecha de generación: {DateTime.Now}");
                    sw.WriteLine("--------------------------------------------------");
                    sw.WriteLine();

                    // Cabeceras
                    foreach (DataColumn col in dt.Columns)
                    {
                        sw.Write($"{col.ColumnName,-20} ");
                    }
                    sw.WriteLine();
                    sw.WriteLine(new string('-', dt.Columns.Count * 21));

                    // Datos
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            sw.Write($"{item?.ToString().Replace("\n", " ").Replace("\r", ""),-20} ");
                        }
                        sw.WriteLine();
                    }

                    sw.WriteLine();
                    sw.WriteLine("--------------------------------------------------");
                    sw.WriteLine("Fin del reporte.");
                    sw.WriteLine("==================================================");
                }

                return path;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
