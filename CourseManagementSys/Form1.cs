using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CourseManagementSys
{
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
          
        }
       // string conString = @"Data Source=DESKTOP-9JRPGU0\MARIAMSQL;Initial Catalog = CRS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9JRPGU0\MARIAMSQL;Initial Catalog = CRS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("select password from Login where username='" + textBox1.Text + "' and usertype='" + comboBox1.SelectedItem.ToString().Trim() + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string pass = dt.Rows[0][0].ToString();
            string cmbItemValue = comboBox1.SelectedText;
            if (comboBox1.SelectedIndex == 0) {
                if (pass == textBox2.Text) {
                    AdminView av = new AdminView();
                    av.Show();
                }

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                if (pass == textBox2.Text)
                {
                    StudentView av = new StudentView(this);
                    av.Show();
                }

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                if (pass == textBox2.Text)
                {
                    InstructorView av = new InstructorView();
                    av.Show();
                }

            }

        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
