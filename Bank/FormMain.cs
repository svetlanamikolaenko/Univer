using Base;
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
            SqlCommand cmdLoan = new SqlCommand("select * from Loan", con);
            SqlDataReader readerLoan = cmdLoan.ExecuteReader();
            while (readerLoan.Read())
            {
                new Loan(Convert.ToString(readerLoan["Name"]), Convert.ToInt32(readerLoan["Percent"]),
                    Convert.ToInt32(readerLoan["Period"]));
            }
            readerLoan.Close();
            SqlCommand cmdMan = new SqlCommand("select * from Manager", con);
            SqlDataReader readerMan = cmdMan.ExecuteReader();
            while (readerMan.Read())
            {
                new Manager(Convert.ToString(readerMan["Name"]));
            }
            readerMan.Close();
            SqlCommand cmdDep = new SqlCommand("select * from Deposit", con);
            SqlDataReader readerDep = cmdDep.ExecuteReader();
            while (readerDep.Read())
            {
                new Deposit(Convert.ToString(readerDep["Name"]), Convert.ToInt32(readerDep["Percent"]),
                    Convert.ToInt32(readerDep["Period"]));
            }

            readerDep.Close();
            con.Close();
        }
        public static int GenerateAutoId(SqlConnection con, string tableName)
        {

            SqlCommand cmd = new SqlCommand("Select Count(Id) from " + tableName, con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            i++;
            return i;
        }
    }
}
