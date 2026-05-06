using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmCitas : Form
    {
        private bool cargando = true;

        private GestorPDF gestorPDF;

        public FrmCitas()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            gestorPDF = new GestorPDF();
            AplicarEstilosModernos();
            AñadirBotonPDF();
        }

        private void AñadirBotonPDF()
        {
            Button btnPDF = new Button();
            DiseñarBoton(btnPDF, Color.FromArgb(231, 76, 60), "  📄  Generar PDF");
            btnPDF.Location = new Point(EliminarCita.Right + 15, 45);
            btnPDF.Click += (s, e) => {
                if (dgvCitas.CurrentRow != null) {
                    DataRowView row = (DataRowView)dgvCitas.CurrentRow.DataBoundItem;
                    int citaId = Convert.ToInt32(row["CitaID"]);
                    string mascota = row["MascotaID"].ToString();
                    string fecha = row["FechaHora"].ToString();
                    string motivo = row["Motivo"].ToString();
                    
                    string path = gestorPDF.GenerarReporteCita(citaId, mascota, fecha, motivo, "Diagnóstico General", "Tratamiento sugerido");
                    MessageBox.Show("PDF generado con éxito en: " + path);
                } else {
                    MessageBox.Show("Por favor, selecciona una cita primero.");
                }
            };
            this.Controls.Add(btnPDF);
        }

        private void AplicarEstilosModernos()
        {
            // --- Configuración del Formulario (idéntica a FrmClientes) ---
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Padding = new Padding(35);

            // --- Estilo del DataGridView ---
            dgvCitas.BackgroundColor = Color.White;
            dgvCitas.BorderStyle = BorderStyle.None;
            dgvCitas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCitas.GridColor = Color.FromArgb(230, 230, 230);
            dgvCitas.RowHeadersVisible = false;
            dgvCitas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCitas.AllowUserToResizeRows = false;
            dgvCitas.EnableHeadersVisualStyles = false;
            dgvCitas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCitas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Cabeceras
            dgvCitas.ColumnHeadersHeight = 45;
            dgvCitas.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvCitas.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 110, 120);
            dgvCitas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
            dgvCitas.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;

            // Filas
            dgvCitas.RowTemplate.Height = 50;
            dgvCitas.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvCitas.DefaultCellStyle.ForeColor = Color.FromArgb(70, 80, 90);
            dgvCitas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 247, 245);
            dgvCitas.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 150, 136);
            dgvCitas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 253);

            // --- Botones (idéntico a FrmClientes) ---
            DiseñarBoton(NuevaCita, Color.FromArgb(0, 184, 148), "  ✚  Nueva Cita");
            DiseñarBoton(ModificarCita, Color.FromArgb(9, 132, 227), "  ✎  Modificar");
            DiseñarBoton(EliminarCita, Color.FromArgb(255, 118, 117), "  🗑  Eliminar");
            DiseñarBoton(QuitarFiltros, Color.FromArgb(108, 117, 125), "  ✕  Limpiar Filtros");

            // Posicionamiento manual de botones
            ModificarCita.Left = NuevaCita.Right + 15;
            EliminarCita.Left = ModificarCita.Right + 15;

            // Configurar filtros después de aplicar estilos
            ConfigurarFiltros();
        }

        private void DiseñarBoton(Button btn, Color colorFondo, string textoConIcono)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = colorFondo;
            btn.ForeColor = Color.White;
            btn.Text = textoConIcono;
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

        private void ConfigurarFiltros()
        {
            Font comboFont = new Font("Segoe UI", 10F);

            cmbFiltrarCliente.DataSource = this._Clinipet_JoaquinSMDataSet.Clientes;
            cmbFiltrarCliente.DisplayMember = "Nombre";
            cmbFiltrarCliente.ValueMember = "ClienteID";
            cmbFiltrarCliente.SelectedIndex = -1;
            cmbFiltrarCliente.Font = comboFont;
            cmbFiltrarCliente.BackColor = Color.White;
            cmbFiltrarCliente.FlatStyle = FlatStyle.Flat;

            cmbFiltrarMascota.DataSource = this._Clinipet_JoaquinSMDataSet.Mascotas;
            cmbFiltrarMascota.DisplayMember = "Nombre";
            cmbFiltrarMascota.ValueMember = "MascotaID";
            cmbFiltrarMascota.SelectedIndex = -1;
            cmbFiltrarMascota.Font = comboFont;
            cmbFiltrarMascota.BackColor = Color.White;
            cmbFiltrarMascota.FlatStyle = FlatStyle.Flat;

            this.cmbFiltrarCliente.SelectedIndexChanged += new EventHandler(Filtros_Changed);
            this.cmbFiltrarMascota.SelectedIndexChanged += new EventHandler(Filtros_Changed);
        }

        private void FrmCitas_Load(object sender, EventArgs e)
        {
            try
            {
                cargando = true;

                // Cargar datos
                this.clientesTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Clientes);
                this.mascotasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Mascotas);
                this.citasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Citas);

                cargando = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Filtros_Changed(object sender, EventArgs e)
        {
            if (cargando) return;

            string filtro = "";

            if (cmbFiltrarCliente.SelectedIndex != -1 && cmbFiltrarCliente.SelectedValue != null)
                filtro += $"ClienteID = {cmbFiltrarCliente.SelectedValue}";

            if (cmbFiltrarMascota.SelectedIndex != -1 && cmbFiltrarMascota.SelectedValue != null)
            {
                if (filtro.Length > 0) filtro += " AND ";
                filtro += $"MascotaID = {cmbFiltrarMascota.SelectedValue}";
            }

            citasBindingSource.Filter = filtro;
        }

        private void QuitarFiltros_Click(object sender, EventArgs e)
        {
            cargando = true;
            cmbFiltrarCliente.SelectedIndex = -1;
            cmbFiltrarMascota.SelectedIndex = -1;
            citasBindingSource.RemoveFilter();
            cargando = false;
        }

        private void NuevaCita_Click(object sender, EventArgs e)
        {
            FrmGestionCita frm = new FrmGestionCita();
            if (frm.ShowDialog() == DialogResult.OK)
                this.citasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Citas);
        }

        private void ModificarCita_Click(object sender, EventArgs e)
        {
            if (citasBindingSource.Current != null)
            {
                DataRowView citaSeleccionada = (DataRowView)citasBindingSource.Current;
                int idCita = Convert.ToInt32(citaSeleccionada["CitaID"]);

                FrmGestionCita frm = new FrmGestionCita(idCita);
                if (frm.ShowDialog() == DialogResult.OK)
                    this.citasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Citas);
            }
            else
            {
                MessageBox.Show("Seleccione una cita para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EliminarCita_Click(object sender, EventArgs e)
        {
            if (citasBindingSource.Current != null)
            {
                if (MessageBox.Show("¿Seguro que desea eliminar esta cita?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        citasBindingSource.RemoveCurrent();
                        this.tableAdapterManager.UpdateAll(this._Clinipet_JoaquinSMDataSet);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar la cita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.citasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Citas);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una cita para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvCitas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}