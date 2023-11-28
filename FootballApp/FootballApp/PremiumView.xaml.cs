// Add these using statements at the top of your BasicView.xaml.cs file
using System.Windows.Controls;
using System.Data;
using FootBallAppGUI;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System;

namespace FootballApp
{
    public partial class PremiumView : Window
    {
        private UserData userData;  // Add this field

        public PremiumView(UserData userData)
        {
            InitializeComponent();
            this.userData = userData;  // Initialize the field with the provided UserData instance
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // You can implement the search functionality here
            string homeTeam = HomeTeamTextBox.Text;

            if (string.IsNullOrEmpty(homeTeam) || homeTeam == "Enter Home Team")
            {
                // Handle case where no home team is entered
                MessageBox.Show("Please enter a home team.");
                return;
            }

            // Assuming you have a method in your database handler to retrieve the data
            DataTable searchData = GetSearchResults(homeTeam);

            // You can then display the search results in a DataGrid or any other UI element
            DataGrid.ItemsSource = searchData?.DefaultView;
        }

        private DataTable GetSearchResults(string homeTeam)
        {
            var conn = new SqlConnection(FootballApp.Properties.Settings.Default.asConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT date, home_team, away_team, result_full, result_ht, home_possession, away_possession, home_red_cards, away_red_cards, home_yellow_cards, away_yellow_cards, home_offsides, away_offsides, home_passes, away_passes, home_shots_on_target, away_shots_on_target, home_corners, away_corners, home_tackles, away_tackles, corners_avg_home, corners_avg_away FROM MatchData WHERE home_team = @HomeTeam";
                cmd.Parameters.AddWithValue("@HomeTeam", homeTeam);
                conn.Open();

                DataTable result = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(result);
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        private void HomeTeamTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (HomeTeamTextBox.Text == "Enter Home Team")
            {
                HomeTeamTextBox.Text = string.Empty;
            }
        }

        private void HomeTeamTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HomeTeamTextBox.Text))
            {
                HomeTeamTextBox.Text = "Enter Home Team";
            }
        }
    }
}

