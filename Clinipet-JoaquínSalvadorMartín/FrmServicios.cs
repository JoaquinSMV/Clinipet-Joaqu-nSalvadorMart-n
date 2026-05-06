using System;
using System.Data;
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

        private DataGridView dgvServicios;
        private GestorServicios gestor;

        private void ConfigurarUI()
        {
            gestor = new GestorServicios();

            // Título
            Label lblTitulo = new Label
            {
                Text = "🛠️ Catálogo de Servicios y Tratamientos",
                Font = new Font("Segoe UI Semibold", 20F),
                ForeColor = Color.FromArgb(45, 52, 54),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblTitulo);

            // Panel de Acciones
            Panel pnlAcciones = new Panel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(0, 10, 0, 10) };
            
            Button btnNuevo = new Button {
                Text = "➕ Nuevo Servicio",
                BackColor = Color.FromArgb(9, 132, 227),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 35),
                Location = new Point(0, 10),
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnNuevo.FlatAppearance.BorderSize = 0;
            pnlAcciones.Controls.Add(btnNuevo);

            this.Controls.Add(pnlAcciones);

            // DataGridView
            dgvServicios = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(230, 230, 230),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 50 }
            };
            
            // Cabeceras
            dgvServicios.ColumnHeadersHeight = 45;
            dgvServicios.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvServicios.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 110, 120);
            dgvServicios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
            dgvServicios.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;

            // Filas
            dgvServicios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 247, 245);
            dgvServicios.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 150, 136);

            this.Controls.Add(dgvServicios);
            dgvServicios.BringToFront();

            CargarServicios();
        }

        private void CargarServicios()
        {
            try {
                DataTable dt = gestor.ObtenerServicios();
                if (dt != null && dt.Rows.Count > 0) {
                    dgvServicios.DataSource = dt;
                } else {
                    CargarDatosEjemplo();
                }
            } catch {
                CargarDatosEjemplo();
            }
        }

        private void CargarDatosEjemplo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nombre_servicio", typeof(string));
            dt.Columns.Add("descripcion", typeof(string));
            dt.Columns.Add("precio", typeof(string));
            dt.Columns.Add("categoria", typeof(string));
            
            dt.Rows.Add("Consulta General", "Revisión rutinaria", "30.00€", "Consulta");
            dt.Rows.Add("Vacuna Rabia", "Inmunización anual", "25.00€", "Vacunación");
            dt.Rows.Add("Limpieza Dental", "Profilaxis con ultrasonido", "80.00€", "Odontología");
            dt.Rows.Add("Corte y Baño", "Estética canina/felina", "35.00€", "Estética");
            
            dgvServicios.DataSource = dt;
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
