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
    public partial class StudentView : Form
    {
        Form1 f1;
        public StudentView(Form1 frm1)
        {
            InitializeComponent();
            this.f1 = frm1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          //  GetData("select S.Stud_ID AS ID,S.Name,S.Sec_ID AS SectionID,C.Name AS CourseName from Registration r,Students S,Courses C where C.C_ID = r.C_ID and S.Stud_ID = r.Stud_ID and S.Stud_ID = "+Convert.ToInt32(textBox1.Text)+"");
        }
        string conString = @"Data Source=DESKTOP-9JRPGU0\MARIAMSQL;Initial Catalog = CRS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlDataAdapter dataAdapter;
        DataTable table;
        private SqlConnection con;

        private void StudentView_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
            GetData("select S.Stud_ID AS ID,S.Name,S.Sec_ID AS SectionID,C.Name AS CourseName from Registration r,Students S,Courses C where C.C_ID = r.C_ID and S.Stud_ID = r.Stud_ID and S.Stud_ID = " + Convert.ToInt32(f1.textBox1.Text) + "");

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // GetData("select S.Stud_ID AS ID,S.Name,S.Sec_ID AS SectionID,C.Name AS CourseName from Registration r,Students S,Courses C where C.C_ID = r.C_ID and S.Stud_ID = r.Stud_ID and S.Stud_ID = " + Convert.ToInt32(f1.textBox1.Text) + "");
        }
    }
}
