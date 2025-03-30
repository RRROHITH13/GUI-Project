using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using DB_Connection; // Import the namespace for Db_connection

namespace WpfApp1
{
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            LoadAppointments(); // Load appointments on window load
        }

        private void LoadAppointments()
        {
            try
            {
                Db_connection dbConn = new Db_connection();
                using (MySqlConnection conn = dbConn.connectDB()) // Use instance method connectDB
                {
                    conn.Open();
                    string query = "SELECT Name, Slots, TIME_FORMAT(Time, '%H:%i') AS Time, DATE_FORMAT(Date, '%Y-%m-%d') AS Date, Specialization FROM Appointments"; // Updated query
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Set the ItemsSource for the DataGrid
                    appointmentDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message);
            }
        }


        private void BookAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentDataGrid.SelectedItem is DataRowView row)
            {
                // Correctly handle nullable Slots column
                int slots = row["Slots"] != DBNull.Value ? Convert.ToInt32(row["Slots"]) : 0;

                if (slots > 0)
                {
                    string name = row["Name"].ToString();
                    string date = row["Date"].ToString();

                    try
                    {
                        // Create an instance of Db_connection for booking
                        Db_connection dbConn = new Db_connection();
                        using (MySqlConnection conn = dbConn.connectDB()) // Use instance method connectDB
                        {
                            conn.Open();
                            string updateQuery = "UPDATE Appointments SET Slots = Slots - 1 WHERE Name = @name AND Date = @date";
                            MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Appointment booked successfully!");
                        LoadAppointments(); // Refresh the table after booking
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error booking appointment: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No slots available for this appointment.");
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment.");
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close(); // Close the current window
        }

        private void appointmentDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.Item is DataRowView row)
            {
                // Correctly handle nullable Slots column
                int slots = row["Slots"] != DBNull.Value ? Convert.ToInt32(row["Slots"]) : 0;

                if (slots == 0)
                {
                    // Change the row background to red if slots are 0
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                    e.Row.Foreground = new SolidColorBrush(Colors.White);
                }
                else
                {
                    // Default background and text color if slots are available
                    e.Row.Background = new SolidColorBrush(Colors.White);
                    e.Row.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }

        private void AddDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 Doc = new Window2();
            Doc.Show();
            this.Close();
        }

        private void GoToWindow2Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2(); // Create an instance of Window2
            window2.Show(); // Show Window2
            this.Close();
        }
    }
}
