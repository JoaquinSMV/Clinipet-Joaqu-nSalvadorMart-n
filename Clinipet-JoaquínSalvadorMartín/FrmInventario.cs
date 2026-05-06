using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmInventario : Form
    {
        private GestorInventario gestorInventario;
        private DataGridView dgvMedicamentos;
        private TextBox txtBuscar;

        public FrmInventario()
        {
            InitializeComponent();
            gestorInventario = new GestorInventario();
            this.DoubleBuffered = true;
            AplicarEstilosModernos();
            CrearControles();
            CargarMedicamentos();
        }

        private void AplicarEstilosModernos()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Padding = new Padding(35);
            this.Font = new Font("Segoe UI", 10F);
        }

        private void CrearControles()
        {
            // Título
            Label lblTitulo = new Label
            {
                Text = "📦 Gestión de Inventario de Medicamentos",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 53, 65),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            this.Controls.Add(lblTitulo);

            // Panel de búsqueda y acciones
            Panel pnlBusqueda = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(20, 70),
                Size = new Size(this.Width - 70, 60),
                Padding = new Padding(15)
            };

            Label lblBuscar = new Label
            {
                Text = "Buscar:",
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                Location = new Point(10, 15)
            };
            pnlBusqueda.Controls.Add(lblBuscar);

            txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(80, 12),
                Size = new Size(250, 30),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtBuscar.TextChanged += (s, e) => FiltrarMedicamentos();
            pnlBusqueda.Controls.Add(txtBuscar);

            Button btnAgregar = new Button
            {
                Text = "➕ Agregar Medicamento",
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 35),
                Location = new Point(350, 12)
            };
            btnAgregar.Click += BtnAgregar_Click;
            pnlBusqueda.Controls.Add(btnAgregar);

            Button btnStockBajo = new Button
            {
                Text = "⚠️ Stock Bajo",
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 35),
                Location = new Point(550, 12)
            };
            btnStockBajo.Click += BtnStockBajo_Click;
            pnlBusqueda.Controls.Add(btnStockBajo);

            this.Controls.Add(pnlBusqueda);

            // DataGridView
            dgvMedicamentos = new DataGridView
            {
                Location = new Point(20, 150),
                Size = new Size(this.Width - 70, this.Height - 220),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(230, 230, 230),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Estilo de encabezados
            dgvMedicamentos.ColumnHeadersHeight = 40;
            dgvMedicamentos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 53, 65);
            dgvMedicamentos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMedicamentos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);

            // Estilo de filas
            dgvMedicamentos.RowTemplate.Height = 40;
            dgvMedicamentos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvMedicamentos.DefaultCellStyle.ForeColor = Color.FromArgb(70, 80, 90);
            dgvMedicamentos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 253);

            // Columnas
            dgvMedicamentos.Columns.Add("id_medicamento", "ID");
            dgvMedicamentos.Columns.Add("nombre_medicamento", "Medicamento");
            dgvMedicamentos.Columns.Add("cantidad_stock", "Stock");
            dgvMedicamentos.Columns.Add("cantidad_minima", "Mínimo");
            dgvMedicamentos.Columns.Add("precio_unitario", "Precio");
            dgvMedicamentos.Columns.Add("fecha_vencimiento", "Vencimiento");
            dgvMedicamentos.Columns.Add("proveedor", "Proveedor");

            dgvMedicamentos.Columns["id_medicamento"].Width = 50;
            dgvMedicamentos.Columns["nombre_medicamento"].Width = 150;
            dgvMedicamentos.Columns["cantidad_stock"].Width = 80;
            dgvMedicamentos.Columns["cantidad_minima"].Width = 80;
            dgvMedicamentos.Columns["precio_unitario"].Width = 80;
            dgvMedicamentos.Columns["fecha_vencimiento"].Width = 100;

            this.Controls.Add(dgvMedicamentos);
        }

        private void CargarMedicamentos()
        {
            try
            {
                DataTable dt = gestorInventario.ObtenerMedicamentos();
                if (dt != null)
                {
                    dgvMedicamentos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar medicamentos: " + ex.Message);
            }
        }

        private void FiltrarMedicamentos()
        {
            try
            {
                DataTable dt = gestorInventario.ObtenerMedicamentos();
                if (dt != null && !string.IsNullOrEmpty(txtBuscar.Text))
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = $"nombre_medicamento LIKE '%{txtBuscar.Text}%'";
                    dgvMedicamentos.DataSource = dv;
                }
                else
                {
                    CargarMedicamentos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            // Aquí se podría abrir un formulario para agregar medicamento
            MessageBox.Show("Función de agregar medicamento (próximamente)");
        }

        private void BtnStockBajo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = gestorInventario.ObtenerMedicamentosStockBajo();
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvMedicamentos.DataSource = dt;
                    MessageBox.Show($"Se encontraron {dt.Rows.Count} medicamentos con stock bajo.");
                }
                else
                {
                    MessageBox.Show("No hay medicamentos con stock bajo.");
                    CargarMedicamentos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
