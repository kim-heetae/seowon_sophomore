using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=222.116.108.118;User ID=201111780;Password=gkstjfghks");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string qq = "select * from 학술제로그인 where 아이디 = '" + comboBox1.Text
            + "' and 비밀번호 = '" + textBox2.Text +"'";
            SqlCommand command = new SqlCommand();
            command.CommandText = qq;
            command.Connection = conn;
            SqlDataReader reader;
            conn.Open();  
            reader = command.ExecuteReader();
            if (reader.HasRows)  
            {
                MessageBox.Show("환영합니다. 로그인 성공");
                this.Hide();
                Form2 f2 = new Form2();
                
                f2.Show();
                

            }
            else
            {
                MessageBox.Show("비밀번호를 확인해주세요.");
                comboBox1.Text = "";  
                textBox2.Clear();  
                textBox2.Focus();  
            }
            conn.Close();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                button1_Click(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
