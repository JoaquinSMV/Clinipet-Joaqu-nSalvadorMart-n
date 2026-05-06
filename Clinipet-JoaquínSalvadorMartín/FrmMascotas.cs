using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmMascotas : Form
    {
        private string dniSeleccionado = "";
        private bool formCargado = false;

        // ─── Paleta ───────────────────────────────────────────
        private readonly Color ColorFondo = Color.FromArgb(240, 242, 245);
        private readonly Color ColorVerde = Color.FromArgb(0, 184, 148);
        private readonly Color ColorAzul = Color.FromArgb(9, 132, 227);
        private readonly Color ColorRojo = Color.FromArgb(255, 118, 117);
        private readonly Color ColorGris = Color.FromArgb(108, 117, 125);

        // ─── Constructores ────────────────────────────────────
        public FrmMascotas()
        {
            InitializeComponent();
        }

        public FrmMascotas(string filtroDni) : this()
        {
            this.dniSeleccionado = filtroDni;
        }

        // ─── Carga ────────────────────────────────────────────
        private void FrmMascotas_Load(object sender, EventArgs e)
        {
            try
            {
                this.clientesTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Clientes);
                this.mascotasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Mascotas);

                AplicarEstilos();
                ConfigurarColumnas();
                ConfigurarComboBox();
                AplicarFiltroInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                formCargado = true;
            }
        }

        private void ConfigurarComboBox()
        {
            dNIComboBox.DataSource = this._Clinipet_JoaquinSMDataSet.Clientes;
            dNIComboBox.DisplayMember = "DNI";
            dNIComboBox.ValueMember = "ClienteID";
            dNIComboBox.SelectedIndex = -1;
            dNIComboBox.Font = new Font("Segoe UI", 10F);
        }

        private void AplicarFiltroInicial()
        {
            if (string.IsNullOrEmpty(dniSeleccionado))
            {
                this.mascotasBindingSource.RemoveFilter();
                dNIComboBox.SelectedIndex = -1;
                dNIComboBox.Text = "";
            }
            else
            {
                int indice = dNIComboBox.FindStringExact(dniSeleccionado);
                if (indice != -1)
                {
                    dNIComboBox.SelectedIndex = indice;
                    this.mascotasBindingSource.Filter = $"ClienteID = {dNIComboBox.SelectedValue}";
                }
            }
        }

        // ─── Filtros ──────────────────────────────────────────
        private void dNIComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formCargado || !dNIComboBox.Focused) return;

            try
            {
                if (dNIComboBox.SelectedIndex == -1 || dNIComboBox.SelectedValue == null) return;

                object valor = dNIComboBox.SelectedValue;
                string filtroId = valor is DataRowView fila
                    ? fila["ClienteID"].ToString()
                    : valor.ToString();

                this.mascotasBindingSource.Filter = $"ClienteID = {filtroId}";
                ActualizarContador();
            }
            catch
            {
                this.mascotasBindingSource.RemoveFilter();
            }
        }

        private void BotonQuitarFiltros_Click(object sender, EventArgs e)
        {
            formCargado = false;
            dNIComboBox.SelectedIndex = -1;
            dNIComboBox.Text = "";
            this.mascotasBindingSource.RemoveFilter();
            formCargado = true;
            ActualizarContador();
            mascotasDataGridView.Refresh();
        }

        // ─── CRUD ─────────────────────────────────────────────
        private void Añadir_Click(object sender, EventArgs e)
        {
            string dniActual = dNIComboBox.SelectedIndex != -1 ? dNIComboBox.Text : "";
            var frm = new Agregar_mascota(dniActual);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.mascotasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Mascotas);
                ActualizarContador();
            }
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            if (mascotasBindingSource.Current == null)
            {
                MessageBox.Show("Seleccione una mascota para modificar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRowView fila = (DataRowView)mascotasBindingSource.Current;
            var frm = new Agregar_mascota(dNIComboBox.Text);
            frm.PrepararEdicion(fila);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.mascotasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Mascotas);
                ActualizarContador();
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (mascotasBindingSource.Current == null)
            {
                MessageBox.Show("Seleccione una mascota para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRowView fila = (DataRowView)mascotasBindingSource.Current;
            int mascotaId = Convert.ToInt32(fila["MascotaID"]);
            string nombreMascota = fila["Nombre"].ToString();

            // Comprobar si tiene citas asociadas
            int numCitas = ContarCitasDeMascota(mascotaId);
            string mensaje = numCitas > 0
                ? $"La mascota '{nombreMascota}' tiene {numCitas} cita(s) registrada(s).\n\n" +
                  "Se eliminarán también todas sus citas.\n¿Desea continuar?"
                : $"¿Eliminar la mascota '{nombreMascota}'?";

            if (MessageBox.Show(mensaje, "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            ConexionBD con = new ConexionBD();
            try
            {
                con.Abrir();

                // 1. Borrar citas asociadas
                EjecutarComando("DELETE FROM Citas WHERE MascotaID = @id", mascotaId, con);

                // 2. Borrar mascota
                EjecutarComando("DELETE FROM Mascotas WHERE MascotaID = @id", mascotaId, con);

                MessageBox.Show($"'{nombreMascota}' eliminada correctamente.", "CliniPet",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.mascotasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Mascotas);
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Cerrar();
            }
        }

        // ─── Helpers BD ───────────────────────────────────────
        private int ContarCitasDeMascota(int mascotaId)
        {
            ConexionBD con = new ConexionBD();
            try
            {
                con.Abrir();
                var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Citas WHERE MascotaID = @id", con.leer);
                cmd.Parameters.AddWithValue("@id", mascotaId);
                return (int)cmd.ExecuteScalar();
            }
            catch { return 0; }
            finally { con.Cerrar(); }
        }

        private void EjecutarComando(string sql, int id, ConexionBD con)
        {
            var cmd = new SqlCommand(sql, con.leer);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // ─── UI ───────────────────────────────────────────────
        private void ActualizarContador()
        {
            int total = mascotasBindingSource.Count;
            this.Text = $"Mascotas ({total} registros)";
        }

        private void AplicarEstilos()
        {
            this.BackColor = ColorFondo;
            this.Padding = new Padding(35);

            EstilarGrid(mascotasDataGridView);

            DiseñarBoton(Añadir, ColorVerde, "  ✚  Añadir Mascota");
            DiseñarBoton(Modificar, ColorAzul, "  ✎  Modificar");
            DiseñarBoton(Eliminar, ColorRojo, "  🗑  Eliminar");
            DiseñarBoton(BotonQuitarFiltros, ColorGris, "  ✕  Limpiar");

            BotonQuitarFiltros.Size = new Size(130, 45);
            Modificar.Left = Añadir.Right + 15;
            Eliminar.Left = Modificar.Right + 15;
        }

        private void EstilarGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 232, 235);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Cabeceras
            dgv.ColumnHeadersHeight = 44;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 95, 110);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 249, 250);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);

            // Filas
            dgv.RowTemplate.Height = 48;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(50, 60, 70);
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 245, 240);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 130, 110);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 253, 254);

            // Hover
            int filaAnterior = -1;
            dgv.CellMouseEnter += (s, ev) => {
                if (ev.RowIndex != filaAnterior && ev.RowIndex >= 0)
                {
                    if (filaAnterior >= 0 && filaAnterior < dgv.Rows.Count)
                        dgv.Rows[filaAnterior].DefaultCellStyle.BackColor = Color.Empty;
                    dgv.Rows[ev.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(240, 248, 245);
                    filaAnterior = ev.RowIndex;
                }
            };
        }

        private void DiseñarBoton(Button btn, Color colorFondo, string texto)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = colorFondo;
            btn.ForeColor = Color.White;
            btn.Text = texto;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Font = new Font("Segoe UI Semibold", 10.5F);
            btn.Cursor = Cursors.Hand;
            btn.Size = new Size(180, 45);

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(
                Math.Min(255, colorFondo.R + 20),
                Math.Min(255, colorFondo.G + 20),
                Math.Min(255, colorFondo.B + 20));
            btn.MouseLeave += (s, e) => btn.BackColor = colorFondo;
        }

        private void ConfigurarColumnas()
        {
            if (mascotasDataGridView.Columns.Contains("dataGridViewTextBoxColumn1"))
                mascotasDataGridView.Columns["dataGridViewTextBoxColumn1"].Visible = false;
            if (mascotasDataGridView.Columns.Contains("dataGridViewTextBoxColumn2"))
                mascotasDataGridView.Columns["dataGridViewTextBoxColumn2"].Visible = false;

            foreach (DataGridViewColumn col in mascotasDataGridView.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (mascotasDataGridView.Columns.Contains("dataGridViewTextBoxColumn8"))
            {
                mascotasDataGridView.Columns["dataGridViewTextBoxColumn8"].FillWeight = 150;
                mascotasDataGridView.Columns["dataGridViewTextBoxColumn8"].HeaderText = "Observaciones";
            }
        }
    }
}