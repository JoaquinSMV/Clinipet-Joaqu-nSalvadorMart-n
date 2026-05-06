using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Padding = new Padding(35);
            ConfigurarUI();
        }

        private void ConfigurarUI()
        {
            Label lblTitulo = new Label
            {
                Text = "Informes y Reportes",
                Font = new Font("Segoe UI Semibold", 20F),
                ForeColor = Color.FromArgb(45, 52, 54),
                Dock = DockStyle.Top,
                Height = 50
            };
            this.Controls.Add(lblTitulo);

            TableLayoutPanel gridReportes = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 20, 0, 0)
            };
            gridReportes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            gridReportes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            gridReportes.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            gridReportes.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.Controls.Add(gridReportes);
            gridReportes.BringToFront();

            gridReportes.Controls.Add(CrearTarjetaReporte("Reporte de Citas", "Resumen mensual de citas atendidas y canceladas.", Color.FromArgb(0, 184, 148)), 0, 0);
            gridReportes.Controls.Add(CrearTarjetaReporte("Reporte de Clientes", "Nuevos registros y actividad de clientes por zona.", Color.FromArgb(9, 132, 227)), 1, 0);
            gridReportes.Controls.Add(CrearTarjetaReporte("Reporte Médico", "Historial de diagnósticos y tratamientos frecuentes.", Color.FromArgb(253, 176, 34)), 0, 1);
            gridReportes.Controls.Add(CrearTarjetaReporte("Reporte de Inventario", "Alertas de stock bajo y consumo de suministros.", Color.FromArgb(255, 118, 117)), 1, 1);
        }

        private Panel CrearTarjetaReporte(string titulo, string descripcion, Color colorAcento)
        {
            Panel pnl = new Panel
            {
                BackColor = Color.White,
                Margin = new Padding(10),
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            Panel acento = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = colorAcento };
            pnl.Controls.Add(acento);

            Label lblTit = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI Semibold", 14F),
                ForeColor = Color.FromArgb(45, 52, 54),
                Dock = DockStyle.Top,
                Height = 35,
                Padding = new Padding(10, 0, 0, 0)
            };
            pnl.Controls.Add(lblTit);

            Label lblDesc = new Label
            {
                Text = descripcion,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(120, 130, 140),
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 10, 0, 0)
            };
            pnl.Controls.Add(lblDesc);

            Button btnGenerar = new Button
            {
                Text = "Generar PDF",
                Dock = DockStyle.Bottom,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = colorAcento,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Bold", 10F),
                Cursor = Cursors.Hand
            };
            btnGenerar.FlatAppearance.BorderSize = 0;
            pnl.Controls.Add(btnGenerar);

            return pnl;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmReportes";
            this.Text = "Reportes";
            this.ResumeLayout(false);
        }
    }
}
