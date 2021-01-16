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
    public partial class InstructorCourses : Form
    {
        public InstructorCourses()
        {
            InitializeComponent();
        }
        string conString = @"Data Source=DESKTOP-9JRPGU0\MARIAMSQL;Initial Catalog = CRS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlDataAdapter dataAdapter;
        DataTable table;
        private SqlConnection con;
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void InstructorCourses_Load(object sender, EventArgs e)
        {
            label6.BackColor = Color.Transparent;
            dataGridView1.DataSource = bindingSource1;
            GetData("Select * from Ins_Reg");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command;

            string insert = @"insert into Ins_Reg(Reg_ID,Ins_ID,C_ID)
                   values(@Reg_ID,@Ins_ID,@C_ID)";
            using (SqlConnection conn = new SqlConnection(conString))
            {
                try
                {
                    conn.Open();
                    command = new SqlCommand(insert, conn);

                    command.Parameters.AddWithValue(@"Reg_ID", Convert.ToInt32(Registration_ID.Text));
                    command.Parameters.AddWithValue(@"Ins_ID", Convert.ToInt32(Ins_ID.Text));
                    command.Parameters.AddWithValue(@"C_ID", Convert.ToInt32(Course_ID.Text));
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                GetData("Select * from Ins_Reg");

                dataGridView1.Update();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            try
            {
                bindingSource1.EndEdit();
                dataAdapter.Update(table);
                MessageBox.Show("Update Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {

                case "RegistrationID":

                    GetData("select * from Ins_Reg where Reg_ID like '%" + Registration_ID.Text.ToLower() + "%'");
                    break;

                case "InstructorID":

                    GetData("select * from Ins_Reg where Ins_ID like '%" + Ins_ID.Text.ToLower() + "%'");

                    break;



            }
        }
    }
}
