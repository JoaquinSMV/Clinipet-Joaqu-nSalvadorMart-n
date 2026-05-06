using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class Agregar_mascota : Form
    {
        ConexionBD con = new ConexionBD();

        public Agregar_mascota(string dniInicial = "")
        {
            InitializeComponent();

            CargarClientesEnCombo();
            AplicarEstilosCliniPet();
            ConfigurarFormulario();
            OcultarErrores();
            AsignarEventosLimpieza();

            CBXDniDueño.Enabled = true;

            if (!string.IsNullOrEmpty(dniInicial))
                CBXDniDueño.Text = dniInicial;

            this.GuardarYModificar.Click -= new System.EventHandler(this.GuardarYModificar_Click);
            this.GuardarYModificar.Click += new System.EventHandler(this.GuardarYModificar_Click);
        }

        private void AplicarEstilosCliniPet()
        {
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);

            label6.Font = new Font("Segoe UI Semibold", 16F);
            label6.ForeColor = Color.FromArgb(45, 52, 54);
            label6.Text = "Ficha de Mascota";

            ConfigurarBoton(GuardarYModificar, Color.FromArgb(0, 184, 148), "Guardar Mascota");

            txtNombreMascota.BorderStyle = BorderStyle.FixedSingle;
            txtRaza.BorderStyle = BorderStyle.FixedSingle;
            txtNotaEspecial.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ConfigurarBoton(Button btn, Color colorFondo, string texto)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = colorFondo;
            btn.ForeColor = Color.White;
            btn.Text = texto;
            btn.Font = new Font("Segoe UI Bold", 10F);
            btn.Cursor = Cursors.Hand;
            btn.Height = 40;
        }

        private void OcultarErrores()
        {
            error1.Visible = error2.Visible = error3.Visible =
            error5.Visible = error6.Visible = error7.Visible =
            label7.Visible = false;
        }

        private void AsignarEventosLimpieza()
        {
            txtNombreMascota.TextChanged += (s, e) => error1.Visible = false;
            cbEspecie.SelectedIndexChanged += (s, e) => error2.Visible = false;
            cbEspecie.TextChanged += (s, e) => error2.Visible = false;
            cbSexo.SelectedIndexChanged += (s, e) => error3.Visible = false;
            dtpFecha.ValueChanged += (s, e) => error5.Visible = false;
            CBXDniDueño.SelectedIndexChanged += (s, e) => error6.Visible = false;
            txtRaza.TextChanged += (s, e) => error7.Visible = false;
            txtNotaEspecial.TextChanged += (s, e) => label7.Visible = false;
        }

        private void CargarClientesEnCombo()
        {
            try
            {
                con.Abrir();
                string sql = "SELECT ClienteID, DNI FROM Clientes ORDER BY DNI ASC";
                SqlDataAdapter da = new SqlDataAdapter(sql, con.leer);
                DataTable dtClientes = new DataTable();
                da.Fill(dtClientes);

                CBXDniDueño.DataSource = dtClientes;
                CBXDniDueño.DisplayMember = "DNI";
                CBXDniDueño.ValueMember = "ClienteID";
                CBXDniDueño.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message,
                                "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Cerrar(); }
        }

        private void ConfigurarFormulario()
        {
            if (cbEspecie.Items.Count == 0)
                cbEspecie.Items.AddRange(new string[] { "Perro", "Gato", "Hámster", "Ave", "Reptil", "Otros" });

            if (cbSexo.Items.Count == 0)
                cbSexo.Items.AddRange(new string[] { "Macho", "Hembra" });
        }

        public void PrepararEdicion(DataRowView mascota)
        {
            label6.Text = "Modificar Mascota";
            ConfigurarBoton(GuardarYModificar, Color.FromArgb(9, 132, 227), "Modificar Datos");

            CBXDniDueño.Enabled = false;
            txtNombreMascota.ReadOnly = true;

            txtNombreMascota.Text = mascota["Nombre"].ToString();
            cbEspecie.Text = mascota["Especie"].ToString();

            string sexoBD = mascota["Sexo"].ToString();
            cbSexo.Text = (sexoBD == "M") ? "Macho" : "Hembra";

            dtpFecha.Value = mascota["FechaNacimiento"] != DBNull.Value
                             ? Convert.ToDateTime(mascota["FechaNacimiento"])
                             : DateTime.Now;

            txtRaza.Text = mascota["Raza"].ToString();
            txtNotaEspecial.Text = mascota["NotasEspeciales"].ToString();

            if (mascota["ClienteID"] != DBNull.Value)
                CBXDniDueño.SelectedValue = mascota["ClienteID"];
        }

        private void GuardarYModificar_Click(object sender, EventArgs e)
        {
            OcultarErrores();
            bool hayError = false;

            // error1 · Nombre: obligatorio, solo letras y espacios
            if (string.IsNullOrWhiteSpace(txtNombreMascota.Text) ||
                !Regex.IsMatch(txtNombreMascota.Text.Trim(),
                               @"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]+$"))
            {
                error1.Text = "Solo letras";
                error1.Visible = true;
                hayError = true;
            }

            // error2 · Especie: obligatoria
            if (cbEspecie.SelectedIndex == -1 &&
                string.IsNullOrWhiteSpace(cbEspecie.Text))
            {
                error2.Text = "Indica la especie";
                error2.Visible = true;
                hayError = true;
            }

            // error3 · Sexo: obligatorio
            if (cbSexo.SelectedIndex == -1)
            {
                error3.Text = "Indica el sexo";
                error3.Visible = true;
                hayError = true;
            }

            // error5 · Fecha: no puede ser futura
            if (dtpFecha.Value.Date > DateTime.Today)
            {
                error5.Text = "No puede ser futura";
                error5.Visible = true;
                hayError = true;
            }

            // error6 · Dueño: obligatorio
            if (CBXDniDueño.SelectedIndex == -1)
            {
                error6.Text = "Selecciona el dueño";
                error6.Visible = true;
                hayError = true;
            }

            // error7 · Raza: opcional, solo letras si se rellena
            if (!string.IsNullOrWhiteSpace(txtRaza.Text) &&
                !Regex.IsMatch(txtRaza.Text.Trim(),
                               @"^[A-Za-záéíóúÁÉÍÓÚüÜñÑ\s]+$"))
            {
                error7.Text = "Solo letras";
                error7.Visible = true;
                hayError = true;
            }

            // label7 · Nota: opcional, máximo 200 caracteres
            if (txtNotaEspecial.Text.Length > 200)
            {
                label7.Text = "Máx. 200 caracteres";
                label7.Visible = true;
                hayError = true;
            }

            if (hayError) return;

            try
            {
                con.Abrir();

                int idDelDueño = Convert.ToInt32(CBXDniDueño.SelectedValue);
                string sexoBD = cbSexo.Text.StartsWith("M") ? "M" : "H";
                string sql;

                if (label6.Text.Contains("Modificar"))
                {
                    sql = "UPDATE Mascotas " +
                          "SET Especie=@esp, Sexo=@sex, FechaNacimiento=@fec, " +
                          "    Raza=@raza, NotasEspeciales=@not " +
                          "WHERE ClienteID=@cid AND Nombre=@nom";
                }
                else
                {
                    sql = "INSERT INTO Mascotas " +
                          "(ClienteID, Nombre, Especie, FechaNacimiento, Sexo, Raza, NotasEspeciales) " +
                          "VALUES (@cid, @nom, @esp, @fec, @sex, @raza, @not)";
                }

                SqlCommand cmd = new SqlCommand(sql, con.leer);
                cmd.Parameters.AddWithValue("@cid", idDelDueño);
                cmd.Parameters.AddWithValue("@nom", txtNombreMascota.Text.Trim());
                cmd.Parameters.AddWithValue("@esp", cbEspecie.Text.Trim());
                cmd.Parameters.AddWithValue("@fec", dtpFecha.Value.Date);
                cmd.Parameters.AddWithValue("@sex", sexoBD);
                cmd.Parameters.AddWithValue("@raza", string.IsNullOrWhiteSpace(txtRaza.Text)
                                                        ? (object)DBNull.Value
                                                        : txtRaza.Text.Trim());
                cmd.Parameters.AddWithValue("@not", string.IsNullOrWhiteSpace(txtNotaEspecial.Text)
                                                        ? (object)DBNull.Value
                                                        : txtNotaEspecial.Text.Trim());

                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                {
                    MessageBox.Show("¡Información actualizada con éxito!", "CliniPet",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se encontró el registro para actualizar.", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la operación: " + ex.Message, "Error BD",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { con.Cerrar(); }
        }
    }
}