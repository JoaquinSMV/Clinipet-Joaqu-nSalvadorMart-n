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
            
            // Nuevos botones para mejoras
            this.btnInventario.Click += new EventHandler(btnInventario_Click);
            this.btnServicios.Click += new EventHandler(btnServicios_Click);
            this.btnEstadisticas.Click += new EventHandler(btnEstadisticas_Click);

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
            AbrirFormularioHijo(new FrmDashboard());
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmInventario());
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmServicios());
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
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
                        var frm = new Agregar_cliente();
                        if (frm.ShowDialog() == DialogResult.OK)
                            AbrirFormularioHijo(new FrmClientes());
                        break;
                }
            };

            AbrirFormularioHijo(panelUsuario);
        }
    }
}