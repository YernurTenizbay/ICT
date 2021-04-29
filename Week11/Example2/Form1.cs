using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Example2
{
    public partial class Form1 : Form
    {
        static SQLiteConnection con = new SQLiteConnection(@"URI=file:C:\Users\Ghost Raven\Downloads\chinook\chinook.db");
        SQLiteCommand cmd = new SQLiteCommand(con);
        string current= "Select* from contact_book";
        string query = "";
        public Form1()
        {
            InitializeComponent();
           
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;


        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                con.Open();
                string name = textBox1.Text;
                int number = Convert.ToInt32(textBox2.Text);
                string query2 = "INSERT INTO contact_book(name, phone_number) VALUES('qwdw',1515)";
                string query = "INSERT INTO contact_book(name, phone_number) VALUES" + "('" + name + "'," + number.ToString() + ")";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                
                con.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                con.Open();
                string name = textBox1.Text;
                int number = Convert.ToInt32(textBox2.Text);

                string query = "Delete from contact_book where name = '" + name + "' and phone_number='" + number.ToString() + "' ";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (textBox3.Text != "" && textBox4.Text != "")
            {
                con.Open();
                string name = textBox4.Text;
                string new_name = textBox6.Text;
                if (new_name == "") new_name = name;
                if (textBox5.Text == "") textBox5.Text = textBox3.Text;
                int number = Convert.ToInt32(textBox3.Text);
                int new_number = Convert.ToInt32(textBox5.Text);
                string query = "UPDATE contact_book Set name='" + new_name+ "', phone_number="+ new_number.ToString() + " where name = '" + name + "' and phone_number='" + number.ToString() + "' ";
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
                 timer1.Enabled = true;  
                con.Open();
                
                string name = textBox1.Text;
                string number = textBox2.Text;
                if(name=="" && number == "")
                {
                    query = "Select* from contact_book";
                }
                else if (name == "" && number!="")
                {
                    query = "Select* from contact_book where phone_number='" + number + "' ";
                    
                }
                else if (name != "" && number == "")
                {
                    query = "Select* from contact_book where name = '" + name + "' ";
                    
                }
                else
                {
                    query = "Select* from contact_book where name = '" + name + "' and phone_number=" + number;
                    
                }

                cmd.CommandText = query;
                current = query;
                cmd.ExecuteNonQuery();
                con.Close();


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string limit = textBox7.Text;

            con.Open();
            DataTable dt = new DataTable();
            query = current;

            if (checkBox1.Checked == true) query = query + " order by name";
            else if (checkBox2.Checked == true)
            {
                query = query + " order by phone_number";

            }
            if (checkBox3.Checked == true) query += " DESC";
            if (limit != "") query = query + " limit " + limit;
            cmd.CommandText = query;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            timer1.Enabled = false;
        }

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            
            
            
            
        }


    }
}
