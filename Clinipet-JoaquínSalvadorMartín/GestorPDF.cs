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
    }
}
