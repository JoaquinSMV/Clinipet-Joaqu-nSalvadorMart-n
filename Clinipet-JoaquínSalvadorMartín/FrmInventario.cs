using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmInventario : Form
    {
        ConexionBD conexion = new ConexionBD();
        private DataGridView dgvProductos;
        private ComboBox cmbCategorias;
        private TextBox txtBuscar;

        // Win32 API para placeholder en TextBox (.NET Framework compatible)
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public FrmInventario()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Padding = new Padding(25);
            ConfigurarUI();
            CargarDatos();
        }

        private void ConfigurarUI()
        {
            // Panel Superior (Filtros y Acciones)
            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 100, Padding = new Padding(0, 0, 0, 20) };
            
            Label lblTitulo = new Label { 
                Text = "Gestión de Inventario", 
                Font = new Font("Segoe UI Semibold", 18F), 
                ForeColor = Color.FromArgb(45, 52, 54),
                AutoSize = true,
                Location = new Point(0, 0)
            };
            pnlTop.Controls.Add(lblTitulo);

            txtBuscar = new TextBox { 
                Width = 250, 
                Location = new Point(0, 50),
                Font = new Font("Segoe UI", 10F)
            };
            // Establecer el placeholder usando Win32 API
            SendMessage(txtBuscar.Handle, EM_SETCUEBANNER, 0, "🔍 Buscar producto...");
            txtBuscar.TextChanged += (s, e) => FiltrarProductos();
            pnlTop.Controls.Add(txtBuscar);

            cmbCategorias = new ComboBox { 
                Width = 150, 
                Location = new Point(260, 50),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategorias.SelectedIndexChanged += (s, e) => FiltrarProductos();
            pnlTop.Controls.Add(cmbCategorias);

            Button btnNuevo = CrearBoton("  ✚  Nuevo Producto", Color.FromArgb(0, 184, 148), new Point(420, 48));
            btnNuevo.Click += (s, e) => MessageBox.Show("Funcionalidad para añadir producto en desarrollo.");
            pnlTop.Controls.Add(btnNuevo);

            Button btnEntrada = CrearBoton("  📥  Entrada Stock", Color.FromArgb(9, 132, 227), new Point(610, 48));
            pnlTop.Controls.Add(btnEntrada);

            this.Controls.Add(pnlTop);

            // DataGridView
            dgvProductos = new DataGridView {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                EnableHeadersVisualStyles = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 40 }
            };
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvProductos.ColumnHeadersHeight = 45;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
            
            this.Controls.Add(dgvProductos);
            dgvProductos.BringToFront();
        }

        private Button CrearBoton(string texto, Color color, Point loc)
        {
            Button btn = new Button {
                Text = texto,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9.5F),
                Size = new Size(180, 35),
                Location = loc,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void CargarDatos()
        {
            try {
                GestorInventario gestor = new GestorInventario();
                DataTable dt = gestor.ObtenerMedicamentos();
                if (dt != null) {
                    dgvProductos.DataSource = dt;
                } else {
                    MostrarDatosEjemplo();
                }
            } catch {
                MostrarDatosEjemplo();
            }
        }

        private void MostrarDatosEjemplo()
        {
            DataTable dtEjemplo = new DataTable();
            dtEjemplo.Columns.Add("nombre_medicamento", typeof(string));
            dtEjemplo.Columns.Add("cantidad_stock", typeof(string));
            dtEjemplo.Columns.Add("precio_unitario", typeof(string));
            dtEjemplo.Columns.Add("proveedor", typeof(string));
            dtEjemplo.Rows.Add("Paracetamol Vet", "50", "12.50€", "FarmaVet");
            dtEjemplo.Rows.Add("Pienso Adulto 10kg", "12", "45.00€", "RoyalCanin");
            dtEjemplo.Rows.Add("Collar Antiparasitario", "5", "18.90€", "Seresto");
            dgvProductos.DataSource = dtEjemplo;
        }

        private void FiltrarProductos() { /* Lógica de filtrado */ }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmInventario";
            this.Text = "Inventario Profesional";
            this.ResumeLayout(false);
        }
    }
}
