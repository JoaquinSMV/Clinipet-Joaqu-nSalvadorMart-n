using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class Agregar_cliente : Form
    {
        ConexionBD con = new ConexionBD();

        public Agregar_cliente()
        {
            InitializeComponent();
            OcultarErrores();
            AsignarEventosLimpieza();
            //ReposicionarLabelsError();
        }

        // Mueve los labels de error a la derecha del TextBox para que no se superpongan
        //private void ReposicionarLabelsError()
        //{
        //    int xError = 365; // justo después del TextBox (120 + 240 + 5)

        //    error1.Left = xError; error1.Top = txtDNI.Top + 5;
        //    error2.Left = xError; error2.Top = txtNombre.Top + 5;
        //    error3.Left = xError; error3.Top = txtApellidos.Top + 5;
        //    error4.Left = xError; error4.Top = txtDireccion.Top + 5;
        //    error5.Left = xError; error5.Top = txtTelefono.Top + 5;
        //    error6.Left = xError; error6.Top = txtCP.Top + 5;
        //    error7.Left = xError; error7.Top = txtLocalidad.Top + 5;
        //    error8.Left = xError; error8.Top = txtProvincia.Top + 5;
        //    error9.Left = xError; error9.Top = txtEmail.Top + 5;
        //    error10.Left = xError; error10.Top = txtObservaciones.Top + 5;
        //}

        // Limpia el error del campo en cuanto el usuario empieza a escribir
        private void AsignarEventosLimpieza()
        {
            txtDNI.TextChanged += (s, e) => error1.Visible = false;
            txtNombre.TextChanged += (s, e) => error2.Visible = false;
            txtApellidos.TextChanged += (s, e) => error3.Visible = false;
            txtDireccion.TextChanged += (s, e) => error4.Visible = false;
            txtTelefono.TextChanged += (s, e) => error5.Visible = false;
            txtCP.TextChanged += (s, e) => error6.Visible = false;
            txtLocalidad.TextChanged += (s, e) => error7.Visible = false;
            txtProvincia.TextChanged += (s, e) => error8.Visible = false;
            txtEmail.TextChanged += (s, e) => error9.Visible = false;
            txtObservaciones.TextChanged += (s, e) => error10.Visible = false;
        }

        private void OcultarErrores()
        {
            error1.Visible = error2.Visible = error3.Visible = error4.Visible = error5.Visible =
            error6.Visible = error7.Visible = error8.Visible = error9.Visible = error10.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OcultarErrores();
            bool hayError = false;

            // ── DNI: 8 dígitos + 1 letra (formato español) ───────────────
            if (!Regex.IsMatch(txtDNI.Text.Trim(), @"^\d{8}[A-Za-z]$"))
            {
                error1.Text = "DNI inválido (ej: 12345678A)";
                error1.Visible = true;
                hayError = true;
            }

            // ── Nombre: obligatorio y sin dígitos ────────────────────────
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                Regex.IsMatch(txtNombre.Text, @"\d"))
            {
                error2.Text = "Nombre inválido (sin números)";
                error2.Visible = true;
                hayError = true;
            }

            // ── Apellidos: obligatorio y sin dígitos ─────────────────────
            if (string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                Regex.IsMatch(txtApellidos.Text, @"\d"))
            {
                error3.Text = "Apellidos inválidos (sin números)";
                error3.Visible = true;
                hayError = true;
            }

            // ── Dirección: si se rellena, mínimo 5 caracteres ────────────
            if (!string.IsNullOrWhiteSpace(txtDireccion.Text) &&
                txtDireccion.Text.Trim().Length < 5)
            {
                error4.Text = "Dirección demasiado corta";
                error4.Visible = true;
                hayError = true;
            }

            // ── Teléfono: exactamente 9 dígitos ──────────────────────────
            if (!Regex.IsMatch(txtTelefono.Text.Trim(), @"^\d{9}$"))
            {
                error5.Text = "Teléfono: 9 dígitos exactos";
                error5.Visible = true;
                hayError = true;
            }

            // ── Código postal: exactamente 5 dígitos ─────────────────────
            if (!Regex.IsMatch(txtCP.Text.Trim(), @"^\d{5}$"))
            {
                error6.Text = "CP: 5 dígitos exactos";
                error6.Visible = true;
                hayError = true;
            }

            // ── Localidad: obligatoria y sin dígitos ─────────────────────
            if (string.IsNullOrWhiteSpace(txtLocalidad.Text) ||
                Regex.IsMatch(txtLocalidad.Text, @"\d"))
            {
                error7.Text = "Localidad inválida";
                error7.Visible = true;
                hayError = true;
            }

            // ── Provincia: obligatoria y sin dígitos ─────────────────────
            if (string.IsNullOrWhiteSpace(txtProvincia.Text) ||
                Regex.IsMatch(txtProvincia.Text, @"\d"))
            {
                error8.Text = "Provincia inválida";
                error8.Visible = true;
                hayError = true;
            }

            // ── Email: formato estándar ───────────────────────────────────
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                error9.Text = "Email no válido";
                error9.Visible = true;
                hayError = true;
            }

            if (hayError) return;

            // ── Guardado en BD ────────────────────────────────────────────
            try
            {
                con.Abrir();

                // Verificar si ya existe ese DNI
                SqlCommand cmdCheck = new SqlCommand(
                    "SELECT COUNT(*) FROM Clientes WHERE DNI = @dni", con.leer);
                cmdCheck.Parameters.AddWithValue("@dni", txtDNI.Text.Trim().ToUpper());
                int existe = (int)cmdCheck.ExecuteScalar();

                string sql;

                if (existe > 0)
                {
                    // Actualizar registro existente
                    sql = "UPDATE Clientes SET " +
                          "Nombre=@nom, Apellidos=@ape, Direccion=@dir, Telefono=@tel, " +
                          "CP=@cp, Localidad=@loc, Provincia=@prov, Email=@mail, " +
                          "Observaciones=@obs WHERE DNI=@dni";
                }
                else
                {
                    // Verificar que el email no esté duplicado
                    SqlCommand cmdEmail = new SqlCommand(
                        "SELECT COUNT(*) FROM Clientes WHERE Email = @mail", con.leer);
                    cmdEmail.Parameters.AddWithValue("@mail", txtEmail.Text.Trim());
                    if ((int)cmdEmail.ExecuteScalar() > 0)
                    {
                        error9.Text = "Email ya registrado";
                        error9.Visible = true;
                        return;
                    }

                    sql = "INSERT INTO Clientes " +
                          "(DNI, Nombre, Apellidos, Direccion, Telefono, CP, " +
                          " Localidad, Provincia, Email, Observaciones) " +
                          "VALUES (@dni, @nom, @ape, @dir, @tel, @cp, @loc, @prov, @mail, @obs)";
                }

                SqlCommand cmd = new SqlCommand(sql, con.leer);
                cmd.Parameters.AddWithValue("@dni", txtDNI.Text.Trim().ToUpper());
                cmd.Parameters.AddWithValue("@nom", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@ape", txtApellidos.Text.Trim());
                cmd.Parameters.AddWithValue("@dir", string.IsNullOrWhiteSpace(txtDireccion.Text)
                                                        ? (object)DBNull.Value
                                                        : txtDireccion.Text.Trim());
                cmd.Parameters.AddWithValue("@tel", txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@cp", txtCP.Text.Trim());
                cmd.Parameters.AddWithValue("@loc", txtLocalidad.Text.Trim());
                cmd.Parameters.AddWithValue("@prov", txtProvincia.Text.Trim());
                cmd.Parameters.AddWithValue("@mail", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@obs", string.IsNullOrWhiteSpace(txtObservaciones.Text)
                                                        ? (object)DBNull.Value
                                                        : txtObservaciones.Text.Trim());
                cmd.ExecuteNonQuery();

                MessageBox.Show(existe > 0 ? "¡Cliente actualizado!" : "¡Cliente guardado!",
                                "Clinipet",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Cerrar();
            }
        }
    }
}