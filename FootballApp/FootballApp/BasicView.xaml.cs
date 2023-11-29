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
    public partial class BasicView : Window
    {
        private UserData userData;  // Add this field

        public BasicView(UserData userData)
        {
            InitializeComponent();
            this.userData = userData;  // Initialize the field with the provided UserData instance
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement the search functionality here
            string homeTeam = HomeTeamTextBox.Text.Trim();
            string awayTeam = AwayTeamTextBox.Text.Trim();

            if ((string.IsNullOrEmpty(homeTeam) || homeTeam == "Enter Home Team") && (string.IsNullOrEmpty(awayTeam) || awayTeam == "Enter Away Team"))
            {
                // Handle case where both teams are empty or contain placeholder text
                MessageBox.Show("Please enter at least one team.");
                return;
            }

            // Assuming you have a method in your database handler to retrieve the data
            DataTable searchData = GetSearchResults(awayTeam, homeTeam);

            // Display the search results in a DataGrid or any other UI element
            DataGrid.ItemsSource = searchData?.DefaultView;

            // Save the entered team names to the database table 'SearchedTeams'
            SaveToSearchedTeams(homeTeam, awayTeam);
        }

        private DataTable GetSearchResults(string awayTeam, string homeTeam)
        {
            var conn = new SqlConnection(FootballApp.Properties.Settings.Default.asConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
<<<<<<< Updated upstream
                cmd.CommandText = "SELECT date, season, home_team, away_team, result_full, result_ht FROM MatchData WHERE home_team = @HomeTeam";
                cmd.Parameters.AddWithValue("@HomeTeam", homeTeam);
                conn.Open();
=======
                conn.Open(); // Open the connection once

                if (!string.IsNullOrEmpty(homeTeam) && string.IsNullOrEmpty(awayTeam))
                {
                    // Only Home Team is entered
                    cmd.CommandText = "SELECT date, home_team, away_team, result_full, result_ht, home_possession, away_possession, home_red_cards, away_red_cards, home_yellow_cards, away_yellow_cards, home_offsides, away_offsides, home_passes, away_passes, home_shots_on_target, away_shots_on_target, home_corners, away_corners, home_tackles, away_tackles, corners_avg_home, corners_avg_away FROM MatchData WHERE home_team = @HomeTeam";
                    cmd.Parameters.AddWithValue("@HomeTeam", homeTeam);
                }
                else if (string.IsNullOrEmpty(homeTeam) && !string.IsNullOrEmpty(awayTeam))
                {
                    // Only Away Team is entered
                    cmd.CommandText = "SELECT date, home_team, away_team, result_full, result_ht, home_possession, away_possession, home_red_cards, away_red_cards, home_yellow_cards, away_yellow_cards, home_offsides, away_offsides, home_passes, away_passes, home_shots_on_target, away_shots_on_target, home_corners, away_corners, home_tackles, away_tackles, corners_avg_home, corners_avg_away FROM MatchData WHERE away_team = @AwayTeam";
                    cmd.Parameters.AddWithValue("@AwayTeam", awayTeam);
                }
                else
                {
                    // Both teams are entered or neither is entered
                    MessageBox.Show("Please enter only one team at a time.");
                    return new DataTable(); // Return an empty DataTable
                }
>>>>>>> Stashed changes

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
                    conn.Close(); // Close the connection in the finally block
            }
        }

        private void HomeTeamTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (HomeTeamTextBox.Text == "")
            {
                HomeTeamTextBox.Text = string.Empty;
            }
        }

        private void HomeTeamTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HomeTeamTextBox.Text))
            {
                HomeTeamTextBox.Text = "";
            }
        }

        private void AwayTeamTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AwayTeamTextBox.Text == "")
            {
                AwayTeamTextBox.Text = string.Empty;
            }
        }

        private void AwayTeamTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AwayTeamTextBox.Text))
            {
                AwayTeamTextBox.Text = "";
            }
        }

        private void SaveToSearchedTeams(string homeTeam, string awayTeam)
        {
            string connectionString = FootballApp.Properties.Settings.Default.asConnectionString;

            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hussa\Documents\github\Football-Insights-Premier-League\FootballApp\FootballApp\FootballData.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                string insertQuery = "INSERT INTO SearchedTeams(team_name)VALUES(@TeamName)";

                try
                {
                    connection.Open();

                    if (!string.IsNullOrEmpty(homeTeam) && homeTeam != "Enter Home Team")
                    {
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@TeamName", homeTeam);
                            command.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrEmpty(awayTeam) && awayTeam != "Enter Away Team")
                    {
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.Clear(); // Clear previous parameters
                            command.Parameters.AddWithValue("@TeamName", awayTeam);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"SQL Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}