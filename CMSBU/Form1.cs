using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite;
using System.IO;

namespace CMSBU
{
    public partial class Form1 : Form
    {
        public static string SetValueForText1 = ""; 

        DatabaSeserver obj11 = new DatabaSeserver();
        int rows = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gb1.BackColor = System.Drawing.Color.Transparent;
            DatabaSeserver obj1 = new DatabaSeserver();
            rows = obj11.rowcounter();
            if (!(rows <= 0))
            {
                label1.Text = "Login";
                button1.Text = "Login";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (rows <= 0)
            {
                obj11.adduser(txtuser.Text, txtpass.Text);
                label1.Text = "Login";
                button1.Text = "Login";
                rows = 1;
            }
            else
            {
                int rw = 0;
                rw = obj11.cridentals(txtuser.Text, txtpass.Text);
                if (rw > 1)
                {
                    SQLiteDataReader sdr = obj11.dgwdata("SELECT * from user where username='" + txtuser.Text + "' AND password='" + txtpass.Text + "'");
                    using (sdr)
                    {
                        while (sdr.Read())
                        {
                            SetValueForText1 = sdr.GetValue(0).ToString();
                        }
                    }
                    Contacts cont = new Contacts();
                    this.Hide();
                    cont.Show();
                }
                else {
                    MessageBox.Show("Incorrect Username Password");
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

    }
}