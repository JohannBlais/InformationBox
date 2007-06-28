using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InfoBox.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InformationBoxResult result = InformationBox.Show("A multiline message\nis displayed over several lines", "The caption can be long as well", InformationBoxButtons.YesNoCancel, InformationBoxIcon.Question);
            InformationBox.Show(String.Format("Hey, you clicked {0}", result), "If the caption is very long, the window is expanded as needed", InformationBoxButtons.OK, InformationBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InformationBox.Show("Icon : Asterisk", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Asterisk);
            InformationBox.Show("Icon : Error", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Error);
            InformationBox.Show("Icon : Exclamation", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Exclamation);
            InformationBox.Show("Icon : Hand", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Hand);
            InformationBox.Show("Icon : Information", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Information);
            InformationBox.Show("Icon : None", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.None);
            InformationBox.Show("Icon : Question", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Question);
            InformationBox.Show("Icon : Stop", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Stop);
            InformationBox.Show("Icon : Warning", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Warning);
            InformationBox.Show("Icon : Success", "Sample Icon", InformationBoxButtons.OK, InformationBoxIcon.Success);
        }
    }
}