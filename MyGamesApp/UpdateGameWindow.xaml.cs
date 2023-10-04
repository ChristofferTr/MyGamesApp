using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace MyGamesApp
{
    public partial class UpdateGameWindow : Window
    {
        private MySqlConnection connection;

        public UpdateGameWindow(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;

          
            LoadGames();
        }

        private void LoadGames()
        {
           
            var games = GetGamesFromDatabase();

           
            gameComboBox.ItemsSource = games;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameComboBox.SelectedItem is Game selectedGame)
            {
                string gameNameToUpdate = selectedGame.GameName;
                string newGameName = newGameNameTextBox.Text;
                string newPublisher = newPublisherTextBox.Text;
                int newPlayed = newPlayedCheckBox.IsChecked ?? false ? 1 : 0;

               
                UpdateGame(gameNameToUpdate, newGameName, newPublisher, newPlayed);

               
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a game to update.");
            }
        }


        private List<Game> GetGamesFromDatabase()
        {
            List<Game> games = new List<Game>();

         
            using (MySqlConnection conn = new MySqlConnection(connection.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string sqlQuery = "SELECT game_id, genre_id, game_name, publisher, played FROM my_games"; // Include genre_id in the query
                    MySqlCommand command = new MySqlCommand(sqlQuery, conn);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int gameId = reader.GetInt32("game_id");
                            int genreId = reader.GetInt32("genre_id");
                            string gameName = reader.GetString("game_name");
                            string publisher = reader.GetString("publisher");
                            int played = reader.GetInt32("played");

                            games.Add(new Game
                            {
                                GameId = gameId,
                                GenreId = genreId,
                                GameName = gameName,
                                Publisher = publisher,
                                Played = played
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return games;
        }

       
        private void UpdateGame(string gameNameToUpdate, string newGameName, string newPublisher, int newPlayed)
        {
            using (MySqlConnection conn = new MySqlConnection(connection.ConnectionString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("UpdateGame", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@gameNameToUpdate", gameNameToUpdate);
                command.Parameters.AddWithValue("@newGameName", newGameName);
                command.Parameters.AddWithValue("@newPublisher", newPublisher);
                command.Parameters.AddWithValue("@newPlayed", newPlayed);
                command.ExecuteNonQuery();
            }
        }
    }
}