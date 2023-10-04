using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesApp
{
    public class DatabaseManager
    {
        private string connectionString;

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void UpdateGame(string gameNameToUpdate, string newGameName, string newPublisher, int? newPlayed)
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

       
    }
}
