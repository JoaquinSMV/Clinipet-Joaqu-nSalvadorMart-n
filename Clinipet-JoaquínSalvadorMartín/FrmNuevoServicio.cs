using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public class FrmNuevoServicio : Form
    {
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private TextBox txtPrecio;
        private ComboBox cmbCategoria;
        private Button btnGuardar;
        private Button btnCancelar;

        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }

        public FrmNuevoServicio()
        {
            this.Text = "Nuevo Servicio";
            this.Width = 450;
            this.Height = 350;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 242, 245);

            CrearControles();
        }

        private void CrearControles()
        {
            int y = 20;
            const int alturaControl = 30;
            const int espaciado = 40;

            // Nombre
            Label lblNombre = new Label { Text = "Nombre del Servicio:", Location = new Point(20, y), AutoSize = true };
            this.Controls.Add(lblNombre);
            txtNombre = new TextBox { Location = new Point(20, y + 25), Width = 400, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtNombre);
            y += espaciado;

            // Descripción
            Label lblDesc = new Label { Text = "Descripción:", Location = new Point(20, y), AutoSize = true };
            this.Controls.Add(lblDesc);
            txtDescripcion = new TextBox { Location = new Point(20, y + 25), Width = 400, Height = 60, Multiline = true, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtDescripcion);
            y += 90;

            // Categoría
            Label lblCat = new Label { Text = "Categoría:", Location = new Point(20, y), AutoSize = true };
            this.Controls.Add(lblCat);
            cmbCategoria = new ComboBox
            {
                Location = new Point(20, y + 25),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDown,
                Font = new Font("Segoe UI", 10F)
            };
            cmbCategoria.Items.AddRange(new[] { "Consulta", "Vacunación", "Odontología", "Estética", "Cirugía", "Análisis" });
            cmbCategoria.SelectedIndex = 0;
            this.Controls.Add(cmbCategoria);

            // Precio
            Label lblPrecio = new Label { Text = "Precio (€):", Location = new Point(240, y), AutoSize = true };
            this.Controls.Add(lblPrecio);
            txtPrecio = new TextBox { Location = new Point(240, y + 25), Width = 180, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtPrecio);
            y += espaciado;

            // Botones
            btnGuardar = new Button
            {
                Text = "✓ Guardar",
                Location = new Point(120, y + 20),
                Width = 150,
                Height = 35,
                BackColor = Color.FromArgb(0, 184, 148),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            btnCancelar = new Button
            {
                Text = "✕ Cancelar",
                Location = new Point(280, y + 20),
                Width = 150,
                Height = 35,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10F)
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancelar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del servicio es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NombreServicio = txtNombre.Text;
            Descripcion = txtDescripcion.Text;
            Precio = precio;
            Categoria = cmbCategoria.SelectedItem.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
