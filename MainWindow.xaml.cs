using System;
using System.Windows;
using MySql.Data.MySqlClient;
using DB_Connection;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            Db_connection db = new Db_connection();
            MySqlConnection con = db.connectDB();

            try
            {
                con.Open();
                string query = "SELECT * FROM users WHERE username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    MessageBox.Show("Login Successful!");

                    // Open Window3 upon successful login
                    Window3 mainAppWindow = new Window3();
                    mainAppWindow.Show();

                    // Close the login window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Register Window
            RE registerWindow = new RE();
            registerWindow.Show();
            this.Close();
        }

        private void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Forgot Password Window
            FO forgotPasswordWindow = new FO();
            forgotPasswordWindow.Show();
            this.Close();
        }
    }
}
