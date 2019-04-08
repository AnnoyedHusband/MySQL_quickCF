using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySQL_quickCF;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExecuteDirectTest()
        {
            Repo r = new Repo();

            try
            {
                // r.ExecuteDirect("INSERT into brinemix(`someValue`, `somePercent`) VALUES (123, 12)");
                r.ExecuteDirect("INSERT into brinemix(`someValue`, `somePercent`, `someNotes`) VALUES (123, 12, \"arse\")");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ToString());

                throw;
            }
        }

        [TestMethod]
        public void GetTasksTest()
        {
            Repo r = new Repo();
            
            r.getTasks();
        }

        [TestMethod]
        public void InsertReportTest()
        {
            Repo r = new Repo();

            try
            {
                r.insertReport(12, 12, "eureka");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ToString());

                throw;
            }
        }

        [TestMethod]
        public void RetrieveVersionInfo()
        {
            string sql = "SELECT version();";
            MySqlConnection con = new MySqlConnection("server=127.0.0.1; uid=orkos2; pwd=orkos; database=rsp_test; Charset=utf8;");
            // con.ConnectionString = Repo.ConnectionString;

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MessageBox.Show(reader.GetString("version()"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ToString());
                throw;
            }

            con.Close();
        }

        [TestMethod]
        public void GetTasksDisectedTest()
        {
            List<Tasks> list = new List<Tasks>();

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
                                Tasks container = new Tasks
                                    (reader.GetInt16("ID"),
                                    reader.GetFloat("brineOrder"),
                                    reader.GetFloat("brinePerc"),
                                    reader.GetString("brineNotes"),
                                    reader.GetByte("isDone"),
                                    reader.GetDateTime("timeStamp")
                                    );
                                list.Add(container);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ToString());
                throw;
            }


        }
    }
}


