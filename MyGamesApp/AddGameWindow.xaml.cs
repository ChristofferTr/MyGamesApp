using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace MyGamesApp
{
    /// <summary>
    /// Interaction logic for AddGameWindow.xaml
    /// </summary>
    public partial class AddGameWindow : Window
    {
        private MySqlConnection connection;
        public AddGameWindow(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string gameName = gameNameTextBox.Text;
            string publisher = publisherTextBox.Text;
            int played = playedCheckBox.IsChecked ?? false ? 1 : 0;

            AddNewGameToDatabase(gameName, publisher, played);

            Close(); // Close the Add Game window after saving
        }

        private void AddNewGameToDatabase(string gameName, string publisher, int played)
        {
            MySqlCommand command = new MySqlCommand("AddGame", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@gameName", gameName);
            command.Parameters.AddWithValue("@publisher", publisher);
            command.Parameters.AddWithValue("@played", played);

            command.ExecuteNonQuery();

        }

        private void UpdateGameInDatabase(string gameNameToUpdate, string newGameName, string newPublisher, int newPlayed)
        {
            MySqlCommand command = new MySqlCommand("UpdateGame", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@gameNameToUpdate", gameNameToUpdate);
            command.Parameters.AddWithValue("@newGameName", newGameName);
            command.Parameters.AddWithValue("@newPublisher", newPublisher);
            command.Parameters.AddWithValue("@newPlayed", newPlayed);

            command.ExecuteNonQuery();
        }


    }
}
