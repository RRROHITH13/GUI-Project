using System;
using System.Windows;
using MySql.Data.MySqlClient;
using DB_Connection;

namespace WpfApp1
{
    public partial class FO : Window
    {
        public FO()
        {
            InitializeComponent();
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim(); // User enters email
            string newPassword = txtNewPassword.Password.Trim(); // User enters new password
            string confirmPassword = txtConfirmPassword.Password.Trim(); // User confirms password

            // Validate user input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return;
            }

            Db_connection db = new Db_connection();
            MySqlConnection con = db.connectDB();

            try
            {
                con.Open();

                // Check if the email exists in the database
                string query = "SELECT * FROM users WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close(); // Close reader before executing another command

                    // Directly update password in the database
                    string updateQuery = "UPDATE users SET password = @NewPassword WHERE email = @Email";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    updateCmd.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Your password has been successfully updated. You can now log in with your new password.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update password. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Email not found. Please check and try again.");
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
    }
}
