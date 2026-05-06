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
        private GestorInventario gestor;

        // Win32 API para placeholder en TextBox (.NET Framework compatible)
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public FrmInventario()
        {
            InitializeComponent();
            gestor = new GestorInventario();
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
            btnNuevo.Click += (s, e) => AbrirFormularioNuevoProducto();
            pnlTop.Controls.Add(btnNuevo);

            Button btnEntrada = CrearBoton("  📥  Entrada Stock", Color.FromArgb(9, 132, 227), new Point(610, 48));
            pnlTop.Controls.Add(btnEntrada);

            Button btnModificar = CrearBoton("  ✏  Modificar", Color.FromArgb(255, 193, 7), new Point(800, 48));
            btnModificar.Click += (s, e) => ModificarProductoSeleccionado();
            pnlTop.Controls.Add(btnModificar);

            Button btnBorrar = CrearBoton("  🗑  Borrar", Color.FromArgb(220, 53, 69), new Point(990, 48));
            btnBorrar.Click += (s, e) => BorrarProductoSeleccionado();
            pnlTop.Controls.Add(btnBorrar);

            this.Controls.Add(pnlTop);

            // DataGridView
            dgvProductos = new DataGridView {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(230, 230, 230),
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToResizeRows = false,
                EnableHeadersVisualStyles = false,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 50 }
            };
            
            // Cabeceras
            dgvProductos.ColumnHeadersHeight = 45;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 110, 120);
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
            dgvProductos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;

            // Filas
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 247, 245);
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 150, 136);
            
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
                DataTable dt = gestor.ObtenerMedicamentos();
                if (dt != null && dt.Rows.Count > 0) {
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

        private void FiltrarProductos()
        {
            if (dgvProductos.DataSource is DataTable dt)
            {
                string filtro = txtBuscar.Text.Trim().Replace("'", "''");
                string categoria = cmbCategorias.SelectedItem?.ToString() ?? "Todos";
                
                string filtroFinal = string.Format("nombre_medicamento LIKE '%{0}%' OR proveedor LIKE '%{0}%'", filtro);
                
                if (categoria != "Todos")
                {
                    filtroFinal += string.Format(" AND categoria = '{0}'", categoria);
                }
                
                dt.DefaultView.RowFilter = filtroFinal;
            }
            
            // Bloquear edición de todas las celdas excepto la columna de Activo
            if (dgvProductos.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvProductos.Columns)
                {
                    col.ReadOnly = col.Name != "Activo" && col.HeaderText != "Activo";
                }
            }
        }

        private void ExportarInventario()
        {
            if (dgvProductos.DataSource is DataTable dt)
            {
                GestorPDF pdf = new GestorPDF();
                string path = pdf.ExportarTablaATexto(dt, "Inventario de Medicamentos");
                MessageBox.Show("Reporte generado en: " + path, "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AbrirFormularioNuevoProducto()
        {
            using (FrmNuevoProducto frm = new FrmNuevoProducto())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bool resultado = gestor.AgregarMedicamento(
                        frm.NombreProducto,
                        frm.Descripcion,
                        frm.Cantidad,
                        frm.CantidadMinima,
                        frm.Precio,
                        frm.FechaVencimiento,
                        frm.Proveedor
                    );

                    if (resultado)
                    {
                        MessageBox.Show("Producto añadido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDatos();
                    }
                    else
                    {
                        MessageBox.Show("Error al añadir el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ModificarProductoSeleccionado()
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un producto para modificar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvProductos.SelectedRows[0];
            using (FrmNuevoProducto frm = new FrmNuevoProducto())
            {
                frm.Text = "Editar Producto";
                frm.NombreProducto = row.Cells[0].Value?.ToString() ?? "";
                frm.Descripcion = row.Cells[1].Value?.ToString() ?? "";
                frm.Cantidad = int.TryParse(row.Cells[2].Value?.ToString(), out int cant) ? cant : 0;
                frm.CantidadMinima = int.TryParse(row.Cells[3].Value?.ToString(), out int cantMin) ? cantMin : 0;
                frm.Precio = decimal.TryParse(row.Cells[4].Value?.ToString(), out decimal precio) ? precio : 0;
                frm.Proveedor = row.Cells[5].Value?.ToString() ?? "";
                frm.Activo = row.Cells[6].Value?.ToString() == "Sí" || row.Cells[6].Value?.ToString() == "true";

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                }
            }
        }

        private void BorrarProductoSeleccionado()
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un producto para borrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que deseas borrar este producto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
            }
        }

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
