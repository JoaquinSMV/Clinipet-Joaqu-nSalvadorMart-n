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
            this.btnInventario.Click += new EventHandler(btnInventario_Click);
            this.btnServicios.Click += new EventHandler(btnServicios_Click);
            this.btnReportes.Click += new EventHandler(btnReportes_Click);

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
                        btnClientes_Click(null, null);
                        break;
                    case "mascotas":
                        btnMascotas_Click(null, null);
                        break;
                    case "citas":
                        btnCitas_Click(null, null);
                        break;
                    case "nuevo":
                        var frmCliente = new Agregar_cliente();
                        if (frmCliente.ShowDialog() == DialogResult.OK)
                            btnClientes_Click(null, null);
                        break;
                    case "nueva_cita":
                        var frmCita = new FrmGestionCita();
                        if (frmCita.ShowDialog() == DialogResult.OK)
                            btnCitas_Click(null, null);
                        break;
                    case "inventario":
                        btnInventario_Click(null, null);
                        break;
                    case "servicios":
                        btnServicios_Click(null, null);
                        break;
                    case "reportes":
                        btnReportes_Click(null, null);
                        break;
                }
            };

            AbrirFormularioHijo(panelUsuario);
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmInventario());
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmServicios());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmReportes());
        }
    }
}