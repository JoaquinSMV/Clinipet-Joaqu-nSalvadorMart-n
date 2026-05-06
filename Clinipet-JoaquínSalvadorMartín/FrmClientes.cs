using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D; // Necesario para bordes suaves
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmClientes : Form
    {
        ConexionBD conexion = new ConexionBD();

        public FrmClientes()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Evita el parpadeo al redimensionar
            AplicarEstilosModernos();
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                conexion.Abrir();
                string query = "SELECT * FROM Clientes";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion.leer);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvClientes.DataSource = dt;

                // Ajuste automático de columnas para que se vea ordenado
                dgvClientes.Columns["ClienteID"].Visible = false;
                foreach (DataGridViewColumn col in dgvClientes.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            finally { conexion.Cerrar(); }

            // 1. Ocultar ID
            if (dgvClientes.Columns.Contains("ClienteID")) dgvClientes.Columns["ClienteID"].Visible = false;

            // 2. Configurar anchos automáticos
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Reset

            // Columnas fijas (datos cortos)
            string[] columnasCortas = { "DNI", "Telefono", "CP", "Provincia" };
            foreach (string col in columnasCortas)
            {
                if (dgvClientes.Columns.Contains(col))
                    dgvClientes.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // Columnas flexibles (datos largos)
            string[] columnasLargas = { "Nombre", "Apellidos", "Direccion", "Email", "Observaciones" };
            foreach (string col in columnasLargas)
            {
                if (dgvClientes.Columns.Contains(col))
                    dgvClientes.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }

        private void AplicarEstilosModernos()
        {
            // --- Configuración del Formulario ---
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Padding = new Padding(35);

            // --- Estilo del DataGridView ---
            dgvClientes.BackgroundColor = Color.White;
            dgvClientes.BorderStyle = BorderStyle.None;
            dgvClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClientes.GridColor = Color.FromArgb(230, 230, 230);
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.AllowUserToResizeRows = false;
            dgvClientes.EnableHeadersVisualStyles = false;
            dgvClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Cabeceras
            dgvClientes.ColumnHeadersHeight = 45;
            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 110, 120);
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
            dgvClientes.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;

            // Filas
            dgvClientes.RowTemplate.Height = 50;
            dgvClientes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 247, 245);
            dgvClientes.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 150, 136);

            // --- Estilo de Botones con Separación e Iconos ---
            // Usamos símbolos modernos que se ven bien en casi cualquier sistema
            DiseñarBoton(button1, Color.FromArgb(0, 184, 148), "  ✚  Añadir Cliente");
            DiseñarBoton(Modificar, Color.FromArgb(9, 132, 227), "  ✎  Modificar");
            DiseñarBoton(btnEliminar, Color.FromArgb(255, 118, 117), "  🗑  Eliminar");

            // Ajustamos la posición manualmente para separarlos (opcional si usas FlowLayoutPanel)
            // Si no usas un panel, esto les da un espacio de 15px entre ellos
            Modificar.Left = button1.Right + 15;
            btnEliminar.Left = Modificar.Right + 15;
        }
        private void DiseñarBoton(Button btn, Color colorFondo, string textoConIcono)
        {
            // Configuración visual básica
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = colorFondo;
            btn.ForeColor = Color.White; // Texto siempre blanco para mejor contraste
            btn.Text = textoConIcono;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Font = new Font("Segoe UI Semibold", 10.5F); // Un pelín más grande
            btn.Cursor = Cursors.Hand;
            btn.Size = new Size(180, 45); // Un poco más ancho para que respire el texto

            // Efecto Hover Pro: El color se aclara un poco al pasar el mouse
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(Math.Min(255, colorFondo.R + 20),
                                                                      Math.Min(255, colorFondo.G + 20),
                                                                      Math.Min(255, colorFondo.B + 20));
            btn.MouseLeave += (s, e) => btn.BackColor = colorFondo;
        }

        // --- Eventos (Se mantienen iguales para no romper la lógica) ---
        private void Agregar_Cliente(object sender, EventArgs e)
        {
            Agregar_cliente frm = new Agregar_cliente();
            if (frm.ShowDialog() == DialogResult.OK) CargarClientes();
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona un cliente de la lista.");
                return;
            }

            DataGridViewRow fila = dgvClientes.CurrentRow;
            Agregar_cliente frm = new Agregar_cliente();

            // Rellenar los campos con los datos de la fila seleccionada
            frm.txtDNI.Text = fila.Cells["DNI"].Value?.ToString();
            frm.txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
            frm.txtApellidos.Text = fila.Cells["Apellidos"].Value?.ToString();
            frm.txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();
            frm.txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString();
            frm.txtCP.Text = fila.Cells["CP"].Value?.ToString();
            frm.txtLocalidad.Text = fila.Cells["Localidad"].Value?.ToString();
            frm.txtProvincia.Text = fila.Cells["Provincia"].Value?.ToString();
            frm.txtEmail.Text = fila.Cells["Email"].Value?.ToString();
            frm.txtObservaciones.Text = fila.Cells["Observaciones"].Value?.ToString();

            // Bloquear el DNI para que no se pueda cambiar en modo edición
            frm.txtDNI.ReadOnly = true;

            if (frm.ShowDialog() == DialogResult.OK)
                CargarClientes();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                if (MessageBox.Show("¿Estás seguro de que deseas eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conexion.Abrir();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Clientes WHERE ClienteID = @id", conexion.leer);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        CargarClientes();
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                    finally { conexion.Cerrar(); }
                }
            }
        }
    }
}