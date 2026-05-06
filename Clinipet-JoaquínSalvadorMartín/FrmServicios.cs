using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmServicios : Form
    {
        private GestorServicios gestorServicios;
        private DataGridView dgvServicios;
        private ComboBox cmbCategoria;

        public FrmServicios()
        {
            InitializeComponent();
            gestorServicios = new GestorServicios();
            this.DoubleBuffered = true;
            AplicarEstilosModernos();
            CrearControles();
            CargarServicios();
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
                Text = "🏥 Catálogo de Servicios",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 53, 65),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            this.Controls.Add(lblTitulo);

            // Panel de filtros
            Panel pnlFiltros = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(20, 70),
                Size = new Size(this.Width - 70, 60),
                Padding = new Padding(15)
            };

            Label lblCategoria = new Label
            {
                Text = "Categoría:",
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                Location = new Point(10, 15)
            };
            pnlFiltros.Controls.Add(lblCategoria);

            cmbCategoria = new ComboBox
            {
                Font = new Font("Segoe UI", 10F),
                Location = new Point(100, 12),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategoria.Items.Add("Todas");
            cmbCategoria.SelectedIndex = 0;
            cmbCategoria.SelectedIndexChanged += (s, e) => FiltrarPorCategoria();
            pnlFiltros.Controls.Add(cmbCategoria);

            Button btnAgregar = new Button
            {
                Text = "➕ Nuevo Servicio",
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 35),
                Location = new Point(320, 12)
            };
            btnAgregar.Click += BtnAgregar_Click;
            pnlFiltros.Controls.Add(btnAgregar);

            Button btnEstadisticas = new Button
            {
                Text = "📊 Estadísticas",
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 35),
                Location = new Point(490, 12)
            };
            btnEstadisticas.Click += BtnEstadisticas_Click;
            pnlFiltros.Controls.Add(btnEstadisticas);

            this.Controls.Add(pnlFiltros);

            // DataGridView
            dgvServicios = new DataGridView
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
            dgvServicios.ColumnHeadersHeight = 40;
            dgvServicios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 53, 65);
            dgvServicios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvServicios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);

            // Estilo de filas
            dgvServicios.RowTemplate.Height = 40;
            dgvServicios.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvServicios.DefaultCellStyle.ForeColor = Color.FromArgb(70, 80, 90);
            dgvServicios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 253);

            // Columnas
            dgvServicios.Columns.Add("id_servicio", "ID");
            dgvServicios.Columns.Add("nombre_servicio", "Servicio");
            dgvServicios.Columns.Add("descripcion", "Descripción");
            dgvServicios.Columns.Add("precio", "Precio ($)");
            dgvServicios.Columns.Add("categoria", "Categoría");

            dgvServicios.Columns["id_servicio"].Width = 50;
            dgvServicios.Columns["nombre_servicio"].Width = 150;
            dgvServicios.Columns["descripcion"].Width = 250;
            dgvServicios.Columns["precio"].Width = 100;

            this.Controls.Add(dgvServicios);
        }

        private void CargarServicios()
        {
            try
            {
                DataTable dt = gestorServicios.ObtenerServicios();
                if (dt != null)
                {
                    dgvServicios.DataSource = dt;

                    // Cargar categorías
                    DataTable categorias = gestorServicios.ObtenerCategorias();
                    if (categorias != null)
                    {
                        foreach (DataRow row in categorias.Rows)
                        {
                            string categoria = row["categoria"].ToString();
                            if (!cmbCategoria.Items.Contains(categoria))
                            {
                                cmbCategoria.Items.Add(categoria);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios: " + ex.Message);
            }
        }

        private void FiltrarPorCategoria()
        {
            try
            {
                if (cmbCategoria.SelectedItem.ToString() == "Todas")
                {
                    CargarServicios();
                }
                else
                {
                    DataTable dt = gestorServicios.ObtenerServiciosPorCategoria(cmbCategoria.SelectedItem.ToString());
                    if (dt != null)
                    {
                        dgvServicios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función de agregar servicio (próximamente)");
        }

        private void BtnEstadisticas_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = gestorServicios.ObtenerEstadisticasServicios();
                if (dt != null && dt.Rows.Count > 0)
                {
                    string mensaje = "📊 Estadísticas de Servicios por Categoría:\n\n";
                    foreach (DataRow row in dt.Rows)
                    {
                        mensaje += $"Categoría: {row["categoria"]}\n";
                        mensaje += $"  Cantidad: {row["cantidad_servicios"]}\n";
                        mensaje += $"  Precio Promedio: ${row["precio_promedio"]}\n";
                        mensaje += $"  Rango: ${row["precio_minimo"]} - ${row["precio_maximo"]}\n\n";
                    }
                    MessageBox.Show(mensaje);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
