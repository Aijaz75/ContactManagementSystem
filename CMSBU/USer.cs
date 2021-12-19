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
    public partial class USer : Form
    {
        public USer()
        {
            InitializeComponent();
        }
        DatabaSeserver obj11 = new DatabaSeserver();

        private void button1_Click(object sender, EventArgs e)
        {
            int rowcount = 0;

            SQLiteDataReader sdr = obj11.dgwdata("SELECT * from user where userid='" + textBox1.Text.Trim() + "' OR username='" + textBox2.Text.Trim() + "'");

            using (sdr)
            {

                while (sdr.Read())
                {
                    textBox1.Text = sdr.GetValue(0).ToString();
                    textBox2.Text = sdr.GetValue(1).ToString();
                    rowcount++;

                }
            }
            button7.Enabled = true;
            if (rowcount <= 0)
            {
                MessageBox.Show("No User available on provided identifications.");
                button7.Enabled = false;
            }
            sdr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are u sure to delete this User ID with name " + textBox1.Text, "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                obj11.commandexecutor("delete from user where upper(userid)='" + textBox1.Text.ToUpper() + "' AND upper(username)='" + textBox2.Text.ToUpper() + "' AND userid!='1'");
                loaddataintogrid();
                MessageBox.Show("User id Deleted Successfully,\n This function can not delete admin user ID");

            }
            else { }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button4.Enabled = true;
            button5.Enabled = true;

        }

        private void USer_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button5.Enabled = false;
            loaddataintogrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            obj11.adduser(textBox2.Text, textBox3.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            button4.Enabled = false;
            button5.Enabled = false;
            loaddataintogrid();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == textBox8.Text)
            {
                obj11.commandexecutor("update user set username='" + textBox5.Text.Trim() + "', password='" + textBox8.Text.Trim() + "'");
                MessageBox.Show("Password Changed");
                textBox6.Text = "";
                textBox5.Text = "";
                textBox4.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox4.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;

            }
            else
            {

                MessageBox.Show("Passwords are not matching");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox6.Text = textBox1.Text;
            textBox5.Text = textBox2.Text;
            textBox4.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            button6.Enabled = true;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pass 1=" + textBox7.Text + " \n Pass 2=" + textBox8.Text);

        }
        private void loaddataintogrid()
        {
            SQLiteDataReader sdr = obj11.dgwdata("Select * from user");
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("User ID", typeof(string)));
            dt.Columns.Add(new DataColumn("User Name", typeof(string)));
         
            using (sdr)
            {

                while (sdr.Read())
                {
                    dt.Rows.Add(
                      sdr.GetValue(0),
                      sdr.GetValue(1)
              
                    );

                }
            }
            dataGridView1.DataSource = dt;
            sdr.Close();
        }
    }
}