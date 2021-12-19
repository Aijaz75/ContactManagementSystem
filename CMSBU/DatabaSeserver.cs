using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using Finisar.SQLite;
using System.IO;

namespace CMSBU
{

    class DatabaSeserver
    {
        // We use these three SQLite objects:
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;
        public int RowCount = 0;
        public DatabaSeserver()
        {
            if (!File.Exists("database.db"))
            {

                // [snip] - As C# is purely object-oriented the following lines must be put into a class:
                // We use these three SQLite objects:
                SQLiteConnection sqlite_conn;
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;
                // create a new database connection:
                sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
                // open the connection:
                sqlite_conn.Open();
                // create a new SQL command:
                sqlite_cmd = sqlite_conn.CreateCommand();
                // Let the SQLiteCommand object know our SQL-Query:
                sqlite_cmd.CommandText = "CREATE TABLE User (Userid INTEGER AUTOINREMENT UNIQUE PRIMARY KEY NOT NULL,Username STRING (0, 30)  NOT NULL,Password STRING (1, 255) NOT NULL);";
                // Now lets execute the SQL ;D
                sqlite_cmd.ExecuteNonQuery();
                // create a new SQL command:
                sqlite_cmd = sqlite_conn.CreateCommand();
                // Let the SQLiteCommand object know our SQL-Query:
                sqlite_cmd.CommandText = "CREATE TABLE Contacts (name STRING (0, 20) NOT NULL, fathername  STRING (0, 20), gander STRING (0, 6) NOT NULL, address STRING (0, 40) NOT NULL, city STRING (0, 15) NOT NULL, company STRING (0, 30) NOT NULL, designation STRING (0, 20) NOT NULL, email STRING (0, 20) NOT NULL UNIQUE, contact1 STRING (0, 20) UNIQUE PRIMARY KEY, contact2 STRING (0, 20) UNIQUE, contact3 STRING (0, 20) UNIQUE);"; // Now lets execute the SQL ;D
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.CommandText = "select * from User;";
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                {
                    RowCount++;

                }
                MessageBox.Show("Database Successfully Generated" + RowCount);
                sqlite_conn.Close();

            }

        }
        public void adduser(string username, string password)
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=False;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM User";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                RowCount = int.Parse(sqlite_datareader.GetValue(0).ToString());

            }
            sqlite_datareader.Close();
           // if (RowCount <= 0)
           // {
            try
            {
                string cmd = "INSERT into User VALUES(" +(RowCount + 1) + ",'" + username + "','" + password + "');";
                sqlite_cmd.CommandText = cmd;
                // Now lets execute the SQL ;D
                sqlite_cmd.ExecuteNonQuery();
                sqlite_conn.Close();
                MessageBox.Show("Your account is generated");

            }
            catch (SQLiteException e) { MessageBox.Show(e.ToString()); }
                // }

        }

        public int rowcounter()
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=False;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM User";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                RowCount++;

            }
            sqlite_datareader.Close();
            sqlite_conn.Close();
            return RowCount;
        }
        public int cridentals(string username, string password)
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=False;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM User where Username=='" + username + "' and password=='" + password + "';";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                RowCount++;

            }
            sqlite_datareader.Close();
            sqlite_conn.Close();
            return RowCount;

        }
        public SQLiteDataReader dgwdata(string statementt)
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=False;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = statementt;
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            return sqlite_datareader;

        }

        internal void savecontacts(string statement)
        {
            try
            {
                sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=False;");
                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = statement;
                // Now lets execute the SQL ;D
                sqlite_cmd.ExecuteNonQuery();
                sqlite_conn.Close();
                MessageBox.Show("Contact is stored inside database");
            }
            catch (SQLiteException e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        public void commandexecutor(string cmd)
        {

            try
            {
                sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=False;Compress=False;");
                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                // Now lets execute the SQL ;D
                sqlite_cmd.ExecuteNonQuery();
                sqlite_conn.Close();

            }
            catch (SQLiteException e)
            {
                MessageBox.Show(e.ToString());
            }
        }




    }
}