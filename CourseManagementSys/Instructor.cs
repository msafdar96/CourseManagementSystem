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
    public partial class Instructor : Form
    {
        public Instructor()
        {
            InitializeComponent();
        }
        string conString = @"Data Source=DESKTOP-9JRPGU0\MARIAMSQL;Initial Catalog = CRS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlDataAdapter dataAdapter;     
        DataTable table;                
        private SqlConnection con;

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void _Instructor_Load(object sender, EventArgs e)
        {
            
            label2.BackColor = Color.Transparent;
           
            dataGridView1.DataSource = bindingSource1;
            GetData("Select * from Instructors");
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command;

            string insert = @"insert into Instructors(Ins_ID, Name, Phone, Email,Dept_ID)

            values(@Ins_ID,@Name,@Phone,@Email,@Dept_ID)";

            using (SqlConnection conn = new SqlConnection(conString))           
            {
                try
                {
                    conn.Open();                    
                    command = new SqlCommand(insert, conn);

                    command.Parameters.AddWithValue(@"Ins_ID", Convert.ToInt32(Instructor_ID.Text));
                    command.Parameters.AddWithValue(@"Name", textBox2.Text);                  
                    command.Parameters.AddWithValue(@"Phone", textBox3.Text);
                    command.Parameters.AddWithValue(@"Email", Email.Text);                                
                    command.Parameters.AddWithValue(@"Dept_ID", Convert.ToInt32(Dept.Text));
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);            
                }
            }
            GetData("Select * from Instructors");

            dataGridView1.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentCell.OwningRow;

            string value = row.Cells["Ins_ID"].Value.ToString();


            string Name = row.Cells["Name"].Value.ToString();
            string Phone = row.Cells["Phone"].Value.ToString();

            string Email = row.Cells["Email"].Value.ToString();
            
            string Dept_ID = row.Cells["Dept_ID"].Value.ToString();
           
            DialogResult result = MessageBox.Show("Do you really want to delete " + Name + " " + Phone + " " + Email + " " + Dept_ID  + " " + ", record " + value, "Message",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question);



            string deleteState = @"Delete from Instructors where Ins_ID = '" + value + "'";


            if (result == DialogResult.Yes)

            {

                using (con = new SqlConnection(conString))

                {

                    try

                    {

                        con.Open();
                        SqlCommand comm = new SqlCommand(deleteState, con);

                        comm.ExecuteNonQuery();
                        GetData("Select * from Instructors");
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

                case "Instructor ID":

                    GetData("select * from Instructors where Ins_ID like '%" + Instructor_ID.Text.ToLower() + "%'");
                    break;

                case "Name":

                    GetData("select * from Instructors where Name like '%" + textBox2.Text.ToLower() + "%'");

                    break;



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

        private void Phone_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
