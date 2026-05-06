namespace Clinipet_JoaquínSalvadorMartín
{
    partial class FrmMascotas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._Clinipet_JoaquinSMDataSet = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSet();
            this.mascotasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mascotasTableAdapter = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.MascotasTableAdapter();
            this.tableAdapterManager = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.TableAdapterManager();
            this.mascotasDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Añadir = new System.Windows.Forms.Button();
            this.Modificar = new System.Windows.Forms.Button();
            this.Eliminar = new System.Windows.Forms.Button();
            this.clientesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientesTableAdapter = new Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.ClientesTableAdapter();
            this.dNIComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BotonQuitarFiltros = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._Clinipet_JoaquinSMDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mascotasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mascotasDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // _Clinipet_JoaquinSMDataSet
            // 
            this._Clinipet_JoaquinSMDataSet.DataSetName = "_Clinipet_JoaquinSMDataSet";
            this._Clinipet_JoaquinSMDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mascotasBindingSource
            // 
            this.mascotasBindingSource.DataMember = "Mascotas";
            this.mascotasBindingSource.DataSource = this._Clinipet_JoaquinSMDataSet;
            // 
            // mascotasTableAdapter
            // 
            this.mascotasTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CitasTableAdapter = null;
            this.tableAdapterManager.ClientesTableAdapter = null;
            this.tableAdapterManager.MascotasTableAdapter = this.mascotasTableAdapter;
            this.tableAdapterManager.UpdateOrder = Clinipet_JoaquínSalvadorMartín._Clinipet_JoaquinSMDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // mascotasDataGridView
            // 
            this.mascotasDataGridView.AllowUserToAddRows = false;
            this.mascotasDataGridView.AutoGenerateColumns = false;
            this.mascotasDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mascotasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mascotasDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.mascotasDataGridView.DataSource = this.mascotasBindingSource;
            this.mascotasDataGridView.Location = new System.Drawing.Point(30, 123);
            this.mascotasDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.mascotasDataGridView.Name = "mascotasDataGridView";
            this.mascotasDataGridView.ReadOnly = true;
            this.mascotasDataGridView.RowHeadersWidth = 51;
            this.mascotasDataGridView.Size = new System.Drawing.Size(1735, 590);
            this.mascotasDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "MascotaID";
            this.dataGridViewTextBoxColumn1.HeaderText = "MascotaID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ClienteID";
            this.dataGridViewTextBoxColumn2.HeaderText = "ClienteID";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Nombre";
            this.dataGridViewTextBoxColumn3.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Especie";
            this.dataGridViewTextBoxColumn4.HeaderText = "Especie";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Raza";
            this.dataGridViewTextBoxColumn5.HeaderText = "Raza";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "FechaNacimiento";
            this.dataGridViewTextBoxColumn6.HeaderText = "FechaNacimiento";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Sexo";
            this.dataGridViewTextBoxColumn7.HeaderText = "Sexo";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "NotasEspeciales";
            this.dataGridViewTextBoxColumn8.HeaderText = "NotasEspeciales";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // Añadir
            // 
            this.Añadir.Location = new System.Drawing.Point(30, 34);
            this.Añadir.Margin = new System.Windows.Forms.Padding(4);
            this.Añadir.Name = "Añadir";
            this.Añadir.Size = new System.Drawing.Size(100, 28);
            this.Añadir.TabIndex = 2;
            this.Añadir.Text = "Añadir";
            this.Añadir.UseVisualStyleBackColor = true;
            this.Añadir.Click += new System.EventHandler(this.Añadir_Click);
            // 
            // Modificar
            // 
            this.Modificar.Location = new System.Drawing.Point(155, 34);
            this.Modificar.Margin = new System.Windows.Forms.Padding(4);
            this.Modificar.Name = "Modificar";
            this.Modificar.Size = new System.Drawing.Size(100, 28);
            this.Modificar.TabIndex = 3;
            this.Modificar.Text = "Modificar";
            this.Modificar.UseVisualStyleBackColor = true;
            this.Modificar.Click += new System.EventHandler(this.Modificar_Click);
            // 
            // Eliminar
            // 
            this.Eliminar.Location = new System.Drawing.Point(286, 34);
            this.Eliminar.Margin = new System.Windows.Forms.Padding(4);
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Size = new System.Drawing.Size(100, 28);
            this.Eliminar.TabIndex = 4;
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.UseVisualStyleBackColor = true;
            this.Eliminar.Click += new System.EventHandler(this.Eliminar_Click);
            // 
            // clientesBindingSource
            // 
            this.clientesBindingSource.DataMember = "Clientes";
            this.clientesBindingSource.DataSource = this._Clinipet_JoaquinSMDataSet;
            // 
            // clientesTableAdapter
            // 
            this.clientesTableAdapter.ClearBeforeFill = true;
            // 
            // dNIComboBox
            // 
            this.dNIComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientesBindingSource, "DNI", true));
            this.dNIComboBox.FormattingEnabled = true;
            this.dNIComboBox.Location = new System.Drawing.Point(1204, 41);
            this.dNIComboBox.Name = "dNIComboBox";
            this.dNIComboBox.Size = new System.Drawing.Size(204, 24);
            this.dNIComboBox.TabIndex = 6;
            this.dNIComboBox.SelectedIndexChanged += new System.EventHandler(this.dNIComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1007, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Filtrar por DNI :";
            // 
            // BotonQuitarFiltros
            // 
            this.BotonQuitarFiltros.Location = new System.Drawing.Point(1584, 34);
            this.BotonQuitarFiltros.Name = "BotonQuitarFiltros";
            this.BotonQuitarFiltros.Size = new System.Drawing.Size(75, 24);
            this.BotonQuitarFiltros.TabIndex = 8;
            this.BotonQuitarFiltros.Text = "Quitar Filtros";
            this.BotonQuitarFiltros.UseVisualStyleBackColor = true;
            this.BotonQuitarFiltros.Click += new System.EventHandler(this.BotonQuitarFiltros_Click);
            // 
            // FrmMascotas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 744);
            this.Controls.Add(this.BotonQuitarFiltros);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dNIComboBox);
            this.Controls.Add(this.Eliminar);
            this.Controls.Add(this.Modificar);
            this.Controls.Add(this.Añadir);
            this.Controls.Add(this.mascotasDataGridView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMascotas";
            this.Text = "FrmMascotas";
            this.Load += new System.EventHandler(this.FrmMascotas_Load);
            ((System.ComponentModel.ISupportInitialize)(this._Clinipet_JoaquinSMDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mascotasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mascotasDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _Clinipet_JoaquinSMDataSet _Clinipet_JoaquinSMDataSet;
        private System.Windows.Forms.BindingSource mascotasBindingSource;
        private _Clinipet_JoaquinSMDataSetTableAdapters.MascotasTableAdapter mascotasTableAdapter;
        private _Clinipet_JoaquinSMDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView mascotasDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Button Añadir;
        private System.Windows.Forms.Button Modificar;
        private System.Windows.Forms.Button Eliminar;
        private System.Windows.Forms.BindingSource clientesBindingSource;
        private _Clinipet_JoaquinSMDataSetTableAdapters.ClientesTableAdapter clientesTableAdapter;
        private System.Windows.Forms.ComboBox dNIComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BotonQuitarFiltros;
    }
}