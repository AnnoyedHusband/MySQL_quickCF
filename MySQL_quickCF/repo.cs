using MySql.Data.MySqlClient;

// -- -- -- -- -- -- -- -- -- --
using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace MySQL_quickCF
{
    public class Repo
    {
        // NOTE : perrasyti taip kad butu galima prisijungimo duomenis (con string, ip) suvesti is paneles
        // ---------------------------variables--------------------------------->//
        private static string server = "Server=127.0.0.1;";//  192.168.25.11   192.168.25.50      localhost
        private static string database = "database=rsp_test;";        // duombazes pavadinimas
        private static string uid = "uid=orkos2;";//operator;";//;   "uid=root";               // prisijungimo vardas
        private static string password = "pwd=orkos;";//Omysop04;";// ORK123;";//"pwd = slapt123";          // prisijungimo slaptažodis Omys01BB04
        private static string charset = "Charset=utf8;";

        public static string ConnectionString
        {
            get { return password + uid + database + server + charset; }
        }
        // <---------------------------variables----------------------------------//

        // ---------------------------Methods---------------------------------->//

        /// <summary>
        /// Tiesiogiai paleidzia komanda SQL serveryje.
        /// </summary>
        public void ExecuteDirect(string exeCommand)
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = Repo.ConnectionString;
                conn.Open();

                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = exeCommand;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void insertReport(float someValue, float somePercent, string Notes)
        {
            string exeCommand = "INSERT INTO brinemix(`someValue`, `somePercent`, `someNotes`) VALUES ( @someValue, @somePercent, @Notes)";
            try
            {

                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.ConnectionString = Repo.ConnectionString;
                    conn.Open();

                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("@someValue", someValue);
                        cmd.Parameters.AddWithValue("@somePercent", somePercent);
                        cmd.Parameters.AddWithValue("@Notes", Notes);

                        cmd.CommandText = exeCommand;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                throw;
            }
        }

        public List<Tasks> getTasks()
        {
            var list = new List<Tasks>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.ConnectionString = Repo.ConnectionString;
                    conn.Open();

                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT * FROM tasks WHERE 'isDone' = 0 ";

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var container = new Tasks
                                    (reader.GetInt16("ID"),
                                    reader.GetFloat("brineOrder"),
                                    reader.GetFloat("brinePerc"),
                                    reader.GetString("brineNotes"),
                                    reader.GetInt32("isDone")
                                    );
                                list.Add(container);
                            }
                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ToString());
                throw;
            }

        }

    }

    public class Tasks
    {
        public int ID { get; set; }
        public float BrineOrder { get; set; }
        public float BrinePerc { get; set; }
        public string BrineNotes { get; set; }
        public int IsDone { get; set; }

        public Tasks() : this(0, 0, 0, "", 0) { }

        public Tasks(int id, float brineOrder, float brinePerc, string brineNotes, int isDone)
        {
            ID = id;
            BrineOrder = brineOrder;
            BrinePerc = brinePerc;
            BrineNotes = brineNotes;
            IsDone = isDone;
        }

        public override string ToString()
        {
            return ID + " " + BrineOrder + " " + BrinePerc + " " + BrineNotes + " " + IsDone;
        }
    }
}
