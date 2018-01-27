using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Bank
{
    public partial class FormManager : Form
    {
        public FormManager()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\dd\Bank\Bank\Bankir.mdf");
        

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            UpdateManagersList();
        }

        private void UpdateManagersList()
        {
            listBox1.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Manager", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(Convert.ToString(reader["Name"]));
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //panel1.Visible = true;
            panel2.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            //panel2.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            //panel1.Visible = false;
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            /*
            Manager managerName = new Manager(textBox1.Text);
            XmlSerializer formatter = new XmlSerializer(typeof(Manager));

            using (FileStream fs = new FileStream("c:\\dd\\Managers.xml", FileMode.Append, FileAccess.Write))
            {
                formatter.Serialize(fs, managerName);
            }
          
            MessageBox.Show("New Manager created successfully.");
            */
            panel3.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void createManBtn_Click(object sender, EventArgs e)
        {
            new Manager(textBox5.Text);
            SaveToXML(Manager.Managers, "Managers.xml");
            SaveToDB();
            MessageBox.Show("Manager was added successfully.");
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

        private void SaveToDB()
        {
            con.Open();
            var last = Manager.Managers.Last();
            foreach (var man in Manager.Managers)
            {
                if (man.Equals(last))
                {
                    /*
                    SqlCommand cmd1 = new SqlCommand("Select Count(Id) from Manager", con);
                    int i = Convert.ToInt32(cmd1.ExecuteScalar());
                    i++;
                    */
                    SqlCommand com = new SqlCommand("INSERT INTO Manager VALUES (@par1, @par2)", con);
                    com.Parameters.AddWithValue("par1", mainForm.GenerateAutoId(con, "Manager"));
                    com.Parameters.AddWithValue("par2", man.Value.Name);
                    com.ExecuteNonQuery();
                }

            }
            con.Close();
            UpdateManagersList();
        }

        /*
private void GenerateAutoId() {
   con.Open();
   SqlCommand cmd = new SqlCommand("Select Count(Id) from Manager", con);
   int i = Convert.ToInt32(cmd.ExecuteScalar());
   con.Close();
   i++;
   label5.Text = i.ToString();
}
*/
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //GenerateAutoId();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void searchManBtn_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Client where Manager_Name = '"+searchField.Text+"'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox2.Items.Add(Convert.ToString(reader["Name"]));
            }
            con.Close();
        }
    }
}
