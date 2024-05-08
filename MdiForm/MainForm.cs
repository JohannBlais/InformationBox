using InfoBox;
using System;
using System.Windows.Forms;

namespace MdiForm
{
    public partial class MainForm : Form
    {
        private ChildForm child;

        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformationBox.Show("My message test");
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (child == null) { 
                child = new ChildForm();
            }

            child.MdiParent = this;
            child.Show();
        }

        private void openModelessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformationBox.Show("My message test", InformationBoxBehavior.Modeless);
        }
    }
}
