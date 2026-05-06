using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public partial class Usuario : Form
    {
        ConexionBD con = new ConexionBD();

        public event Action<string> NavegacionSolicitada;

        private readonly Color ColorFondo = Color.FromArgb(240, 242, 245);
        private readonly Color ColorBlanco = Color.White;
        private readonly Color ColorTeal = Color.FromArgb(0, 184, 148);
        private readonly Color ColorTealOscuro = Color.FromArgb(0, 150, 120);
        private readonly Color ColorAzul = Color.FromArgb(9, 132, 227);
        private readonly Color ColorAmbar = Color.FromArgb(253, 176, 34);
        private readonly Color ColorTexto = Color.FromArgb(45, 52, 54);
        private readonly Color ColorTextoSuave = Color.FromArgb(120, 130, 140);
        private readonly Color ColorBorde = Color.FromArgb(225, 228, 232);

        private Timer timerActualizacion;

        public Usuario()
        {
            InitializeComponent();
            ConfigurarTimer();
        }

        private void ConfigurarTimer()
        {
            timerActualizacion = new Timer();
            timerActualizacion.Interval = 30000; // 30 segundos
            timerActualizacion.Tick += (s, e) => CargarEstadisticas();
            timerActualizacion.Start();
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            ConstruirUI();
            CargarEstadisticas();
        }

        private void ConstruirUI()
        {
            this.Controls.Clear();
            this.BackColor = ColorFondo;
            this.Padding = new Padding(0);

            // ── Cabecera ─────────────────────────────────────
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = ColorBlanco,
                Padding = new Padding(40, 0, 40, 0)
            };

            Panel lineaAccento = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 3,
                BackColor = ColorTeal
            };
            pnlHeader.Controls.Add(lineaAccento);

            Label lblTitulo = new Label
            {
                Text = "Panel de Control",
                Font = new Font("Segoe UI Semibold", 20F),
                ForeColor = ColorTexto,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblFecha = new Label
            {
                Text = "Hoy, " + DateTime.Now.ToString("dddd d 'de' MMMM 'de' yyyy",
                              new System.Globalization.CultureInfo("es-ES")),
                Font = new Font("Segoe UI", 10F),
                ForeColor = ColorTextoSuave,
                AutoSize = false,
                Dock = DockStyle.Right,
                Width = 320,
                TextAlign = ContentAlignment.MiddleRight
            };

            pnlHeader.Controls.Add(lblFecha);
            pnlHeader.Controls.Add(lblTitulo);
            this.Controls.Add(pnlHeader);

            // ── Contenedor principal scrollable ───────────────
            Panel pnlScroll = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = ColorFondo,
                Padding = new Padding(40, 30, 40, 30)
            };
            this.Controls.Add(pnlScroll);

            // ── Fila de tarjetas ──────────────────────────────
            TableLayoutPanel filaTarjetas = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 1,
                Dock = DockStyle.Top,
                Height = 140,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 24)
            };
            filaTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            filaTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            filaTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            filaTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            Panel cardClientes = CrearTarjetaStat("TOTAL CLIENTES", "0", ColorTeal, "Registros activos", out lblClientes);
            Panel cardMascotas = CrearTarjetaStat("TOTAL MASCOTAS", "0", ColorAzul, "En el sistema", out lblMascotas);
            Panel cardCitas = CrearTarjetaStat("CITAS HOY", "0", ColorAmbar, "Programadas hoy", out lblCitas);
            Panel cardRecaudado = CrearTarjetaStat("RECAUDADO", "0.00€", Color.FromArgb(108, 92, 231), "Total servicios", out lblRecaudado);

            filaTarjetas.Controls.Add(cardClientes, 0, 0);
            filaTarjetas.Controls.Add(cardMascotas, 1, 0);
            filaTarjetas.Controls.Add(cardCitas, 2, 0);
            filaTarjetas.Controls.Add(cardRecaudado, 3, 0);

            pnlScroll.Controls.Add(filaTarjetas);

            // ── Fila de Gráfica ──────────────────────────────
            Panel pnlChart = new Panel
            {
                Dock = DockStyle.Top,
                Height = 300,
                BackColor = ColorBlanco,
                Margin = new Padding(0, 0, 0, 24),
                Padding = new Padding(20)
            };
            AplicarBordeRedondeado(pnlChart);

            Label lblChartTit = new Label {
                Text = "Tendencia de Citas (Últimos Meses)",
                Font = new Font("Segoe UI Semibold", 12F),
                ForeColor = ColorTexto,
                Dock = DockStyle.Top,
                Height = 30
            };
            pnlChart.Controls.Add(lblChartTit);

            chartCitas = new System.Windows.Forms.DataVisualization.Charting.Chart {
                Dock = DockStyle.Fill,
                BackColor = ColorBlanco
            };
            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            chartCitas.ChartAreas.Add(chartArea);
            
            pnlChart.Controls.Add(chartCitas);
            pnlScroll.Controls.Add(pnlChart);
            pnlChart.BringToFront();

            // ── Fila inferior ─────────────────────────────────
            TableLayoutPanel filaInferior = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Top,
                Height = 340,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 0)
            };
            filaInferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            filaInferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));

            filaInferior.Controls.Add(CrearPanelBienvenida(), 0, 0);
            filaInferior.Controls.Add(CrearPanelAccesosRapidos(), 1, 0);

            pnlScroll.Controls.Add(filaInferior);

            pnlScroll.Controls.SetChildIndex(filaInferior, 0);
            pnlScroll.Controls.SetChildIndex(filaTarjetas, 0);
        }

        private Panel CrearTarjetaStat(string titulo, string valorInicial,
                                        Color colorAccento, string subtitulo,
                                        out Label lblValor)
        {
            Panel card = new Panel
            {
                BackColor = ColorBlanco,
                Margin = new Padding(0, 0, 16, 0),
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 20, 24, 20)
            };

            Panel acento = new Panel
            {
                Width = 5,
                Dock = DockStyle.Left,
                BackColor = colorAccento
            };
            card.Controls.Add(acento);

            Label lblTit = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = ColorTextoSuave,
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 22,
                TextAlign = ContentAlignment.BottomLeft,
                Padding = new Padding(6, 0, 0, 0)
            };

            lblValor = new Label
            {
                Text = valorInicial,
                Font = new Font("Segoe UI Light", 36F),
                ForeColor = ColorTexto,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(4, 0, 0, 0)
            };

            Label lblSub = new Label
            {
                Text = subtitulo,
                Font = new Font("Segoe UI", 9F),
                ForeColor = colorAccento,
                AutoSize = false,
                Dock = DockStyle.Bottom,
                Height = 22,
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(6, 0, 0, 0)
            };

            card.Controls.Add(lblValor);
            card.Controls.Add(lblSub);
            card.Controls.Add(lblTit);

            AplicarBordeRedondeado(card);
            return card;
        }

        private Panel CrearPanelBienvenida()
        {
            Panel card = new Panel
            {
                BackColor = ColorBlanco,
                Margin = new Padding(0, 0, 16, 0),
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 28, 30, 28)
            };

            Label lblBienvenida = new Label
            {
                Text = "Bienvenido a CliniPet",
                Font = new Font("Segoe UI Semibold", 18F),
                ForeColor = ColorTexto,
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.TopLeft
            };

            Label lblDesc = new Label
            {
                Text = "Gestiona tu clínica veterinaria de forma sencilla.\n\n" +
                       "Desde este panel puedes ver el estado general de la clínica. " +
                       "Usa el menú lateral para navegar entre clientes, mascotas y citas.\n\n" +
                       "Las tarjetas superiores se actualizan automáticamente con los " +
                       "datos reales de tu base de datos.",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = ColorTextoSuave,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopLeft
            };

            Panel franjaInferior = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 48,
                BackColor = Color.FromArgb(240, 252, 248),
                Padding = new Padding(0, 10, 0, 0)
            };
            Label lblVersion = new Label
            {
                Text = "CliniPet v1.0  ·  Joaquín Salvador Martín",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ColorTealOscuro,
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            franjaInferior.Controls.Add(lblVersion);

            card.Controls.Add(lblDesc);
            card.Controls.Add(lblBienvenida);
            card.Controls.Add(franjaInferior);

            AplicarBordeRedondeado(card);
            return card;
        }

        private Panel CrearPanelAccesosRapidos()
        {
            Panel card = new Panel
            {
                BackColor = ColorBlanco,
                Margin = new Padding(0, 0, 0, 0),
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 24, 24, 24)
            };

            Label lblTitAccesos = new Label
            {
                Text = "Acceso rápido",
                Font = new Font("Segoe UI Semibold", 13F),
                ForeColor = ColorTexto,
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 36,
                TextAlign = ContentAlignment.TopLeft
            };

            TableLayoutPanel grid = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 4,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 8, 0, 0)
            };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            grid.Controls.Add(CrearBotonAcceso("👥  Clientes", ColorTeal, Color.FromArgb(230, 252, 245), "clientes"), 0, 0);
            grid.Controls.Add(CrearBotonAcceso("🐾  Mascotas", ColorAzul, Color.FromArgb(230, 244, 253), "mascotas"), 1, 0);
            grid.Controls.Add(CrearBotonAcceso("📅  Citas", Color.FromArgb(124, 77, 255), Color.FromArgb(240, 235, 255), "citas"), 0, 1);
            grid.Controls.Add(CrearBotonAcceso("➕  Nueva Cita", ColorAmbar, Color.FromArgb(255, 248, 225), "nueva_cita"), 1, 1);
            grid.Controls.Add(CrearBotonAcceso("📦  Inventario", Color.FromArgb(255, 118, 117), Color.FromArgb(255, 235, 235), "inventario"), 0, 2);
            grid.Controls.Add(CrearBotonAcceso("🛠️  Servicios", Color.FromArgb(9, 132, 227), Color.FromArgb(235, 245, 255), "servicios"), 1, 2);
            grid.Controls.Add(CrearBotonAcceso("📊  Reportes", Color.FromArgb(108, 117, 125), Color.FromArgb(245, 245, 245), "reportes"), 0, 3);

            card.Controls.Add(grid);
            card.Controls.Add(lblTitAccesos);

            AplicarBordeRedondeado(card);
            return card;
        }

        private Button CrearBotonAcceso(string texto, Color colorTexto, Color colorFondo, string destino)
        {
            Button btn = new Button
            {
                Text = texto,
                Font = new Font("Segoe UI Semibold", 10.5F),
                ForeColor = colorTexto,
                BackColor = colorFondo,
                FlatStyle = FlatStyle.Flat,
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,
                Margin = new Padding(5),
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.FlatAppearance.BorderColor = colorTexto;
            btn.FlatAppearance.BorderSize = 1;

            Color fondoNormal = colorFondo;
            Color fondoHover = ControlPaint.Light(colorTexto, 0.7F);
            btn.MouseEnter += (s, e) => { btn.BackColor = fondoHover; btn.ForeColor = Color.White; };
            btn.MouseLeave += (s, e) => { btn.BackColor = fondoNormal; btn.ForeColor = colorTexto; };

            btn.Click += (s, e) => NavegacionSolicitada?.Invoke(destino);

            return btn;
        }

        private void AplicarBordeRedondeado(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(ColorBorde, 1F))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            };
        }

        private void CargarEstadisticas()
        {
            try
            {
                if (this.IsDisposed || !this.Created)
                    return;

                GestorEstadisticas gestor = new GestorEstadisticas();
                DataTable dt = gestor.ObtenerEstadisticasGenerales();
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (lblClientes != null) lblClientes.Text = row["total_clientes"].ToString();
                    if (lblMascotas != null) lblMascotas.Text = row["total_mascotas"].ToString();
                    if (lblCitas != null) lblCitas.Text = row["citas_hoy"].ToString();
                    if (lblRecaudado != null) lblRecaudado.Text = string.Format("{0:N2}€", row["total_recaudado"]);
                }

                // Cargar Gráfica (con validación exhaustiva)
                if (chartCitas != null && !chartCitas.IsDisposed)
                {
                    DataTable dtCitas = gestor.ObtenerCitasPorMes();
                    if (dtCitas != null && dtCitas.Rows.Count > 0)
                    {
                        try
                        {
                            if (chartCitas.Series != null)
                            {
                                chartCitas.Series.Clear();
                                var serie = chartCitas.Series.Add("Citas");
                                serie.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                                serie.Color = Color.FromArgb(150, 0, 184, 148);
                                serie.BorderColor = Color.FromArgb(0, 184, 148);
                                serie.BorderWidth = 3;

                                foreach (DataRow r in dtCitas.Rows)
                                {
                                    serie.Points.AddXY(r["nombre_mes"].ToString(), r["total_citas"]);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
    }
}