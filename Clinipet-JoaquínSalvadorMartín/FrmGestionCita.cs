using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmGestionCita : Form
    {
        private int? citaIdEdicion = null;

        // Constructor para Nueva Cita
        public FrmGestionCita()
        {
            InitializeComponent();
            citaIdEdicion = null;
        }

        // Constructor para Modificar Cita
        public FrmGestionCita(int idCita)
        {
            InitializeComponent();
            this.citaIdEdicion = idCita;
        }

        private void FrmGestionCita_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Cargar datos
                this.mascotasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Mascotas);
                this.clientesTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Clientes);

                // 2. Aplicar Diseño CliniPet
                AplicarEstilosCliniPet();

                // 3. Configuración del ComboBox
                ElegirMascota.DataSource = this._Clinipet_JoaquinSMDataSet.Mascotas;
                ElegirMascota.DisplayMember = "Nombre";
                ElegirMascota.ValueMember = "MascotaID";

                if (citaIdEdicion.HasValue)
                {
                    label1.Text = "Editar Cita Veterinaria";
                    this.citasTableAdapter.Fill(this._Clinipet_JoaquinSMDataSet.Citas);
                    var cita = this._Clinipet_JoaquinSMDataSet.Citas.FindByCitaID(citaIdEdicion.Value);

                    if (cita != null)
                    {
                        ElegirMascota.SelectedValue = cita.MascotaID;
                        fechaCita.Value = cita.FechaHora;
                        Motivo.Text = cita.Motivo;
                        Observacion.Text = cita.IsObservacionesNull() ? "" : cita.Observaciones;
                    }
                    ConfigurarBoton(GuardarYModificar, Color.FromArgb(9, 132, 227), "Actualizar Cita");
                }
                else
                {
                    label1.Text = "Programar Nueva Cita";
                    ElegirMascota.SelectedIndex = -1;
                    NombreDueño.Text = "Seleccione una mascota...";
                    ConfigurarBoton(GuardarYModificar, Color.FromArgb(0, 184, 148), "Guardar Cita");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar: " + ex.Message);
            }
        }

        private void AplicarEstilosCliniPet()
        {
            // Fondo y fuente general
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);

            // Estilo del título principal (label1)
            label1.Font = new Font("Segoe UI Semibold", 16F);
            label1.ForeColor = Color.FromArgb(45, 52, 54);

            // Estilo de la etiqueta del dueño (NombreDueño)
            NombreDueño.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Italic);
            NombreDueño.ForeColor = Color.FromArgb(9, 132, 227);

            // Botón Cancelar/Cerrar (si tienes uno llamado 'btnCancelar', si no, ignora esta línea)
            // ConfigurarBoton(btnCancelar, Color.FromArgb(108, 117, 125), "Cancelar");

            // Estilo para los TextBox y ComboBox (Bordes simples)
            Motivo.BorderStyle = BorderStyle.FixedSingle;
            Observacion.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ConfigurarBoton(Button btn, Color colorFondo, string texto)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = colorFondo;
            btn.ForeColor = Color.White;
            btn.Text = texto;
            btn.Font = new Font("Segoe UI Bold", 10F);
            btn.Cursor = Cursors.Hand;
            btn.Height = 40;
        }

        private void ElegirMascota_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ElegirMascota.SelectedValue != null && ElegirMascota.SelectedIndex != -1)
            {
                try
                {
                    DataRowView mascotaFila = (DataRowView)ElegirMascota.SelectedItem;
                    int clienteId = Convert.ToInt32(mascotaFila["ClienteID"]);
                    var cliente = this._Clinipet_JoaquinSMDataSet.Clientes.FindByClienteID(clienteId);
                    NombreDueño.Text = (cliente != null) ? $"👤 Dueño: {cliente.Nombre}" : "Dueño no encontrado";
                }
                catch { }
            }
        }

        private void GuardarYModificar_Click(object sender, EventArgs e)
        {
            if (ElegirMascota.SelectedIndex == -1 || string.IsNullOrWhiteSpace(Motivo.Text))
            {
                MessageBox.Show("Por favor, rellena los campos obligatorios (Mascota y Motivo).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRowView mascotaFila = (DataRowView)ElegirMascota.SelectedItem;
                int idMascota = Convert.ToInt32(ElegirMascota.SelectedValue);
                int idCliente = Convert.ToInt32(mascotaFila["ClienteID"]);

                if (citaIdEdicion.HasValue)
                {
                    this.citasTableAdapter.UpdateQuery(
                        idMascota,
                        fechaCita.Value,
                        Motivo.Text,
                        Observacion.Text,
                        idCliente,
                        citaIdEdicion.Value
                    );
                    MessageBox.Show("¡Cita actualizada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.citasTableAdapter.Insert(
                        idMascota,
                        fechaCita.Value,
                        Motivo.Text,
                        Observacion.Text,
                        idCliente
                    );
                    MessageBox.Show("¡Cita guardada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}