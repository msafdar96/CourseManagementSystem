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
    public partial class course : Form
    {
        public course()
        {
            InitializeComponent();
        }
        string conString = @"Data Source=DESKTOP-9JRPGU0\MARIAMSQL;Initial Catalog = CRS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlDataAdapter dataAdapter;
        DataTable table;
        private SqlConnection con;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command;

            string insert = @"insert into Courses(C_ID, Name, Credit_Hrs, Ins_ID,Sec_ID,Sem_ID)

            values(@C_ID, @Name, @Credit_Hrs,@Ins_ID,@Sec_ID,@Sem_ID)";

            using (SqlConnection conn = new SqlConnection(conString))
            {
                try
                {
                    conn.Open();
                    command = new SqlCommand(insert, conn);

                    command.Parameters.AddWithValue(@"C_ID", Convert.ToInt32(Course_ID.Text));
                    command.Parameters.AddWithValue(@"Name", c_name.Text);
                    command.Parameters.AddWithValue(@"Credit_Hrs", Convert.ToInt32(Credit_Hours.Text));
                    command.Parameters.AddWithValue(@"Ins_ID", Convert.ToInt32(Instructor_ID.Text));
                    command.Parameters.AddWithValue(@"Sec_ID", Convert.ToInt32(Section_ID.Text));
                    command.Parameters.AddWithValue(@"Sem_ID", Convert.ToInt32(Semester_ID.Text));
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            GetData("Select * from Courses");

            dataGridView1.Update();
        }

        private void Course_Load(object sender, EventArgs e)
        {
            label6.BackColor = Color.Transparent;
            dataGridView1.DataSource = bindingSource1;
            GetData("Select * from Courses");
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

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentCell.OwningRow;

            string value = row.Cells["C_ID"].Value.ToString();


            string Name = row.Cells["Name"].Value.ToString();
            string Phone = row.Cells["Credit_Hrs"].Value.ToString();

            string Address = row.Cells["Ins_ID"].Value.ToString();
            string Total_Credit = row.Cells["Sec_ID"].Value.ToString();
            string Dept_ID = row.Cells["Sem_ID"].Value.ToString();

            DialogResult result = MessageBox.Show("Do you really want to delete " +Name + " " + Credit_Hours + " " + Instructor_ID + " " + Section_ID + " " + Semester_ID + " " + ", record " + value, "Message",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question);



            string deleteState = @"Delete from Courses where C_ID = '" + value + "'";


            if (result == DialogResult.Yes)

            {

                using (con = new SqlConnection(conString))

                {

                    try

                    {

                        con.Open();
                        SqlCommand comm = new SqlCommand(deleteState, con);

                        comm.ExecuteNonQuery();
                        GetData("Select * from Courses");
                        dataGridView1.Update();

                    }

                    catch (Exception ex)

                    {

                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {

                case "C_ID":

                    GetData("select * from Courses where C_ID like '%" + Course_ID.Text.ToLower() + "%'");
                    break;

                case "Name":

                    GetData("select * from Courses where Name like '%" + c_name.Text.ToLower() + "%'");

                    break;



            }

        }
    }
}
