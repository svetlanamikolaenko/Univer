using Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            LoadDataFromDB();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\dd\Bank\Bank\Bankir.mdf");

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void managerBtn_Click(object sender, EventArgs e)
        {
            FormManager managerForm = new FormManager();
            managerForm.Show();
        }

        private void clientBtn_Click(object sender, EventArgs e)
        {
            FormClient clientForm = new FormClient();
            clientForm.Show();
        }

        private void creditBtn_Click(object sender, EventArgs e)
        {
            FormLoan creditForm = new FormLoan();
            creditForm.Show();
        }

        private void depositBtn_Click(object sender, EventArgs e)
        {
            FormDeposit depositForm = new FormDeposit();
            depositForm.Show();
        }
        
        private void LoadDataFromDB() {
            con.Open();
            SqlCommand cmdMan = new SqlCommand("select * from Manager", con);
            SqlDataReader readerMan = cmdMan.ExecuteReader();
            while (readerMan.Read())
            {
                new Manager(Int32.Parse(readerMan["ID"].ToString()), readerMan["Name"].ToString());
            }
            readerMan.Close();
            SqlCommand cmdCl = new SqlCommand("select * from Client", con);
            SqlDataReader readerCl = cmdCl.ExecuteReader();
            while (readerCl.Read())
            {
                new Client(Int32.Parse(readerCl["ID"].ToString()), readerCl["Name"].ToString(), readerCl["Manager_Name"].ToString());
            }
            readerCl.Close();
            
            SqlCommand cmdLoan = new SqlCommand("select * from Loan", con);
            SqlDataReader readerLoan = cmdLoan.ExecuteReader();
            while (readerLoan.Read())
            {
                new Loan(Int32.Parse(readerLoan["Id"].ToString()), readerLoan["Name"].ToString(),
                    Int32.Parse(readerLoan["Procent"].ToString()), Int32.Parse(readerLoan["Period"].ToString()));
            }
            readerLoan.Close();
            
            SqlCommand cmdDep = new SqlCommand("select * from Deposit", con);
            SqlDataReader readerDep = cmdDep.ExecuteReader();
            while (readerDep.Read())
            {
                new Deposit(Int32.Parse(readerDep["Id"].ToString()), readerDep["Name"].ToString(),
                    Int32.Parse(readerDep["Procent"].ToString()), Int32.Parse(readerDep["Period"].ToString()));
            }

            readerDep.Close();
            
            con.Close();
        }
        
        public static int GenerateAutoId(int c, SqlConnection con, string tableName)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Count(Id) from " + tableName, con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            i++;
            while (i <= c)
                i++;
            con.Close();
            return i;
        }
/*
        public static int GenerateAutoId()
        {
            int i = Manager.Managers.Count();
            i++;
            return i;
        }
*/
        internal static void DeleteFromDB(SqlConnection con, string tableName, string value)
        {
            SqlCommand com = new SqlCommand("DELETE FROM " + tableName + " where Name = '" + value + "'", con);
            com.ExecuteNonQuery();
        }

        internal static void UpdateDB(SqlConnection con, string tableName, string name, int id)
        {
            SqlCommand com = 
                new SqlCommand("UPDATE " + tableName + " SET Name = '" + name + "' Where ID = " +id, con);
            com.ExecuteNonQuery();
        }

        internal static void UpdateDB(SqlConnection con, string tableName, string name, int procent, int period, int id)
        {
            SqlCommand com =
                new SqlCommand("UPDATE " + tableName + " SET Name = '" + name + "', Procent = " + procent + ", Period = " + period + " Where ID = " + id, con);
            com.ExecuteNonQuery();
        }
    }
}
