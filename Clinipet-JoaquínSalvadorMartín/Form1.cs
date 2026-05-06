using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class Form1 : Form
    {
        private Form formularioActivo = null;

        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new EventHandler(btnClientes_Click);
            this.button2.Click += new EventHandler(btnMascotas_Click);
            this.button3.Click += new EventHandler(btnCitas_Click);
            this.button4.Click += new EventHandler(btnInicio_Click);

            btnInicio_Click(null, null);
        }

        private void AbrirFormularioHijo(Form nuevoFormulario)
        {
            if (formularioActivo != null)
                formularioActivo.Close();

            formularioActivo = nuevoFormulario;
            nuevoFormulario.TopLevel = false;
            nuevoFormulario.FormBorderStyle = FormBorderStyle.None;
            nuevoFormulario.Dock = DockStyle.Fill;

            this.pnlContenedor.Controls.Clear();
            this.pnlContenedor.Controls.Add(nuevoFormulario);
            this.pnlContenedor.Tag = nuevoFormulario;
            nuevoFormulario.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmClientes());
        }

        private void btnMascotas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmMascotas(""));
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmCitas());
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            var panelUsuario = new Usuario();

            panelUsuario.NavegacionSolicitada += (destino) =>
            {
                switch (destino)
                {
                    case "clientes":
                        AbrirFormularioHijo(new FrmClientes());
                        break;
                    case "mascotas":
                        AbrirFormularioHijo(new FrmMascotas(""));
                        break;
                    case "citas":
                        AbrirFormularioHijo(new FrmCitas());
                        break;
                    case "nuevo":
                        var frmCliente = new Agregar_cliente();
                        if (frmCliente.ShowDialog() == DialogResult.OK)
                            AbrirFormularioHijo(new FrmClientes());
                        break;
                    case "nueva_cita":
                        var frmCita = new FrmGestionCita();
                        if (frmCita.ShowDialog() == DialogResult.OK)
                            AbrirFormularioHijo(new FrmCitas());
                        break;
                    case "inventario":
                        AbrirFormularioHijo(new FrmInventario());
                        break;
                    case "servicios":
                        AbrirFormularioHijo(new FrmServicios());
                        break;
                    case "reportes":
                        AbrirFormularioHijo(new FrmReportes());
                        break;
                }
            };

            AbrirFormularioHijo(panelUsuario);
        }
    }
}