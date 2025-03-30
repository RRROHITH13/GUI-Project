using System;
using System.Windows;
using MySql.Data.MySqlClient;
using DB_Connection;  

namespace WpfApp1
{
    public partial class RE : Window
    {
        public RE()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string email = txtEmail.Text;

            //  Db_connection to establish the connection
            Db_connection db = new Db_connection();
            MySqlConnection con = db.connectDB();

            try
            {
                // Open the database connection
                con.Open();

                // Prepare the SQL query to insert user details
                string query = "INSERT INTO users (username, password, email) VALUES (@username, @password, @email)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password); 
                cmd.Parameters.AddWithValue("@email", email);

                // Execute the query
                int result = cmd.ExecuteNonQuery();

                // Check if the registration was successful
                if (result > 0)
                {
                    MessageBox.Show("Registration Successful!");
                }
                else
                {
                    MessageBox.Show("Registration failed. Try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Login Window (MainWindow) and close the Register window
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
