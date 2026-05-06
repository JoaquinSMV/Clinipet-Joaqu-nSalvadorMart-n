namespace Clinipet_JoaquínSalvadorMartín
{
    partial class FrmGestionCita
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.ElegirMascota = new System.Windows.Forms.ComboBox();
            this.NombreDueño = new System.Windows.Forms.Label();
            this.fechaCita = new System.Windows.Forms.DateTimePicker();
            this.Motivo = new System.Windows.Forms.RichTextBox();
            this.GuardarYModificar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._Clinipet_JoaquinSMDataSet = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSet();
            this.mascotasTableAdapter = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.MascotasTableAdapter();
            this.clientesTableAdapter = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.ClientesTableAdapter();
            this.citasTableAdapter = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.CitasTableAdapter();
            this.Observacion = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._Clinipet_JoaquinSMDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // ElegirMascota
            // 
            this.ElegirMascota.FormattingEnabled = true;
            this.ElegirMascota.Location = new System.Drawing.Point(117, 117);
            this.ElegirMascota.Name = "ElegirMascota";
            this.ElegirMascota.Size = new System.Drawing.Size(242, 24);
            this.ElegirMascota.TabIndex = 0;
            this.ElegirMascota.SelectedIndexChanged += new System.EventHandler(this.ElegirMascota_SelectedIndexChanged);
            // 
            // NombreDueño
            // 
            this.NombreDueño.AutoSize = true;
            this.NombreDueño.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.NombreDueño.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(122)))), ((int)(((byte)(109)))));
            this.NombreDueño.Location = new System.Drawing.Point(115, 169);
            this.NombreDueño.Name = "NombreDueño";
            this.NombreDueño.Size = new System.Drawing.Size(81, 20);
            this.NombreDueño.TabIndex = 4;
            this.NombreDueño.Text = "Dueño: ---";
            // 
            // fechaCita
            // 
            this.fechaCita.Location = new System.Drawing.Point(117, 211);
            this.fechaCita.Name = "fechaCita";
            this.fechaCita.Size = new System.Drawing.Size(242, 22);
            this.fechaCita.TabIndex = 3;
            // 
            // Motivo
            // 
            this.Motivo.Location = new System.Drawing.Point(117, 265);
            this.Motivo.Name = "Motivo";
            this.Motivo.Size = new System.Drawing.Size(242, 114);
            this.Motivo.TabIndex = 2;
            this.Motivo.Text = "";
            // 
            // GuardarYModificar
            // 
            this.GuardarYModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(43)))));
            this.GuardarYModificar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.GuardarYModificar.ForeColor = System.Drawing.SystemColors.Control;
            this.GuardarYModificar.Location = new System.Drawing.Point(118, 555);
            this.GuardarYModificar.Name = "GuardarYModificar";
            this.GuardarYModificar.Size = new System.Drawing.Size(242, 38);
            this.GuardarYModificar.TabIndex = 1;
            this.GuardarYModificar.Text = "Guardar Cita";
            this.GuardarYModificar.UseVisualStyleBackColor = false;
            this.GuardarYModificar.Click += new System.EventHandler(this.GuardarYModificar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(122)))), ((int)(((byte)(109)))));
            this.label1.Location = new System.Drawing.Point(112, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nueva Cita";
            // 
            // _Clinipet_JoaquinSMDataSet
            // 
            this._Clinipet_JoaquinSMDataSet.DataSetName = "_Clinipet_JoaquinSMDataSet";
            this._Clinipet_JoaquinSMDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mascotasTableAdapter
            // 
            this.mascotasTableAdapter.ClearBeforeFill = true;
            // 
            // clientesTableAdapter
            // 
            this.clientesTableAdapter.ClearBeforeFill = true;
            // 
            // citasTableAdapter
            // 
            this.citasTableAdapter.ClearBeforeFill = true;
            // 
            // Observacion
            // 
            this.Observacion.Location = new System.Drawing.Point(117, 407);
            this.Observacion.Name = "Observacion";
            this.Observacion.Size = new System.Drawing.Size(242, 114);
            this.Observacion.TabIndex = 5;
            this.Observacion.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(122)))), ((int)(((byte)(109)))));
            this.label2.Location = new System.Drawing.Point(115, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mascota";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(122)))), ((int)(((byte)(109)))));
            this.label3.Location = new System.Drawing.Point(115, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Motivo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(122)))), ((int)(((byte)(109)))));
            this.label4.Location = new System.Drawing.Point(114, 385);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Observacion";
            // 
            // FrmGestionCita
            // 
            this.ClientSize = new System.Drawing.Size(493, 625);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Observacion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GuardarYModificar);
            this.Controls.Add(this.Motivo);
            this.Controls.Add(this.fechaCita);
            this.Controls.Add(this.NombreDueño);
            this.Controls.Add(this.ElegirMascota);
            this.Name = "FrmGestionCita";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Cita";
            this.Load += new System.EventHandler(this.FrmGestionCita_Load);
            ((System.ComponentModel.ISupportInitialize)(this._Clinipet_JoaquinSMDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ElegirMascota;
        private System.Windows.Forms.Label NombreDueño;
        private System.Windows.Forms.DateTimePicker fechaCita;
        private System.Windows.Forms.RichTextBox Motivo;
        private System.Windows.Forms.Button GuardarYModificar;
        private System.Windows.Forms.Label label1;

        // DEFINICIÓN DE OBJETOS DE DATOS
        private _Clinipet_JoaquinSMDataSet _Clinipet_JoaquinSMDataSet;
        private _Clinipet_JoaquinSMDataSetTableAdapters.MascotasTableAdapter mascotasTableAdapter;
        private _Clinipet_JoaquinSMDataSetTableAdapters.ClientesTableAdapter clientesTableAdapter;
        private _Clinipet_JoaquinSMDataSetTableAdapters.CitasTableAdapter citasTableAdapter;
        private System.Windows.Forms.RichTextBox Observacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}