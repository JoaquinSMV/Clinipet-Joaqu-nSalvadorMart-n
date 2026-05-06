using System;
using System.IO;

namespace Clinipet_JoaquínSalvadorMartín
{
    /// <summary>
    /// Clase para configurar rutas y parámetros de las nuevas funcionalidades
    /// </summary>
    public static class ConfiguracionMejoras
    {
        // Rutas de almacenamiento
        public static string RutaPDFs { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
            "Clinipet", "Reportes");

        public static string RutaBackups { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
            "Clinipet", "Backups");

        public static string RutaExportes { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
            "Clinipet", "Exportes");

        // Configuración de alertas
        public static int DiasAlertaMedicamentosProximosAVencer { get; set; } = 30;
        public static int DiasAlertaCitasProximas { get; set; } = 7;
        public static decimal PorcentajeAlertaStockBajo { get; set; } = 0.20m; // 20%

        // Configuración de reportes
        public static string NombreClinica { get; set; } = "Clinipet";
        public static string DireccionClinica { get; set; } = "Dirección no configurada";
        public static string TelefonoClinica { get; set; } = "Teléfono no configurado";
        public static string EmailClinica { get; set; } = "email@clinipet.com";
        public static string LogoClinica { get; set; } = "";

        // Configuración de moneda
        public static string SimboloMoneda { get; set; } = "$";
        public static string NombreMoneda { get; set; } = "Pesos";

        // Configuración de estadísticas
        public static int LimiteClientesMasActivos { get; set; } = 10;
        public static int LimiteServiciosMasSolicitados { get; set; } = 10;
        public static int LimiteMedicamentosMasUtilizados { get; set; } = 10;

        // Configuración de PDF
        public static bool GenerarPDFAutomaticamente { get; set; } = false;
        public static bool AbrirPDFAlGenerarse { get; set; } = true;

        /// <summary>
        /// Inicializa las rutas necesarias
        /// </summary>
        public static void InicializarRutas()
        {
            try
            {
                if (!Directory.Exists(RutaPDFs))
                    Directory.CreateDirectory(RutaPDFs);

                if (!Directory.Exists(RutaBackups))
                    Directory.CreateDirectory(RutaBackups);

                if (!Directory.Exists(RutaExportes))
                    Directory.CreateDirectory(RutaExportes);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Error al crear directorios: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la ruta completa para un PDF de reporte
        /// </summary>
        public static string ObtenerRutaPDF(string nombreReporte)
        {
            return Path.Combine(RutaPDFs, 
                $"{nombreReporte}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        }

        /// <summary>
        /// Obtiene la ruta completa para un backup
        /// </summary>
        public static string ObtenerRutaBackup(string nombreBackup)
        {
            return Path.Combine(RutaBackups, 
                $"{nombreBackup}_{DateTime.Now:yyyyMMdd_HHmmss}.bak");
        }

        /// <summary>
        /// Obtiene la ruta completa para un archivo de exportación
        /// </summary>
        public static string ObtenerRutaExporte(string nombreExporte, string extension = ".xlsx")
        {
            return Path.Combine(RutaExportes, 
                $"{nombreExporte}_{DateTime.Now:yyyyMMdd_HHmmss}{extension}");
        }

        /// <summary>
        /// Valida que todas las rutas existan
        /// </summary>
        public static bool ValidarRutas()
        {
            return Directory.Exists(RutaPDFs) &&
                   Directory.Exists(RutaBackups) &&
                   Directory.Exists(RutaExportes);
        }

        /// <summary>
        /// Obtiene información de configuración actual
        /// </summary>
        public static string ObtenerInfoConfiguracion()
        {
            return $@"
=== CONFIGURACIÓN DE MEJORAS CLINIPET ===

Clínica: {NombreClinica}
Dirección: {DireccionClinica}
Teléfono: {TelefonoClinica}
Email: {EmailClinica}

Rutas:
- PDFs: {RutaPDFs}
- Backups: {RutaBackups}
- Exportes: {RutaExportes}

Alertas:
- Días para alertar medicamentos próximos a vencer: {DiasAlertaMedicamentosProximosAVencer}
- Días para alertar citas próximas: {DiasAlertaCitasProximas}
- Porcentaje de alerta stock bajo: {PorcentajeAlertaStockBajo * 100}%

Moneda: {NombreMoneda} ({SimboloMoneda})

Límites de reportes:
- Clientes más activos: {LimiteClientesMasActivos}
- Servicios más solicitados: {LimiteServiciosMasSolicitados}
- Medicamentos más utilizados: {LimiteMedicamentosMasUtilizados}

PDF:
- Generar automáticamente: {GenerarPDFAutomaticamente}
- Abrir al generarse: {AbrirPDFAlGenerarse}
";
        }
    }
}
