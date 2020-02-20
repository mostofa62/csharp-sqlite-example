using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace SqlliteExample
{
    class Program
    {
        
        static void Main(string[] args)
        {

            string cs = @"Data Source="+Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)+"\\info.db";
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "DROP TABLE IF EXISTS login";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE login(id INTEGER PRIMARY KEY,
                    name TEXT, password TEXT)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO login(name, password) VALUES(@name, @password)";
                    cmd.Parameters.AddWithValue("@name", "mostofa");
                    cmd.Parameters.AddWithValue("@password", "stupid");
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("row inserted");
                    cmd.CommandText = "SELECT * FROM login LIMIT 1";
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)}");
                        }
                    }
                }
            }
            Console.ReadLine();

        }
    }
}
