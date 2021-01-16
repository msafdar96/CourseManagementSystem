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

namespace CourseManagementSys
{
    public partial class InstructorView : Form
    {
        public InstructorView()
        {
            InitializeComponent();
        }
        string conString = @"Data Source=DESKTOP-9JRPGU0\MARIAMSQL;Initial Catalog = CRS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlDataAdapter dataAdapter;
        DataTable table;
        private SqlConnection con;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData("select I.Ins_ID AS ID,I.Name,C.Name AS CourseName,C.Sec_ID AS SectionID from Ins_Reg ir,Instructors I,Courses C where C.C_ID = ir.C_ID and I.Ins_ID = ir.Ins_ID and I.Ins_ID = " + Convert.ToInt32(textBox1.Text) + "");
        }

        private void InstructorView_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            dataGridView1.DataSource = bindingSource1;
        }
        private void GetData(string selectCommand)
        {
            try
            {
                dataAdapter = new SqlDataAdapter(selectCommand, conString);
                table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;
                dataGridView1.Columns[0].ReadOnly = true;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
