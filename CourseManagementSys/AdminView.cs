using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseManagementSys
{
    public partial class AdminView : Form
    {
        public AdminView()
        {
            InitializeComponent();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student frm = new Student(); 

            frm.MdiParent = this;   
            label1.Visible = false;
            frm.Show(); 			

        }

        private void manageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Instructor frm = new Instructor(); 

            frm.MdiParent = this;   
            label1.Visible = false;
            frm.Show();

        }

        private void manageToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            course frm = new course(); 

            frm.MdiParent = this;   
            label1.Visible = false;
            frm.Show();
        }

        private void manageToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CourseRegistration frm = new CourseRegistration(); 

            frm.MdiParent = this; 
            label1.Visible = false;
            frm.Show();
        }

        private void AdminView_Load(object sender, EventArgs e)
        {

        }

        private void coursesAssignedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstructorCourses frm = new InstructorCourses();
            frm.MdiParent = this;
            label1.Visible = false;
            frm.Show();
        }

        private void assigningToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            InstructorCourses frm = new InstructorCourses();

            frm.MdiParent = this;
            label1.Visible = false;
            frm.Show();
        }
    }
}
