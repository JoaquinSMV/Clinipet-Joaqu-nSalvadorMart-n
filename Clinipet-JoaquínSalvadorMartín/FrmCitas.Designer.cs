namespace Clinipet_JoaquínSalvadorMartín
{
    partial class FrmCitas
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._Clinipet_JoaquinSMDataSet = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSet();
            this.citasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.citasTableAdapter = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.CitasTableAdapter();
            this.tableAdapterManager = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.TableAdapterManager();
            this.clientesTableAdapter = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.ClientesTableAdapter();
            this.mascotasTableAdapter = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.MascotasTableAdapter();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.FechaHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Motivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NuevaCita = new System.Windows.Forms.Button();
            this.ModificarCita = new System.Windows.Forms.Button();
            this.EliminarCita = new System.Windows.Forms.Button();
            this.QuitarFiltros = new System.Windows.Forms.Button();
            this.cmbFiltrarCliente = new System.Windows.Forms.ComboBox();
            this.cmbFiltrarMascota = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._Clinipet_JoaquinSMDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.citasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.SuspendLayout();
            // 
            // _Clinipet_JoaquinSMDataSet
            // 
            this._Clinipet_JoaquinSMDataSet.DataSetName = "_Clinipet_JoaquinSMDataSet";
            this._Clinipet_JoaquinSMDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // citasBindingSource
            // 
            this.citasBindingSource.DataMember = "Citas";
            this.citasBindingSource.DataSource = this._Clinipet_JoaquinSMDataSet;
            // 
            // citasTableAdapter
            // 
            this.citasTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CitasTableAdapter = this.citasTableAdapter;
            this.tableAdapterManager.ClientesTableAdapter = this.clientesTableAdapter;
            this.tableAdapterManager.MascotasTableAdapter = this.mascotasTableAdapter;
            this.tableAdapterManager.UpdateOrder = Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // clientesTableAdapter
            // 
            this.clientesTableAdapter.ClearBeforeFill = true;
            // 
            // mascotasTableAdapter
            // 
            this.mascotasTableAdapter.ClearBeforeFill = true;
            // 
            // dgvCitas
            // 
            this.dgvCitas.AllowUserToAddRows = false;
            this.dgvCitas.AllowUserToDeleteRows = false;
            this.dgvCitas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCitas.AutoGenerateColumns = false;
            this.dgvCitas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCitas.ColumnHeadersHeight = 45;
            this.dgvCitas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaHora,
            this.Motivo,
            this.Observaciones});
            this.dgvCitas.DataSource = this.citasBindingSource;
            this.dgvCitas.Location = new System.Drawing.Point(35, 115);
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.ReadOnly = true;
            this.dgvCitas.RowHeadersVisible = false;
            this.dgvCitas.RowHeadersWidth = 51;
            this.dgvCitas.RowTemplate.Height = 50;
            this.dgvCitas.Size = new System.Drawing.Size(1269, 580);
            this.dgvCitas.TabIndex = 0;
            this.dgvCitas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCitas_CellContentClick);
            // 
            // FechaHora
            // 
            this.FechaHora.DataPropertyName = "FechaHora";
            this.FechaHora.HeaderText = "Fecha y Hora";
            this.FechaHora.MinimumWidth = 6;
            this.FechaHora.Name = "FechaHora";
            this.FechaHora.ReadOnly = true;
            // 
            // Motivo
            // 
            this.Motivo.DataPropertyName = "Motivo";
            this.Motivo.HeaderText = "Motivo de la Cita";
            this.Motivo.MinimumWidth = 6;
            this.Motivo.Name = "Motivo";
            this.Motivo.ReadOnly = true;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "Observaciones";
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.MinimumWidth = 6;
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.ReadOnly = true;
            // 
            // NuevaCita
            // 
            this.NuevaCita.Location = new System.Drawing.Point(35, 45);
            this.NuevaCita.Name = "NuevaCita";
            this.NuevaCita.Size = new System.Drawing.Size(180, 45);
            this.NuevaCita.TabIndex = 1;
            this.NuevaCita.Text = "  ✚  Nueva Cita";
            this.NuevaCita.Click += new System.EventHandler(this.NuevaCita_Click);
            // 
            // ModificarCita
            // 
            this.ModificarCita.Location = new System.Drawing.Point(225, 45);
            this.ModificarCita.Name = "ModificarCita";
            this.ModificarCita.Size = new System.Drawing.Size(180, 45);
            this.ModificarCita.TabIndex = 2;
            this.ModificarCita.Text = "  ✎  Modificar";
            this.ModificarCita.Click += new System.EventHandler(this.ModificarCita_Click);
            // 
            // EliminarCita
            // 
            this.EliminarCita.Location = new System.Drawing.Point(415, 45);
            this.EliminarCita.Name = "EliminarCita";
            this.EliminarCita.Size = new System.Drawing.Size(180, 45);
            this.EliminarCita.TabIndex = 3;
            this.EliminarCita.Text = "  🗑  Eliminar";
            this.EliminarCita.Click += new System.EventHandler(this.EliminarCita_Click);
            // 
            // QuitarFiltros
            // 
            this.QuitarFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QuitarFiltros.Location = new System.Drawing.Point(1060, 45);
            this.QuitarFiltros.Name = "QuitarFiltros";
            this.QuitarFiltros.Size = new System.Drawing.Size(180, 45);
            this.QuitarFiltros.TabIndex = 4;
            this.QuitarFiltros.Text = "  ✕  Limpiar Filtros";
            this.QuitarFiltros.Click += new System.EventHandler(this.QuitarFiltros_Click);
            // 
            // cmbFiltrarCliente
            // 
            this.cmbFiltrarCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFiltrarCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltrarCliente.FormattingEnabled = true;
            this.cmbFiltrarCliente.Location = new System.Drawing.Point(680, 55);
            this.cmbFiltrarCliente.Name = "cmbFiltrarCliente";
            this.cmbFiltrarCliente.Size = new System.Drawing.Size(170, 24);
            this.cmbFiltrarCliente.TabIndex = 6;
            // 
            // cmbFiltrarMascota
            // 
            this.cmbFiltrarMascota.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFiltrarMascota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltrarMascota.FormattingEnabled = true;
            this.cmbFiltrarMascota.Location = new System.Drawing.Point(860, 55);
            this.cmbFiltrarMascota.Name = "cmbFiltrarMascota";
            this.cmbFiltrarMascota.Size = new System.Drawing.Size(170, 24);
            this.cmbFiltrarMascota.TabIndex = 5;
            // 
            // FrmCitas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1339, 744);
            this.Controls.Add(this.cmbFiltrarMascota);
            this.Controls.Add(this.cmbFiltrarCliente);
            this.Controls.Add(this.QuitarFiltros);
            this.Controls.Add(this.EliminarCita);
            this.Controls.Add(this.ModificarCita);
            this.Controls.Add(this.NuevaCita);
            this.Controls.Add(this.dgvCitas);
            this.Name = "FrmCitas";
            this.Padding = new System.Windows.Forms.Padding(35);
            this.Text = "CliniPet - Gestión de Citas";
            this.Load += new System.EventHandler(this.FrmCitas_Load);
            ((System.ComponentModel.ISupportInitialize)(this._Clinipet_JoaquinSMDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.citasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.ResumeLayout(false);

        }



        private _Clinipet_JoaquinSMDataSet _Clinipet_JoaquinSMDataSet;
        private System.Windows.Forms.BindingSource citasBindingSource;
        private _Clinipet_JoaquinSMDataSetTableAdapters.CitasTableAdapter citasTableAdapter;
        private _Clinipet_JoaquinSMDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private _Clinipet_JoaquinSMDataSetTableAdapters.ClientesTableAdapter clientesTableAdapter;
        private _Clinipet_JoaquinSMDataSetTableAdapters.MascotasTableAdapter mascotasTableAdapter;
        private System.Windows.Forms.DataGridView dgvCitas;
        private System.Windows.Forms.Button NuevaCita;
        private System.Windows.Forms.Button ModificarCita;
        private System.Windows.Forms.Button EliminarCita;
        private System.Windows.Forms.Button QuitarFiltros;
        private System.Windows.Forms.ComboBox cmbFiltrarCliente;
        private System.Windows.Forms.ComboBox cmbFiltrarMascota;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn Motivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
    }
}