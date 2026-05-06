namespace Clinipet_JoaquínSalvadorMartín
{
    partial class Usuario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.ClientSize = new System.Drawing.Size(1397, 759);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Usuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CliniPet - Panel de Usuario";
            this.Load += new System.EventHandler(this.Usuario_Load);

            this.ResumeLayout(false);
        }

        #endregion

        // Declaración de controles creados en código
        public System.Windows.Forms.Label lblClientes;
        public System.Windows.Forms.Label lblMascotas;
        public System.Windows.Forms.Label lblCitas;
    }
}