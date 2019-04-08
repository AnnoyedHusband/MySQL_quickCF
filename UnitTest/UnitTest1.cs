using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySQL_quickCF;
using MySql.Data.MySqlClient;

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

            try
            {
                r.getTasks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ToString());

                throw;
            }
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
        public void copyPasted()
        {
            string sql = "SELECT * FROM tasks;";
            MySqlConnection con = new MySqlConnection("server=127.0.0.1; uid=orkos2; pwd=orkos; database=rsp_test; Charset=utf8;");
            // con.ConnectionString = Repo.ConnectionString;

            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.ToString());
                throw;
            }

            // while (reader.Read())
            // {
            //     MessageBox.Show(reader.GetDecimal("brinePerc").ToString());
            // }

            con.Close();
        }
    }
}
