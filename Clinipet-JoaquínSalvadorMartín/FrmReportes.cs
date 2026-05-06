using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Padding = new Padding(30);
            ConfigurarUI();
        }

        private void ConfigurarUI()
        {
            Label lblTitulo = new Label { 
                Text = "Centro de Reportes y Estadísticas", 
                Font = new Font("Segoe UI Semibold", 20F), 
                ForeColor = Color.FromArgb(45, 52, 54),
                Dock = DockStyle.Top,
                Height = 60
            };
            this.Controls.Add(lblTitulo);

            FlowLayoutPanel pnlCards = new FlowLayoutPanel {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0, 10, 0, 0)
            };
            this.Controls.Add(pnlCards);

            pnlCards.Controls.Add(CrearTarjetaReporte("Resumen de Citas", "Analiza la afluencia de pacientes por día y mes.", Color.FromArgb(0, 184, 148), "citas"));
            pnlCards.Controls.Add(CrearTarjetaReporte("Rendimiento Económico", "Informe de ingresos por servicios y ventas de productos.", Color.FromArgb(9, 132, 227), "finanzas"));
            pnlCards.Controls.Add(CrearTarjetaReporte("Estado de Inventario", "Productos próximos a caducar y alertas de stock bajo.", Color.FromArgb(255, 118, 117), "stock"));
            pnlCards.Controls.Add(CrearTarjetaReporte("Actividad de Clientes", "Nuevos registros y fidelidad de los dueños.", Color.FromArgb(108, 117, 125), "clientes"));
        }

        private Panel CrearTarjetaReporte(string titulo, string desc, Color color, string tipo)
        {
            Panel card = new Panel {
                Size = new Size(350, 200),
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(20)
            };

            Label lblTit = new Label { Text = titulo, Font = new Font("Segoe UI Bold", 13F), Dock = DockStyle.Top, Height = 30 };
            Label lblDesc = new Label { Text = desc, Font = new Font("Segoe UI", 10F), ForeColor = Color.Gray, Dock = DockStyle.Fill, Padding = new Padding(0, 10, 0, 0) };
            
            Button btnExportar = new Button {
                Text = "📄 Exportar a PDF",
                Dock = DockStyle.Bottom,
                Height = 45,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10F),
                Cursor = Cursors.Hand
            };
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Click += (s, e) => ExportarReporte(tipo);

            card.Controls.Add(lblDesc);
            card.Controls.Add(lblTit);
            card.Controls.Add(btnExportar);

            return card;
        }

        private void ExportarReporte(string tipo)
        {
            try {
                GestorPDF gestor = new GestorPDF();
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Clinipet_PDFs");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                
                string ruta = Path.Combine(folder, $"Reporte_{tipo}_{DateTime.Now:yyyyMMdd}.txt");
                
                // Nota: En la rama mejoras-clinipet, GestorPDF genera .txt simulando PDF 
                // o requiere iTextSharp para PDF real.
                string resultado = gestor.GenerarReporteCita(0, "General", DateTime.Now.ToShortDateString(), "Reporte de " + tipo, "N/A", "N/A");
                
                MessageBox.Show($"Reporte generado con éxito en:\n{resultado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show("Error al generar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmReportes";
            this.Text = "Reportes Avanzados";
            this.ResumeLayout(false);
        }
    }
}
