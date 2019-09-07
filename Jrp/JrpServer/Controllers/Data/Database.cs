using CitizenFX.Core;
using JrpShared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static JrpShared.Data.Serialization;

namespace JrpServer.Controllers.Data
{
    sealed class Database
    {
        private string ConnectionString;

        public Database()
        {
            ConnectionString = GetConnectionString();

            ExecuteNonQuery("CREATE TABLE IF NOT EXISTS `User` (Id INTEGER PRIMARY KEY, LicenseId TEXT);");
            ExecuteNonQuery("CREATE TABLE IF NOT EXISTS `Character` (Id INTEGER PRIMARY KEY, Name TEXT, Cash INTEGER, Credit INTEGER, Job TEXT, Items TEXT, Skin TEXT, UserId INTEGER UNIQUE, FOREIGN KEY (UserId) REFERENCES User(Id));");
        }

        public object ExecuteScalar(string query)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                return new SQLiteCommand(query, connection).ExecuteScalar();
            }
        }

        public DataTable GetDataTable(string query)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                DataTable dataTable = new DataTable();

                new SQLiteDataAdapter(new SQLiteCommand(query, connection)).Fill(dataTable);

                return dataTable;
            }
        }

        public int FetchUserId(Player player)
        {
            object result = ExecuteScalar($"SELECT Id FROM `User` WHERE Licenseid = '{player.Identifiers["license"]}';");

            if (result != null)
                return Convert.ToInt32(result);
            else
                return 0;
        }

        public int FetchCharacterId(int userId)
        {
            object result = ExecuteScalar($"SELECT Id FROM `Character` WHERE UserId = '{userId}';");

            if (result != null)
                return Convert.ToInt32(result);
            else
                return 0;
        }

        public int ExecuteNonQuery(string query)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                return new SQLiteCommand(query, connection).ExecuteNonQuery();
            }
        }

        public void RegisterNewUser(Player player) => ExecuteNonQuery($"INSERT INTO `User` (LicenseId) VALUES ('{player.Identifiers["license"]}');");

        public void RegisterNewCharacter(int userId, ICharacter character) => ExecuteNonQuery($"INSERT INTO `Character` (Name, Cash, Credit, Job, Items, Skin, UserId) VALUES ('{SerializeObject(character.Name)}', '{SerializeObject(character.Cash)}', '{SerializeObject(character.Credit)}', '{SerializeObject(character.Job)}', '{SerializeObject(character.Inventory)}', '{SerializeObject(character.Skin)}', '{userId}');");

        public ICharacter FetchCharacter(int userId)
        {
            DataRow row = GetDataTable($"SELECT * FROM `Character` WHERE UserId = '{userId}'").Select().First();

            return new Character(row["Name"].ToString(), Convert.ToUInt32(row["Cash"]), Convert.ToUInt32(row["Credit"]), DeserializeObject<IJob>(row["Job"].ToString()), DeserializeObject<ICollection<IItem>>(row["Items"].ToString()), DeserializeObject<Skin>(row["Skin"].ToString()));
        }

        private string GetConnectionString()
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder
            {
                DataSource = "Database.jrp"
            };

            return builder.ConnectionString;
        }
    }
}
