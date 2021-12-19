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
    public partial class Contacts : Form
    {
        string adminID = "";
        DatabaSeserver obj11 = new DatabaSeserver();
        public Contacts()
        {
            InitializeComponent();

        }

        private void Contacts_Load(object sender, EventArgs e)
        {
            adminID = Form1.SetValueForText1;
            if (adminID == "1")
            {
                button10.Visible = true;
            }
            else {

                button10.Visible = false;
            }
            Form1 form1 = new Form1();
            form1.Close();
            addcombodata();
            contentdisable();
            loaddataintogrid("select * from Contacts");

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            sort();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            sort();
        }
        string gander, city, company, searchstatement = "";
        private void sort()
        {
            if (comboBox2.Text != "Gander")
            {

                gander = "gander='" + comboBox2.Text + "'";
            }
            else
            {
                gander = "";
            }
            if (comboBox3.Text != "City" && comboBox2.Text != "Gander")
            {
                city = " AND city='" + comboBox3.Text + "'";
            }
            else if (comboBox3.Text != "City" && comboBox2.Text == "Gander")
            {
                city = "city='" + comboBox3.Text + "'";
            }
            else
            {
                city = "";
            }
            if (comboBox4.Text != "Company" && (comboBox2.Text != "Gander" || comboBox3.Text != "City"))
            {
                company = " AND company='" + comboBox4.Text + "'";
            }
            else if (comboBox4.Text != "Company" && (comboBox2.Text == "Gander" && comboBox3.Text == "City"))
            {
                company = "company='" + comboBox4.Text + "'";
            }
            else
            {
                company = "";
            }
            if (gander != "" || city != "" || company != "")
            {
                searchstatement = "select * from Contacts where " + gander + city + company;

            }
            else
            {
                searchstatement = "select * from Contacts";
            }
            MessageBox.Show(searchstatement);
            loaddataintogrid(searchstatement);

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obj11.savecontacts("INSERT INTO contacts VALUES('" + txtname.Text + "','" + txtfather.Text + "','" + cbogander.Text + "','" + txtaddress.Text + "','" + txtcity.Text + "','" + txtcompany.Text + "','" + txtdesignation.Text + "','" + txtemail.Text + "','" + txtcontact1.Text + "','" + txtcontact2.Text + "','" + txtcontact3.Text + "');");
            loaddataintogrid("Select * from Contacts");
            addcombodata();
            contentdisable();

        }
        private void loaddataintogrid(string statementt)
        {
            SQLiteDataReader sdr = obj11.dgwdata(statementt);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Father Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Gander", typeof(string)));
            dt.Columns.Add(new DataColumn("City", typeof(string)));
            dt.Columns.Add(new DataColumn("Company", typeof(string)));
            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("Contact1", typeof(string)));
            dt.Columns.Add(new DataColumn("Contact2", typeof(string)));
            dt.Columns.Add(new DataColumn("Contact3", typeof(string)));

            using (sdr)
            {

                while (sdr.Read())
                {
                    dt.Rows.Add(
                      sdr.GetValue(0),
                      sdr.GetValue(1),
                      sdr.GetValue(2),
                      sdr.GetValue(4),
                      sdr.GetValue(5),
                      sdr.GetValue(6),
                      sdr.GetValue(7),
                      sdr.GetValue(8),
                      sdr.GetValue(9),
                      sdr.GetValue(10)
                    );

                }
            }
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loaddataintogrid("select * from Contacts");
        }
        private void addcombodata()
        {
            SQLiteDataReader sdr = obj11.dgwdata("SELECT city,company FROM contacts GROUP BY city, company");
            using (sdr)
            {

                while (sdr.Read())
                {
                    comboBox3.Items.Add(sdr.GetValue(0));
                    comboBox4.Items.Add(sdr.GetValue(1));

                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            loaddataintogrid("Select * from contacts where upper(name) like '%" + txtname.Text.ToUpper() + "%' OR upper(name) like '%" + txtname.Text.ToUpper() + "' OR upper(name) like '" + txtname.Text.ToUpper() + "%' OR upper(name)='" + txtname.Text.ToUpper() + "%");

        }

        private void button7_Click(object sender, EventArgs e)
        {
            loaddataintogrid("Select * from contacts where contact1='" + txtcontact1.Text + "' OR contact2='" + txtcontact2.Text + "' OR contact3='" + txtcontact3.Text + "'");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            loaddataintogrid("Select * from contacts where upper(email)='" + txtemail.Text.ToUpper() + "'");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            contentenable();
            button9.Enabled = true;
        }
        private void contentdisable()
        {
            txtaddress.Enabled = false;
            txtcity.Enabled = false;
            txtcompany.Enabled = false;
            txtdesignation.Enabled = false;
            txtfather.Enabled = false;
            cbogander.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button9.Enabled = false;

        }
        private void contentenable()
        {
            txtaddress.Enabled = true;
            txtcity.Enabled = true;
            txtcompany.Enabled = true;
            txtdesignation.Enabled = true;
            txtfather.Enabled = true;
            cbogander.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            txtcity.Text = "";
            txtaddress.Text = "";
            txtcompany.Text = "";
            txtcontact1.Text = "";
            txtcontact2.Text = "";
            txtcontact3.Text = "";
            txtdesignation.Text = "";
            txtemail.Text = "";
            txtemail.Text = "";
            txtfather.Text = "";
            txtname.Text = "";
            cbogander.Text = "Gander";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are u sure to delete this contact with name " + txtname.Text, "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                obj11.commandexecutor("delete from contacts where upper(name)='" + txtname.Text.ToUpper() + "' OR contact1='" + txtcontact1.Text + "' OR contact2='" + txtcontact2.Text + "' OR contact3='" + txtcontact3.Text + "'");
                MessageBox.Show("Record is deleted");
            }
            else
            {
                // Do something else
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string keytxt = txtcontact1.Text.Trim();
            int rowcount = 0;
            if (button4.Text == "Edit" && txtcontact1.Text.Length != 0)
            {
                SQLiteDataReader sdr = obj11.dgwdata("SELECT * from contacts where contact1='" + txtcontact1.Text.Trim() + "' OR contact2 like '%" + txtcontact1.Text.Trim() + "%' OR contact3 like '%" + txtcontact1.Text.Trim() + "%'");
                using (sdr)
                {

                    while (sdr.Read())
                    {

                        txtname.Text = sdr.GetValue(0).ToString();
                        txtfather.Text = sdr.GetValue(1).ToString();
                        cbogander.Text = sdr.GetValue(2).ToString();
                        txtaddress.Text = sdr.GetValue(3).ToString();
                        txtcity.Text = sdr.GetValue(4).ToString();
                        txtcompany.Text = sdr.GetValue(5).ToString();
                        txtdesignation.Text = sdr.GetValue(6).ToString();
                        txtemail.Text = sdr.GetValue(7).ToString();
                        txtcontact1.Text = sdr.GetValue(8).ToString();
                        txtcontact2.Text = sdr.GetValue(9).ToString();
                        txtcontact3.Text = sdr.GetValue(10).ToString();
                        rowcount++;

                    }
                    if (rowcount < 1)
                    {
                        MessageBox.Show("No any record found of this contact \nPlese provide correct contact");
                        button4.Text = "Edit";
                        txtcontact1.Focus();

                    }
                }
                button1.Enabled = false;
                button4.Text = "Update";
            }
            else if (button4.Text == "Update" && txtcontact1.Text.Length != 0)
            {
                obj11.commandexecutor("update contacts set name='" + txtname.Text.Trim() + "', fathername='" + txtfather.Text.Trim() + "', gander='" + cbogander.Text + "', address='" + txtaddress.Text.Trim() + "', city='" + txtcity.Text + "', company='" + txtcompany.Text.Trim() + "', designation='" + txtdesignation.Text.Trim() + "', email='" + txtemail.Text + "', contact1='" + txtcontact1.Text.Trim() + "', contact2='" + txtcontact2.Text.Trim() + "', contact3='" + txtcontact3.Text.Trim() + "' where contact1='" + keytxt + "'");
                loaddataintogrid("select * from Contacts");
                MessageBox.Show("Record is Updated");

                button4.Text = "Edit";
            }
            else
            {
                MessageBox.Show("Please provide the primary contact to search and update");
                txtcontact1.Focus();
                button4.Text = "Edit";

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button4.Text = "Edit";
            contentenable();
            contentdisable();
            button9.Enabled = false;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            USer User = new USer();
            User.Show();
        }


    




    }

}