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
            InformationBoxResult result = InformationBox.Show("Néness\nGeorges", "Un caption sympa comme le fromage", InformationBoxButtons.YesNoCancel);
            InformationBox.Show(String.Format("Hey, you clicked {0}", result));
            MessageBox.Show(String.Format("Hey, you clicked {0}", result));
        }
    }
}