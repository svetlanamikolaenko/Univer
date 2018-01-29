using Lib;
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
                listBox1.Items.Add((reader["Name"]).ToString());
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
            int lastId = 0;
            if (Manager.Managers.LastOrDefault() != null)
            {
                lastId = Manager.Managers.Last().ID;
            }
            new Manager(mainForm.GenerateAutoId(lastId, con, "Manager"), textBox5.Text);
            SaveToXML(Manager.Managers, "Managers.xml");
            SaveToDB();
            textBox5.Clear();
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
                    com.Parameters.AddWithValue("par1", man.ID);
                    com.Parameters.AddWithValue("par2", man.Name);
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
        
        private void delBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            var man = listBox1.SelectedItem.ToString();
            Manager.Managers.RemoveAll(r => r.Name == man);
            mainForm.DeleteFromDB(con, "Manager",man);
            con.Close();
            UpdateManagersList();
            SaveToXML(Manager.Managers, "Managers.xml");
            MessageBox.Show("Manager was deleted.");
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void editManBtn_Click(object sender, EventArgs e)
        {
            con.Open();
            var oldMan = listBox1.SelectedItem.ToString();
            int id=0;
            foreach (var par in Manager.Managers) {
                if (par.Name == oldMan)
                {
                    par.Name = textBox6.Text;
                    id = par.ID;
                }
            }

            mainForm.UpdateDB(con, "Manager", textBox6.Text, id);
            con.Close();
            UpdateManagersList();
            SaveToXML(Manager.Managers, "Managers.xml");
            textBox6.Clear();
            MessageBox.Show("Manager was updated.");
        }
    }
}
