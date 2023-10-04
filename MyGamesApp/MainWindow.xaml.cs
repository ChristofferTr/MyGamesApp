/*using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace MyGamesApp
{
    public partial class MainWindow : Window
    {
        private MySqlConnection connection;
        private List<Game> games;

        private string connectionString = "Server=localhost;Database=mygamesdatabase;User=root;Password=HorizonAloy1234!;";

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            LoadGameData();
        }

        private void InitializeDatabaseConnection()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        private void LoadGameData()
        {
            string sqlQuery = "SELECT game_id, game_name, publisher, played FROM my_games";
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            games = new List<Game>(); // Initialize the games list

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int gameId = reader.GetInt32("game_id");
                    string gameName = reader.GetString("game_name");
                    string publisher = reader.GetString("publisher");
                    int played = reader.GetInt32("played");

                    games.Add(new Game { GameId = gameId, GameName = gameName, Publisher = publisher, Played = played });
                }
            }

            gameDataGrid.ItemsSource = games;
        }

        private void ShowOwnedGames_Click(object sender, RoutedEventArgs e)
        {
            LoadFilteredGameData(1, null);
        }

        private void ShowPlayedGames_Click(object sender, RoutedEventArgs e)
        {
            LoadFilteredGameData(null, 1);
        }

        private void ShowGamesNotPlayed_Click(object sender, RoutedEventArgs e)
        {
            LoadFilteredGameData(null, 0);
        }

        private void LoadFilteredGameData(int? owned, int? played)
        {
            games.Clear();

            string query = "SELECT game_id, game_name, publisher, played FROM my_games WHERE 1=1";

            if (owned.HasValue)
            {
                query += $" AND owned = {owned}";
            }

            if (played.HasValue)
            {
                query += $" AND played = {played}";
            }

            MySqlCommand command = new MySqlCommand(query, connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int gameId = reader.GetInt32("game_id");
                    string gameName = reader.GetString("game_name");
                    string publisher = reader.GetString("publisher");
                    int gamePlayed = reader.GetInt32("played");

                    games.Add(new Game { GameId = gameId, GameName = gameName, Publisher = publisher, Played = gamePlayed });
                }
            }

            gameDataGrid.ItemsSource = games;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connection.Close();
        }

        private void UpdateGame(string gameNameToUpdate, string newGameName, string newPublisher, int newPlayed)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("UpdateGame", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@gameNameToUpdate", gameNameToUpdate);
                command.Parameters.AddWithValue("@newGameName", newGameName);
                command.Parameters.AddWithValue("@newPublisher", newPublisher);
                command.Parameters.AddWithValue("@newPlayed", newPlayed);
                command.ExecuteNonQuery();
            }
        }

        private void UpdatePlayedStatus_Click(object sender, RoutedEventArgs e)
        {
            List<Game> allGames = gameDataGrid.ItemsSource as List<Game>;

            if (allGames == null || allGames.Count == 0)
            {
                MessageBox.Show("There are no games available.");
                return;
            }

            UpdatePlayedStatusWindow updateWindow = new UpdatePlayedStatusWindow(allGames, this);
            if (updateWindow.ShowDialog() == true)
            {
                int playedValue = updateWindow.Played ? 1 : 0;
                int selectedGameId = updateWindow.SelectedGameId;

                UpdateGame(selectedGameId, playedValue);

                LoadGameData();
            }
        }

        // Additional methods and event handlers can be added here.
    }
}*/