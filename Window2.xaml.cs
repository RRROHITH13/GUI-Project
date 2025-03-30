using System;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using MySql.Data.MySqlClient;
using DB_Connection; // Import the namespace for Db_connection

namespace WpfApp1
{
    public partial class Window2 : Window
    {
        private Db_connection dbConnection;

        public Window2()
        {
            InitializeComponent();
            dbConnection = new Db_connection();
        }

        private void AddAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            int slots;
            string time = TimeTextBox.Text;
            DateTime date;
            string specialization = SpecializationTextBox.Text;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(name) || !int.TryParse(SlotsTextBox.Text, out slots) ||
                !DateTime.TryParse(DateTextBox.Text, out date) || string.IsNullOrWhiteSpace(specialization))
            {
                MessageBox.Show("Please fill in all fields correctly.");
                return;
            }

            try
            {
                using (MySqlConnection conn = dbConnection.connectDB())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO Appointments (Name, Slots, Time, Date, Specialization) VALUES (@Name, @Slots, @Time, @Date, @Specialization)", conn);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Slots", slots);
                    cmd.Parameters.AddWithValue("@Time", time);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Specialization", specialization);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Appointment added successfully.");
                ClearInputs(); // Clear input fields after adding
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding appointment: " + ex.Message);
            }
        }

        private void EditAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AppointmentIdTextBox.Text, out int id))
            {
                MessageBox.Show("Please enter a valid Appointment ID.");
                return;
            }

            string name = NameTextBox.Text;
            int slots;
            string time = TimeTextBox.Text;
            DateTime date;
            string specialization = SpecializationTextBox.Text;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(name) || !int.TryParse(SlotsTextBox.Text, out slots) ||
                !DateTime.TryParse(DateTextBox.Text, out date) || string.IsNullOrWhiteSpace(specialization))
            {
                MessageBox.Show("Please fill in all fields correctly.");
                return;
            }

            try
            {
                using (MySqlConnection conn = dbConnection.connectDB())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Appointments SET Name=@Name, Slots=@Slots, Time=@Time, Date=@Date, Specialization=@Specialization WHERE ID=@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Slots", slots);
                    cmd.Parameters.AddWithValue("@Time", time);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Specialization", specialization);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Appointment updated successfully.");
                ClearInputs(); // Clear input fields after editing
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating appointment: " + ex.Message);
            }
        }

        private void DeleteAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AppointmentIdTextBox.Text, out int id))
            {
                MessageBox.Show("Please enter a valid Appointment ID.");
                return;
            }

            try
            {
                using (MySqlConnection conn = dbConnection.connectDB())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Appointments WHERE ID=@ID", conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Appointment deleted successfully.");
                ClearInputs(); // Clear input fields after deleting
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting appointment: " + ex.Message);
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Window3 win = new Window3();
            win.Show();
            this.Close(); // Close the current window
        }

        private void ClearInputs()
        {
            AppointmentIdTextBox.Clear();
            NameTextBox.Clear();
            SlotsTextBox.Clear();
            TimeTextBox.Clear();
            DateTextBox.Clear();
            SpecializationTextBox.Clear();
        }

        private void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Tag != null && textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black; // Change text color to black
            }
        }

        private void AddPlaceholder(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Tag != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = System.Windows.Media.Brushes.Gray; // Change text color to gray
            }
        }
    }
  }