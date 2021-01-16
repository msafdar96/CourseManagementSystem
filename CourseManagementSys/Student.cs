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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }
        string conString = @"Data Source=DESKTOP-9JRPGU0\MARIAMSQL;Initial Catalog = CRS; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlDataAdapter dataAdapter;     
        DataTable table;                
        private SqlConnection con;

        private void Student_Load(object sender, EventArgs e)
        {
            label6.BackColor = Color.Transparent;
            dataGridView1.DataSource = bindingSource1;
            GetData("Select * from Students");
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

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlCommand command;

            string insert = @"insert into Students(Stud_ID, Name, Phone, Address,Total_Credit,Dept_ID,Sec_ID)

            values(@Stud_ID, @Name, @Phone,@Address,@Total_Credit,@Dept_ID,@Sec_ID)";

            using (SqlConnection conn = new SqlConnection(conString))            
            {
                try
                {
                    conn.Open();                   
                    command = new SqlCommand(insert, conn); 

                    command.Parameters.AddWithValue(@"Stud_ID", Convert.ToInt32(Student_ID.Text));
                    command.Parameters.AddWithValue(@"Name", s_Name.Text);                    
                    command.Parameters.AddWithValue(@"Phone", Phone.Text);
                    command.Parameters.AddWithValue(@"Address", Address.Text);                               
                    command.Parameters.AddWithValue(@"Total_Credit", Convert.ToInt32(Total_Credit.Text));                	
                    command.Parameters.AddWithValue(@"Dept_ID",Convert.ToInt32( Dept_ID.Text));
                    command.Parameters.AddWithValue(@"Sec_ID", Convert.ToInt32( Sec_ID.Text));
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);              
                }
            } 
            GetData("Select * from Students");

            dataGridView1.Update();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentCell.OwningRow;  

            string value = row.Cells["Stud_ID"].Value.ToString();


            string Name = row.Cells["Name"].Value.ToString();    
            string Phone = row.Cells["Phone"].Value.ToString(); 

            string Address = row.Cells["Address"].Value.ToString();
            string Total_Credit = row.Cells["Total_Credit"].Value.ToString();
            string Dept_ID = row.Cells["Dept_ID"].Value.ToString();
            string Sec_ID = row.Cells["Sec_ID"].Value.ToString();
             DialogResult result = MessageBox.Show("Do you really want to delete " + Name + " " + Phone +" " + Address+ " "+Total_Credit +" " + Dept_ID + " "+ Sec_ID+ " "+ ", record " + value, "Message",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);



            string deleteState = @"Delete from Students where Stud_ID = '" + value + "'"; 


            if (result == DialogResult.Yes) 

            {

                using (con = new SqlConnection(conString))

                {

                    try

                    {

                        con.Open();
                        SqlCommand comm = new SqlCommand(deleteState, con);

                        comm.ExecuteNonQuery();
                        GetData("Select * from Students");
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

                case "Student ID":

                    GetData("select * from Students where Stud_ID like '%" + Student_ID.Text.ToLower() + "%'");
                    break;

                case "Name":

                    GetData("select * from Students where Name like '%" + s_Name.Text.ToLower() + "%'");

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
    }
}
