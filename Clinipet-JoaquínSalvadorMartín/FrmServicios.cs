using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmServicios : Form
    {
        public FrmServicios()
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
                Text = "Catálogo de Servicios",
                Font = new Font("Segoe UI Semibold", 20F),
                ForeColor = Color.FromArgb(45, 52, 54),
                Dock = DockStyle.Top,
                Height = 50
            };
            this.Controls.Add(lblTitulo);

            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            this.Controls.Add(pnlMain);
            pnlMain.BringToFront();

            Label lblAviso = new Label
            {
                Text = "Módulo de Servicios\n\nConfigura aquí los servicios ofrecidos: Consultas, Vacunaciones, Cirugías, Peluquería, etc.\n\n(Sección en desarrollo)",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(120, 130, 140),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlMain.Controls.Add(lblAviso);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmServicios";
            this.Text = "Servicios";
            this.ResumeLayout(false);
        }
    }
}
