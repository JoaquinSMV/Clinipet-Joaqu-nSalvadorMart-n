using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class FrmDashboard : Form
    {
        private GestorEstadisticas gestorEstadisticas;
        private GestorInventario gestorInventario;

        public FrmDashboard()
        {
            InitializeComponent();
            gestorEstadisticas = new GestorEstadisticas();
            gestorInventario = new GestorInventario();
            this.DoubleBuffered = true;
            AplicarEstilosModernos();
            CargarDatos();
        }

        private void AplicarEstilosModernos()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Padding = new Padding(35);
            this.Font = new Font("Segoe UI", 10F);
        }

        private void CargarDatos()
        {
            try
            {
                // Crear controles dinámicamente
                this.Controls.Clear();

                // Título
                Label lblTitulo = new Label
                {
                    Text = "📊 Dashboard - Resumen de la Clínica",
                    Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(41, 53, 65),
                    AutoSize = true,
                    Location = new Point(20, 20)
                };
                this.Controls.Add(lblTitulo);

                // Obtener estadísticas
                DataTable stats = gestorEstadisticas.ObtenerEstadisticasGenerales();
                if (stats != null && stats.Rows.Count > 0)
                {
                    DataRow row = stats.Rows[0];
                    
                    int totalClientes = Convert.ToInt32(row["total_clientes"]);
                    int totalMascotas = Convert.ToInt32(row["total_mascotas"]);
                    int totalCitas = Convert.ToInt32(row["total_citas"]);
                    int citasCompletadas = Convert.ToInt32(row["citas_completadas"]);
                    int citasPendientes = Convert.ToInt32(row["citas_pendientes"]);
                    int citasHoy = Convert.ToInt32(row["citas_hoy"]);

                    // Crear tarjetas de estadísticas
                    int posX = 20;
                    int posY = 80;
                    int cardWidth = 220;
                    int cardHeight = 120;
                    int spacing = 30;

                    // Tarjeta: Total Clientes
                    CrearTarjeta(posX, posY, cardWidth, cardHeight, "👥 Clientes", totalClientes.ToString(), Color.FromArgb(52, 152, 219));
                    posX += cardWidth + spacing;

                    // Tarjeta: Total Mascotas
                    CrearTarjeta(posX, posY, cardWidth, cardHeight, "🐾 Mascotas", totalMascotas.ToString(), Color.FromArgb(46, 204, 113));
                    posX += cardWidth + spacing;

                    // Tarjeta: Citas Hoy
                    CrearTarjeta(posX, posY, cardWidth, cardHeight, "📅 Citas Hoy", citasHoy.ToString(), Color.FromArgb(241, 196, 15));
                    posX += cardWidth + spacing;

                    // Tarjeta: Citas Pendientes
                    CrearTarjeta(posX, posY, cardWidth, cardHeight, "⏳ Pendientes", citasPendientes.ToString(), Color.FromArgb(230, 126, 34));

                    // Segunda fila de tarjetas
                    posX = 20;
                    posY = 230;

                    // Tarjeta: Citas Completadas
                    CrearTarjeta(posX, posY, cardWidth, cardHeight, "✅ Completadas", citasCompletadas.ToString(), Color.FromArgb(155, 89, 182));
                    posX += cardWidth + spacing;

                    // Tarjeta: Total Citas
                    CrearTarjeta(posX, posY, cardWidth, cardHeight, "📊 Total Citas", totalCitas.ToString(), Color.FromArgb(52, 73, 94));

                    // Medicamentos con stock bajo
                    DataTable medicamentosStockBajo = gestorInventario.ObtenerMedicamentosStockBajo();
                    if (medicamentosStockBajo != null && medicamentosStockBajo.Rows.Count > 0)
                    {
                        Label lblAlerta = new Label
                        {
                            Text = "⚠️ ALERTA: Medicamentos con Stock Bajo",
                            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                            ForeColor = Color.FromArgb(231, 76, 60),
                            AutoSize = true,
                            Location = new Point(20, 380)
                        };
                        this.Controls.Add(lblAlerta);

                        string medicamentosTexto = "";
                        foreach (DataRow medicamento in medicamentosStockBajo.Rows)
                        {
                            medicamentosTexto += $"• {medicamento["nombre_medicamento"]} (Stock: {medicamento["cantidad_stock"]})\n";
                        }

                        Label lblMedicamentos = new Label
                        {
                            Text = medicamentosTexto,
                            Font = new Font("Segoe UI", 10F),
                            ForeColor = Color.FromArgb(70, 80, 90),
                            AutoSize = true,
                            Location = new Point(40, 410),
                            MaximumSize = new Size(800, 200)
                        };
                        this.Controls.Add(lblMedicamentos);
                    }

                    // Botones de acción rápida
                    Button btnNuevaCita = new Button
                    {
                        Text = "➕ Nueva Cita",
                        Font = new Font("Segoe UI", 11F),
                        BackColor = Color.FromArgb(0, 150, 136),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(150, 45),
                        Location = new Point(20, 550)
                    };
                    btnNuevaCita.Click += (s, e) => MessageBox.Show("Ir a Nueva Cita");
                    this.Controls.Add(btnNuevaCita);

                    Button btnInventario = new Button
                    {
                        Text = "📦 Inventario",
                        Font = new Font("Segoe UI", 11F),
                        BackColor = Color.FromArgb(52, 152, 219),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(150, 45),
                        Location = new Point(190, 550)
                    };
                    btnInventario.Click += (s, e) => MessageBox.Show("Ir a Inventario");
                    this.Controls.Add(btnInventario);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar dashboard: " + ex.Message);
            }
        }

        private void CrearTarjeta(int x, int y, int width, int height, string titulo, string valor, Color color)
        {
            Panel panel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Padding = new Padding(15)
            };

            // Sombra simulada
            panel.Paint += (s, e) =>
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(200, 200, 200), 1), 0, 0, width - 1, height - 1);
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 11F),
                ForeColor = color,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            panel.Controls.Add(lblTitulo);

            Label lblValor = new Label
            {
                Text = valor,
                Font = new Font("Segoe UI", 28F, FontStyle.Bold),
                ForeColor = color,
                AutoSize = true,
                Location = new Point(10, 40)
            };
            panel.Controls.Add(lblValor);

            this.Controls.Add(panel);
        }
    }
}
