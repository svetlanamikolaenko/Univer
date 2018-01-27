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
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\dd\Bank\Bank\Bankir.mdf");

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            UpdateClientList();
        }

        private void UpdateClientList()
        {
            listBox3.Items.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Client", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox3.Items.Add(Convert.ToString(reader["Name"]));
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
            // panel1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            listBox1.Items.Clear();
            //GenerateAutoId();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Manager", con);
            SqlDataReader readerMan = cmd.ExecuteReader();
            while (readerMan.Read())
            {
                listBox1.Items.Add(Convert.ToString(readerMan["Name"]));
            }
            con.Close();
            /*
            using (FileStream fs = new FileStream("c:\\dd\\Managers.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Manager));
                Manager newManager = (Manager)formatter.Deserialize(fs);

                listBox1.Items.Add(newManager.Name);
                
            }
            */
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }
        /*
        private void GenerateAutoId()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Count(Id) from Client", con);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            i++;
            label3.Text = i.ToString();
        }
        */
        private void SaveToDB() {
            con.Open();
            var last = Client.Clients.Last();
            foreach (var cl in Client.Clients)
            {
                if (cl.Equals(last))
                {
                    /*
                    SqlCommand cmd1 = new SqlCommand("Select Count(Id) from Manager", con);
                    int i = Convert.ToInt32(cmd1.ExecuteScalar());
                    i++;
                    */
                    SqlCommand com = new SqlCommand("INSERT INTO Client VALUES (@par1, @par2,                        @par3, '','')", con);
                    com.Parameters.AddWithValue("par1", mainForm.GenerateAutoId(con, "Client"));
                    com.Parameters.AddWithValue("par2", cl.Value.Name);
                    com.Parameters.AddWithValue("par3", cl.Value.Manager.Name);
                    com.ExecuteNonQuery();
                }

            }
            con.Close();
            UpdateClientList();
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

        private void button9_Click(object sender, EventArgs e)
        {
            /*
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Client values ('" + label3.Text + "','" + textBox5.Text + "', '"+listBox1.SelectedItem.ToString()+"','','')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            GenerateAutoId();
            UpdateClientList();
            */
            new Client(textBox5.Text,new Manager(listBox1.Text));
            SaveToDB();
            SaveToXML(Client.Clients, "Clients.xml");
            MessageBox.Show("Client was created successfully.");

            /*
            Client client = new Client(textBox5.Text, new Manager(listBox1.Text));
            XmlSerializer formatter = new XmlSerializer(typeof(Client));

            using (FileStream fs = new FileStream("c:\\dd\\Clients.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, client);
            }
            MessageBox.Show("Client was created successfully.");
            */
        }
    }
}
