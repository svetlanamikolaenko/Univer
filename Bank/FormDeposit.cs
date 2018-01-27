using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Bank
{
    public partial class FormDeposit : Form
    {
        public FormDeposit()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\dd\Bank\Bank\Bankir.mdf");

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Deposit(textBox5.Text, Int32.Parse(textBox6.Text), Int32.Parse(textBox7.Text));
            SaveToXML(Deposit.Deposits, "Deposits.xml");
            SaveToDB();
            MessageBox.Show("Deposit was created.");
        }

        private void SaveToDB()
        {
            con.Open();
            var last = Deposit.Deposits.Last();
            foreach (var dep in Deposit.Deposits)
            {
                if (dep.Equals(last))
                {
                    SqlCommand localcommand = new SqlCommand("INSERT INTO DEPOSIT VALUES (@par1, @par2, @par3, @par4, '')", con);
                    localcommand.Parameters.AddWithValue("par1", mainForm.GenerateAutoId(con, "DEPOSIT"));
                    localcommand.Parameters.AddWithValue("par2", dep.Value.Name);
                    localcommand.Parameters.AddWithValue("par3", dep.Value.Percent);
                    localcommand.Parameters.AddWithValue("par4", dep.Value.Period);
                    localcommand.ExecuteNonQuery();
                }
            }
            con.Close();
            UpdateLoanList();
        }

        private void UpdateLoanList()
        {
            listBox1.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Deposit", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(Convert.ToString(reader["Name"]));
            }
            con.Close();
        }

        private void SaveToXML(object obj, string fileName)
        {
            DataContractSerializer dcs = new DataContractSerializer(obj.GetType());
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.NewLineOnAttributes = true;
            XmlWriter xmlw = XmlWriter.Create(fileName, xmlWriterSettings);
            dcs.WriteObject(xmlw, obj);
            xmlw.Close();
        }
    }
}
