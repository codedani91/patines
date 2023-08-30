using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using K4os.Compression.LZ4.Streams.Adapters;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace patines
{
    public partial class Form1 : Form
    {
        string[] modelos = { "SERIE A", "PHOENIX","A CONNECTED","A CONNECTED MAX","S UNLIMITED","Z RED","Z DARK GREEN","Z BLUE" };
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < modelos.Length; i++)
            {
                comboBox1.Items.Add(modelos[i]);
                comboBox2.Items.Add(modelos[i]);
            }
            label3.Visible = false;
            comboBox2.Visible = false;
            button2.Visible = false;
            button2.Enabled = false;
            
            
            


        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                label3.Visible = false;
                comboBox2.Visible = false;
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
                button1.Enabled = true;
                button2.Visible = false;

            }
            else
            {
                label3.Visible = true;
                comboBox2.Visible = true;
                textBox1.Enabled = false;
                comboBox1.Enabled = false;
                button1.Enabled = false;
                button2.Visible = true;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string server = "localhost";
            string user = "root";
            string pass = "";
            string database = "errores";
            if (textBox1.Text == "" && comboBox1.Text == "")
            {
                MessageBox.Show("debes introducir los datos para buscar el error", "ERROR");
                textBox1.BackColor = Color.Red;
                comboBox1.BackColor = Color.Red;
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("introduce los datos", "ERROR");
                textBox1.BackColor = Color.Red;
            }
            else if (comboBox1.Text == "")
            {
                MessageBox.Show("elige un modelo", "ERROR");
                comboBox1.BackColor = Color.Red;
            }
            else
            {
                textBox1.BackColor = default;
                comboBox1.BackColor = default;
                MySqlConnection con = new MySqlConnection($"server={server};user={user};password={pass};database={database}");
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = $"SELECT ERROR,DESCRIPCION,SOLUCION FROM ERRORES WHERE ERROR='{textBox1.Text.ToUpper()}' AND MODELO='{comboBox1.Text}'";
                    cmd.Connection = con;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Form2 errores = new Form2();
                    errores.label1.Text = textBox1.Text.ToUpper();
                    errores.Text = "Errores display";
                    errores.dataGridView1.DataSource = dt;
                    errores.ShowDialog();
                    textBox1.Text = "";
                    comboBox1.Text = "";
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form2 erroresgen = new Form2();
            string server = "localhost";
            string user = "root";
            string password = "";
            string database = "errores";
            try
            {
                MySqlConnection con = new MySqlConnection($"server={server};user={user};password={password};database={database}");
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = $"SELECT * FROM ERRORES WHERE MODELO='{comboBox2.Text}'";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                erroresgen.dataGridView1.DataSource = dt;
                erroresgen.Text = "Errores patinete";
                erroresgen.label1.Visible = false;
                erroresgen.ShowDialog();
                comboBox2.Text = "";
                button2.Enabled = false;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;

        }
    }
}
