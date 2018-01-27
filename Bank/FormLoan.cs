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
    public partial class FormLoan : Form
    {
        private mainForm _mainForm;
        public FormLoan()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\dd\Bank\Bank\Bankir.mdf");

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            UpdateLoanList();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Loan(textBox2.Text, Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text));
            SaveToXML(Loan.Loans, "Loans.xml");
            SaveToDB();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            MessageBox.Show("Loan was created.");

        }

        private void SaveToDB()
         {
            con.Open();
            var last = Loan.Loans.Last();
            foreach (var credit in Loan.Loans)
                {
                if (credit.Equals(last))
                {
                    SqlCommand localcommand = new SqlCommand("INSERT INTO LOAN VALUES (@par1, @par2, @par3, @par4, '')", con);
                    localcommand.Parameters.AddWithValue("par1", mainForm.GenerateAutoId(con, "LOAN"));
                    localcommand.Parameters.AddWithValue("par2", credit.Value.Name);
                    localcommand.Parameters.AddWithValue("par3", credit.Value.Percent);
                    localcommand.Parameters.AddWithValue("par4", credit.Value.Period);
                    localcommand.ExecuteNonQuery();
                }
               }
            con.Close();
            UpdateLoanList();
        }
        
        private void UpdateLoanList() {
            listBox1.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Loan", con);
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
