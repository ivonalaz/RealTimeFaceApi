using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace RealTimeFaceApi.DataAccess
{
    public static class DbConnection
    {
        private static SQLiteConnection sqlite_conn;
       
        public static SQLiteConnection CreateConnection()
        {
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");

            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception)
            {
                throw;
            }
            return sqlite_conn;
        }

        public static void CreateTable()
        {

            SQLiteCommand sqlite_cmd;
            //string Createsql = "CREATE TABLE SampleTable(Col1 VARCHAR(20), Col2 INT)";
            string Createsql = "CREATE TABLE Emotions(Id VARCHAR(50), EmotionName VARCHAR(50), Value REAL)";
            //string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
            //sqlite_cmd.CommandText = Createsql1;
            //sqlite_cmd.ExecuteNonQuery();

        }

        public static void InsertData(KeyValuePair<string,double> summarizedEmotion)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            var id = Guid.NewGuid();
            sqlite_cmd.CommandText = $"INSERT INTO Emotions(Id, EmotionName, Value) VALUES('{id}', '{summarizedEmotion.Key}',{summarizedEmotion.Value}); ";
            sqlite_cmd.ExecuteNonQuery();
            //sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2); ";
            //sqlite_cmd.ExecuteNonQuery();
            //sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3); ";
            //sqlite_cmd.ExecuteNonQuery();


            //sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3); ";
            //sqlite_cmd.ExecuteNonQuery();

        }

        public static void ReadData()
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            sqlite_conn.Close();
        }
    }
}
