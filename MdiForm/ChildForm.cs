using InfoBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MdiForm
{
    public partial class ChildForm : Form
    {
        public ChildForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformationBox.Show("My message test", this, InformationBoxPosition.CenterOnParent);
        }

        private void openModelessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformationBox.Show("My message test", this, InformationBoxPosition.CenterOnParent, InformationBoxBehavior.Modeless);
        }
    }
}
