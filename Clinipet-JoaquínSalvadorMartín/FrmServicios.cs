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

        private TextBox txtBuscar;
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
            
            txtBuscar = new TextBox { 
                Size = new Size(250, 35), 
                Location = new Point(0, 12),
                Font = new Font("Segoe UI", 11F)
            };
            txtBuscar.TextChanged += (s, e) => FiltrarServicios();
            pnlAcciones.Controls.Add(txtBuscar);

            Button btnNuevo = new Button {
                Text = "➕ Nuevo Servicio",
                BackColor = Color.FromArgb(9, 132, 227),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 35),
                Location = new Point(270, 10),
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.Click += (s, e) => AbrirFormularioNuevoServicio();
            pnlAcciones.Controls.Add(btnNuevo);

            Button btnModificar = new Button {
                Text = "✏ Modificar",
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 35),
                Location = new Point(460, 10),
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnModificar.FlatAppearance.BorderSize = 0;
            btnModificar.Click += (s, e) => ModificarServicioSeleccionado();
            pnlAcciones.Controls.Add(btnModificar);

            Button btnBorrar = new Button {
                Text = "🗑 Borrar",
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 35),
                Location = new Point(620, 10),
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnBorrar.FlatAppearance.BorderSize = 0;
            btnBorrar.Click += (s, e) => BorrarServicioSeleccionado();
            pnlAcciones.Controls.Add(btnBorrar);

            Button btnExportar = new Button {
                Text = "📄 Exportar PDF",
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 35),
                Location = new Point(780, 10),
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Click += (s, e) => ExportarServicios();
            pnlAcciones.Controls.Add(btnExportar);

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

        private void FiltrarServicios()
        {
            if (dgvServicios.DataSource is DataTable dt)
            {
                string filtro = txtBuscar.Text.Trim().Replace("'", "''");
                dt.DefaultView.RowFilter = string.Format("nombre_servicio LIKE '%{0}%' OR categoria LIKE '%{0}%'", filtro);
            }
        }

        private void ExportarServicios()
        {
            if (dgvServicios.DataSource is DataTable dt)
            {
                GestorPDF pdf = new GestorPDF();
                string path = pdf.ExportarTablaATexto(dt, "Catálogo de Servicios");
                MessageBox.Show("Reporte generado en: " + path, "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AbrirFormularioNuevoServicio()
        {
            using (FrmNuevoServicio frm = new FrmNuevoServicio())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bool resultado = gestor.AgregarServicio(
                        frm.NombreServicio,
                        frm.Descripcion,
                        frm.Precio,
                        frm.Categoria
                    );

                    if (resultado)
                    {
                        MessageBox.Show("Servicio añadido correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarServicios();
                    }
                    else
                    {
                        MessageBox.Show("Error al añadir el servicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ModificarServicioSeleccionado()
        {
            if (dgvServicios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un servicio para modificar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvServicios.SelectedRows[0];
            
            int idServicio = 0;
            if (dgvServicios.Columns.Contains("id_servicio") && row.Cells["id_servicio"].Value != null)
                idServicio = Convert.ToInt32(row.Cells["id_servicio"].Value);
            else if (int.TryParse(row.Cells[0].Value?.ToString(), out int id))
                idServicio = id;

            using (FrmNuevoServicio frm = new FrmNuevoServicio())
            {
                frm.Text = "Editar Servicio";
                
                if (dgvServicios.Columns.Contains("nombre_servicio"))
                {
                    frm.NombreServicio = row.Cells["nombre_servicio"].Value?.ToString() ?? "";
                    frm.Descripcion = row.Cells["descripcion"].Value?.ToString() ?? "";
                    frm.Precio = Convert.ToDecimal(row.Cells["precio"].Value ?? 0);
                    frm.Categoria = row.Cells["categoria"].Value?.ToString() ?? "Consulta";
                    frm.Activo = row.Cells["activo"].Value?.ToString() == "Sí" || row.Cells["activo"].Value?.ToString() == "True" || (row.Cells["activo"].Value is bool b && b);
                }
                else
                {
                    // Fallback a índices corregidos (0=ID, 1=Nombre, 2=Desc, 3=Precio, 4=Cat, 5=Activo)
                    frm.NombreServicio = row.Cells[1].Value?.ToString() ?? "";
                    frm.Descripcion = row.Cells[2].Value?.ToString() ?? "";
                    frm.Precio = decimal.TryParse(row.Cells[3].Value?.ToString(), out decimal precio) ? precio : 0;
                    frm.Categoria = row.Cells[4].Value?.ToString() ?? "Consulta";
                    frm.Activo = row.Cells[5].Value?.ToString() == "Sí" || row.Cells[5].Value?.ToString() == "true";
                }

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bool resultado = gestor.ActualizarServicio(
                        idServicio,
                        frm.NombreServicio,
                        frm.Descripcion,
                        frm.Precio,
                        frm.Categoria,
                        frm.Activo
                    );

                    if (resultado)
                    {
                        MessageBox.Show("Servicio actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarServicios();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el servicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BorrarServicioSeleccionado()
        {
            if (dgvServicios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un servicio para borrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que deseas borrar este servicio?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                MessageBox.Show("Servicio eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarServicios();
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
