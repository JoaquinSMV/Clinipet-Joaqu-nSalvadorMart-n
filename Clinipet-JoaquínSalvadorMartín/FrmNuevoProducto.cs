using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public class FrmNuevoProducto : Form
    {
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private TextBox txtCantidad;
        private TextBox txtCantidadMinima;
        private TextBox txtPrecio;
        private DateTimePicker dtpVencimiento;
        private TextBox txtProveedor;
        private CheckBox chkActivo;
        private Button btnGuardar;
        private Button btnCancelar;

        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int CantidadMinima { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Proveedor { get; set; }
        public bool Activo { get; set; }

        public FrmNuevoProducto()
        {
            this.Text = "Nuevo Producto";
            this.Width = 550;
            this.Height = 530;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Load += FrmNuevoProducto_Load;

            CrearControles();
        }

        private void FrmNuevoProducto_Load(object sender, EventArgs e)
        {
            // Pre-llenar los datos si existen
            if (!string.IsNullOrEmpty(NombreProducto))
                txtNombre.Text = NombreProducto;
            if (!string.IsNullOrEmpty(Descripcion))
                txtDescripcion.Text = Descripcion;
            if (Cantidad > 0)
                txtCantidad.Text = Cantidad.ToString();
            if (CantidadMinima > 0)
                txtCantidadMinima.Text = CantidadMinima.ToString();
            if (Precio > 0)
                txtPrecio.Text = Precio.ToString("F2");
            if (FechaVencimiento != DateTime.MinValue)
                dtpVencimiento.Value = FechaVencimiento;
            if (!string.IsNullOrEmpty(Proveedor))
                txtProveedor.Text = Proveedor;
            chkActivo.Checked = Activo;
        }

        private void CrearControles()
        {
            int y = 20;
            const int margenIzq = 20;
            const int anchoControl = 480;
            const int espaciado = 50;

            // Nombre
            Label lblNombre = new Label { Text = "Nombre del Producto:", Location = new Point(margenIzq, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            this.Controls.Add(lblNombre);
            txtNombre = new TextBox { Location = new Point(margenIzq, y + 25), Width = anchoControl, Height = 30, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtNombre);
            y += espaciado;

            // Descripción
            Label lblDesc = new Label { Text = "Descripción:", Location = new Point(margenIzq, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            this.Controls.Add(lblDesc);
            txtDescripcion = new TextBox { Location = new Point(margenIzq, y + 25), Width = anchoControl, Height = 70, Multiline = true, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtDescripcion);
            y += 100;

            // Cantidad
            Label lblCant = new Label { Text = "Cantidad en Stock:", Location = new Point(margenIzq, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            this.Controls.Add(lblCant);
            txtCantidad = new TextBox { Location = new Point(margenIzq, y + 25), Width = 220, Height = 30, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtCantidad);

            // Cantidad Mínima
            Label lblCantMin = new Label { Text = "Cantidad Mínima:", Location = new Point(280, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            this.Controls.Add(lblCantMin);
            txtCantidadMinima = new TextBox { Location = new Point(280, y + 25), Width = 220, Height = 30, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtCantidadMinima);
            y += espaciado;

            // Precio
            Label lblPrecio = new Label { Text = "Precio Unitario (€):", Location = new Point(margenIzq, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            this.Controls.Add(lblPrecio);
            txtPrecio = new TextBox { Location = new Point(margenIzq, y + 25), Width = 220, Height = 30, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtPrecio);

            // Vencimiento
            Label lblVenc = new Label { Text = "Fecha de Vencimiento:", Location = new Point(280, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            this.Controls.Add(lblVenc);
            dtpVencimiento = new DateTimePicker { Location = new Point(280, y + 25), Width = 220, Height = 30, Format = DateTimePickerFormat.Short };
            this.Controls.Add(dtpVencimiento);
            y += espaciado;

            // Proveedor
            Label lblProv = new Label { Text = "Proveedor:", Location = new Point(margenIzq, y), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            this.Controls.Add(lblProv);
            txtProveedor = new TextBox { Location = new Point(margenIzq, y + 25), Width = anchoControl, Height = 30, Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtProveedor);
            y += espaciado;

            // Activo
            chkActivo = new CheckBox
            {
                Text = "✓ Producto Activo",
                Location = new Point(margenIzq, y),
                Width = 200,
                Height = 25,
                Checked = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(45, 52, 54)
            };
            this.Controls.Add(chkActivo);
            y += 40;

            // Botones
            btnGuardar = new Button
            {
                Text = "✓ Guardar",
                Location = new Point(120, y),
                Width = 180,
                Height = 40,
                BackColor = Color.FromArgb(0, 184, 148),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 11F),
                Cursor = Cursors.Hand
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;
            this.Controls.Add(btnGuardar);

            btnCancelar = new Button
            {
                Text = "✕ Cancelar",
                Location = new Point(310, y),
                Width = 180,
                Height = 40,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 11F),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancelar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad))
            {
                MessageBox.Show("La cantidad debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCantidadMinima.Text, out int cantidadMinima))
            {
                MessageBox.Show("La cantidad mínima debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NombreProducto = txtNombre.Text;
            Descripcion = txtDescripcion.Text;
            Cantidad = cantidad;
            CantidadMinima = cantidadMinima;
            Precio = precio;
            FechaVencimiento = dtpVencimiento.Value;
            Proveedor = txtProveedor.Text;
            Activo = chkActivo.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
