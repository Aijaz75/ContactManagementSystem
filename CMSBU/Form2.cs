using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite;

namespace CMSBU
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DatabaSeserver obj11 = new DatabaSeserver();
        private void button1_Click(object sender, EventArgs e)
        {
            int rowcount=0;
            SQLiteDataReader sdr = obj11.dgwdata("SELECT * from User where Username='" + textBox2.Text + "' OR Userid=" + textBox1.Text + "'");
            using (sdr)
            {

                while (sdr.Read())
                {
                    textBox1.Text = sdr.GetValue(0).ToString();
                    textBox2.Text = sdr.GetValue(2).ToString();

                    rowcount++;

                }
                
            }
            if (rowcount <= 0) { MessageBox.Show("No user available on Username/ID=" + textBox1 + " " + textBox2.Text); }


        }
    }
}

