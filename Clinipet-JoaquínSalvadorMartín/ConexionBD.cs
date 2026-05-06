using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Clinipet_JoaquínSalvadorMartín
{
    public class ConexionBD
    {
        // 1. La cadena de conexión con tus datos exactos de SQL Express
        // El símbolo @ es necesario para que la barra invertida \ no de error
        private string cadena = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Clinipet-JoaquinSM;Integrated Security=True";

        // 2. El objeto de conexión que usarán los formularios
        public SqlConnection leer = new SqlConnection();

        public ConexionBD()
        {
            leer.ConnectionString = cadena;
        }

        // 3. Método para abrir la base de datos de forma segura
        public void Abrir()
        {
            try
            {
                if (leer.State == ConnectionState.Closed)
                {
                    leer.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos: " + ex.Message);
            }
        }

        // 4. Método para cerrar la conexión y liberar recursos
        public void Cerrar()
        {
            try
            {
                if (leer.State == ConnectionState.Open)
                {
                    leer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar la conexión: " + ex.Message);
            }
        }
    }
}