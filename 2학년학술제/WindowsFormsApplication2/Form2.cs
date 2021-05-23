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
    public partial class Form2 : Form
    {
        
        SqlConnection conn = new SqlConnection("Data Source=222.116.108.118;User ID=201111780;Password=gkstjfghks");
        List<Beverage> order = new List<Beverage>();
        List<Beverage> orderq = new List<Beverage>();
        DataGridView dgvTemp = new DataGridView();
        DataGridView dgvTempq = new DataGridView();
        List<Receipt> receipts = new List<Receipt>();
        public int qq;

        public Form2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString("F");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.Format = "N0";
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            textBox1.TextAlign = HorizontalAlignment.Right;
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox14.Text = "0";
            label4.Text = "0";
            label4.Font = new Font("Tahoma", 22.0F);
            label4.TextAlign = ContentAlignment.MiddleRight;
            toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleRight;
            
            
            
        }

        private void refreshDGV()
        {
            dataGridView1.DataSource = order.ToList();
        }

        private void qqqq()
        {
            dataGridView3.DataSource = orderq.ToList();
        }
 
        private void setDGV(object sender, int price)
        {
            order.Add(new Beverage(((Button)sender).Text, price));
            refreshDGV();
        }

        private void qqqqq()
        {
            var order메뉴s = from a in orderq
                             group a by a.메뉴 into s
                             select new { 날짜 = s.Key, 매출금액 = s.Sum(a => a.가격) };
            int 금액합계s = orderq
                .GroupBy(a => a.메뉴)
                .Select(g => g.Sum(a => a.가격))
                .Sum();
            
            var orderList = order메뉴s.ToList();
            orderList.Add(new { 날짜 = "합계", 매출금액 = 금액합계s });
            dataGridView3.DataSource = orderList;
            textBox13.Text = 금액합계s.ToString();
            textBox13.TextAlign = HorizontalAlignment.Right;
        }

        private void calcOrder()
        {
            var order메뉴 = from o in order
                             group o by o.메뉴 into g
                select new {메뉴 = g.Key, 수량 = g.Count(),  금액합계 = g.Sum(o => o.가격) };
            int 금액합계s = order
                .GroupBy(o => o.메뉴)
                .Select(g => g.Sum(o => o.가격))            
                .Sum();
            int Quantities = order
                .GroupBy(o => o.메뉴) 
                .Select(g => g.Count())
                .Sum();
            var orderList = order메뉴.ToList();
            orderList.Add(new { 메뉴 = "합계", 수량 = Quantities, 금액합계 = 금액합계s });
            dataGridView1.DataSource = orderList;
            textBox3.Text = 금액합계s.ToString();
            textBox3.TextAlign = HorizontalAlignment.Right;
        }

        private void dgvOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string item = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            DialogResult result = MessageBox.Show(item + " 선택을 삭제하겠습니까?", "선택삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                order.RemoveAt(e.RowIndex);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = order;
                dataGridView1.Refresh();
            }
        }

       

        
        private void button9_Click_1(object sender, EventArgs e)
        {
            calcOrder(); 
        }

        private void button10_Click(object sender, EventArgs e)
        {
            refreshDGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setDGV(sender, 8000);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            setDGV(sender, 4000);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setDGV(sender, 8000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setDGV(sender, 5500);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setDGV(sender, 5500);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setDGV(sender, 5500);
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            setDGV(sender, 7000);
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            setDGV(sender, 6000);
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            setDGV(sender, 6000);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            setDGV(sender, 4000);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            setDGV(sender, 2000);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            setDGV(sender, 0);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            setDGV(sender, 0);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            refreshDGV();
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            calcOrder();
            double discount = 0;
            int y = Convert.ToInt32(textBox3.Text);
            if (checkBox1.Checked)
            {
                discount += (Convert.ToInt32(textBox3.Text) * 0.1);   
            }
            textBox2.Text = discount.ToString(); 
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                
                int changeAmount = (Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox3.Text));
                double discount = 0;
                int x = Convert.ToInt32(textBox1.Text);
                int y = Convert.ToInt32(textBox3.Text);
                if (changeAmount < 0)
                {
                    changeAmount *= -1;
                    MessageBox.Show(changeAmount.ToString("N0") + " 원이 부족합니다.");
                }
                else
                {
                    label4.Text = changeAmount.ToString("N0");
                    label4.TextAlign = ContentAlignment.MiddleRight;
                    int z = Convert.ToInt32(textBox2.Text);
                    int w = Convert.ToInt32(textBox1.Text);
                    int v = Convert.ToInt32(textBox3.Text);
                    int q = v - z;
                    string sql = "INSERT INTO 학술제매출 (날짜, 매출금액, 결제방법) VALUES('"
            + toolStripStatusLabel1.Text + "', '"
            + q.ToString() + "', '"
            + "현금" + "')";

                    SqlCommand command = new SqlCommand();
                    command.CommandText = sql;
                    command.Connection = conn;
                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("매출이 추가되었습니다.");
                }
                if (checkBox1.Checked)
                {
                    discount += (Convert.ToInt32(textBox3.Text) * 0.1);
                    textBox2.Text = discount.ToString("N0");
                    textBox14.Text = (y - discount).ToString();
                }
                else
                {
                    textBox2.Text = "0";
                    textBox14.Text = y.ToString();
                }
                label4.Text = (Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox14.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("계산할 항목이 없습니다.", ex.Message);
            }
            
        }

        private void button12_Click_1(object sender, EventArgs e)
        {

            
            {

                int changeAmount = (Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox3.Text));
                double discount = 0;
                int x = Convert.ToInt32(textBox1.Text);
                int y = Convert.ToInt32(textBox3.Text);
                int z = Convert.ToInt32(textBox2.Text);
                int q = y - z;
                string sql = "INSERT INTO 학술제매출 (날짜, 매출금액, 결제방법) VALUES('"
        + toolStripStatusLabel1.Text + "', '"
        + q.ToString() + "', '"
            + "카드" + "')";

                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("매출이 추가되었습니다.");


                if (checkBox1.Checked)
                {
                    discount += (Convert.ToInt32(textBox3.Text) * 0.1);
                }
                else
                {
                    textBox2.Text = "0";
                }
                textBox1.Text = "카드";
                textBox2.Text = discount.ToString("N0");
                label4.Text = "0";
                textBox14.Text = (y - discount).ToString();
            }
            
           
               
            

        }

        private void btn50000_Click_1(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = 50000;
            int sum = x + y;
            textBox1.Text = sum.ToString();
        }

        private void btn30000_Click_1(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = 30000;
            int sum = x + y;
            textBox1.Text = sum.ToString();
        }

        private void btn20000_Click_1(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = 20000;
            int sum = x + y;
            textBox1.Text = sum.ToString();
        }

        private void btn10000_Click_1(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = 10000;
            int sum = x + y;
            textBox1.Text = sum.ToString();
        }

        private void btn5000_Click_1(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = 5000;
            int sum = x + y;
            textBox1.Text = sum.ToString();
        }

        private void btn1000_Click_1(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = 1000;
            int sum = x + y;
            textBox1.Text = sum.ToString();
        }

        private void btn500_Click_1(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = 500;
            int sum = x + y;
            textBox1.Text = sum.ToString();
        }

        private void btn100_Click_1(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textBox1.Text);
            int y = 100;
            int sum = x + y;
            textBox1.Text = sum.ToString();
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            label4.Text = "0";
            textBox14.Text = "0";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string item = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            DialogResult result = MessageBox.Show(item + " 선택을 삭제하겠습니까?", "선택삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                order.RemoveAt(e.RowIndex);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = order;
                dataGridView1.Refresh();
            }
        }

        
        private void button20_Click(object sender, EventArgs e)
        {
            string q;
            if (radioButton1.Checked)
            {
                q = radioButton1.Text;
            }
            else
            {
                q = radioButton2.Text;
            }

            string sql = "INSERT INTO 회원관리 (회원이름, 전화번호, 생년월일, 소주키핑, 맥주키핑, 성별) VALUES('"
            + textBox5.Text + "', '"
            + textBox6.Text + "', '"
            + textBox7.Text + "', '"
            + textBox8.Text + "', '"
            + textBox9.Text + "', '"
            + q + "')";
            
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            button21.PerformClick(); //버튼4를 이용   

            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

        }

        private void button21_Click(object sender, EventArgs e)
        {
            string sql = "select 회원이름,전화번호,생년월일,소주키핑,맥주키핑,성별 from 회원관리";
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE 회원관리 SET " +
                
               "전화번호 = '" + textBox6.Text + "'," +
               "생년월일 = '" + textBox7.Text + "'," +
               "소주키핑 = '" + textBox8.Text + "'," +
               "맥주키핑 = '" + textBox9.Text + "'" +
               " WHERE 회원이름 = '" + textBox5.Text + "'";

            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            button21.PerformClick(); //버튼4를 이용   

            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

        }

        private void button23_Click(object sender, EventArgs e)
        {
            string sql = "DELETE 회원관리 " +
   " WHERE 회원이름 = '" + textBox5.Text + "'";

            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            button21.PerformClick(); //버튼4를 이용   

            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(textBox8.Text);
                int y = 1;
                int sum = x + y;
                textBox8.Text = sum.ToString();
                string sql = "UPDATE 회원관리 SET " +

                  "전화번호 = '" + textBox6.Text + "'," +
                  "생년월일 = '" + textBox7.Text + "'," +
                  "소주키핑 = '" + sum.ToString() + "'," +
                  "맥주키핑 = '" + textBox9.Text + "'" +
                  " WHERE 회원이름 = '" + textBox5.Text + "'";
                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                button21.PerformClick();

                string sql2 = "SELECT 회원이름, 전화번호, 생년월일, 소주키핑, 맥주키핑, 성별 FROM 회원관리 WHERE 회원이름 LIKE '%" + (textBox5.Text) + "%'";
                SqlCommand command2 = new SqlCommand();
                command2.CommandText = sql2;
                command2.Connection = conn;
                SqlDataAdapter adapter2 = new SqlDataAdapter(command2);
                DataTable table2 = new DataTable();
                adapter2.Fill(table2);
                dataGridView2.DataSource = table2;
            }
            catch
            {
                MessageBox.Show("고객을 선택해 주세요.");
            }
        }


        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(textBox8.Text);
                int y = 1;
                int m = x - y;
                textBox8.Text = m.ToString();
                string sql = "UPDATE 회원관리 SET " +

                  "전화번호 = '" + textBox6.Text + "'," +
                  "생년월일 = '" + textBox7.Text + "'," +
                  "소주키핑 = '" + m.ToString() + "'," +
                  "맥주키핑 = '" + textBox9.Text + "'" +
                  " WHERE 회원이름 = '" + textBox5.Text + "'";

                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                button21.PerformClick();

                string sql2 = "SELECT 회원이름, 전화번호, 생년월일, 소주키핑, 맥주키핑, 성별 FROM 회원관리 WHERE 회원이름 LIKE '%" + (textBox5.Text) + "%'";
                SqlCommand command2 = new SqlCommand();
                command2.CommandText = sql2;
                command2.Connection = conn;
                SqlDataAdapter adapter2 = new SqlDataAdapter(command2);
                DataTable table2 = new DataTable();
                adapter2.Fill(table2);
                dataGridView2.DataSource = table2;
            }
            catch
            {
                MessageBox.Show("고객을 선택해 주세요.");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(textBox9.Text);
                int y = 1;
                int sum = x + y;
                textBox9.Text = sum.ToString();
                string sql = "UPDATE 회원관리 SET " +

                  "전화번호 = '" + textBox6.Text + "'," +
                  "생년월일 = '" + textBox7.Text + "'," +
                  "소주키핑 = '" + textBox8.Text + "'," +
                  "맥주키핑 = '" + sum.ToString() + "'" +
                  " WHERE 회원이름 = '" + textBox5.Text + "'";

                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                button21.PerformClick();

                string sql2 = "SELECT 회원이름, 전화번호, 생년월일, 소주키핑, 맥주키핑, 성별 FROM 회원관리 WHERE 회원이름 LIKE '%" + (textBox5.Text) + "%'";
                SqlCommand command2 = new SqlCommand();
                command2.CommandText = sql2;
                command2.Connection = conn;
                SqlDataAdapter adapter2 = new SqlDataAdapter(command2);
                DataTable table2 = new DataTable();
                adapter2.Fill(table2);
                dataGridView2.DataSource = table2;
            }
            catch
            {
                MessageBox.Show("고객을 선택해 주세요.");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(textBox9.Text);
                int y = 1;
                int m = x - y;
                textBox9.Text = m.ToString();
                string sql = "UPDATE 회원관리 SET " +

                  "전화번호 = '" + textBox6.Text + "'," +
                  "생년월일 = '" + textBox7.Text + "'," +
                  "소주키핑 = '" + textBox8.Text + "'," +
                  "맥주키핑 = '" + m.ToString() + "'" +
                  " WHERE 회원이름 = '" + textBox5.Text + "'";

                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                button21.PerformClick();

                string sql2 = "SELECT 회원이름, 전화번호, 생년월일, 소주키핑, 맥주키핑, 성별 FROM 회원관리 WHERE 회원이름 LIKE '%" + (textBox5.Text) + "%'";
                SqlCommand command2 = new SqlCommand();
                command2.CommandText = sql2;
                command2.Connection = conn;
                SqlDataAdapter adapter2 = new SqlDataAdapter(command2);
                DataTable table2 = new DataTable();
                adapter2.Fill(table2);
                dataGridView2.DataSource = table2;
            }
            catch
            {
                MessageBox.Show("고객을 선택해 주세요.");
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button11_Click_4(object sender, EventArgs e)
        {
            string sql = "SELECT 회원이름, 전화번호, 생년월일, 소주키핑, 맥주키핑, 성별 FROM 회원관리 WHERE 회원이름 LIKE '%" + (textBox4.Text) + "%'";
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView2.DataSource = table;
            textBox4.Clear();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox5.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox6.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox7.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox8.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox9.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            double discount = 0;
            
            int y = Convert.ToInt32(textBox3.Text);
            dgvTempq.DataSource = dataGridView1.DataSource;
            Form5 frmReceipt = new Form5(dgvTempq);
            frmReceipt.label4.Text = String.Format("{0:#,#}", textBox1.Text);
            if (checkBox1.Checked)
            {
                frmReceipt.label5.Text = String.Format("{0:#,#}", discount += (Convert.ToInt32(textBox3.Text) * 0.1));
            }
            else
            {
                frmReceipt.label5.Text = "0";
            }
            frmReceipt.label6.Text = String.Format("{0:#,#}", Convert.ToInt32(textBox14.Text));
            frmReceipt.ShowDialog();
            
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button25_Click(object sender, EventArgs e)
        {

            string sql = "SELECT 날짜,매출금액,결제방법 FROM 학술제매출 WHERE 날짜 LIKE '%" + (textBox10.Text+"년") + "%'";
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
            textBox10.Clear();
            
            
           
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                sum += Convert.ToInt32(r.Cells[1].Value);
            }
            textBox13.Text = sum.ToString("N0");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            string sql = "SELECT 날짜,매출금액,결제방법 FROM 학술제매출 WHERE 날짜 LIKE '%" + (textBox11.Text + "월") + "%'";
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
            textBox11.Clear();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            string sql = "SELECT 날짜,매출금액,결제방법 FROM 학술제매출 WHERE 날짜 LIKE '%" + (textBox12.Text + "일") + "%'";
            SqlCommand command = new SqlCommand();
            command.CommandText = sql;
            command.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView3.DataSource = table;
            textBox12.Clear();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                sum += Convert.ToInt32(r.Cells[1].Value);
            }
            textBox13.Text = sum.ToString("N0");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                sum += Convert.ToInt32(r.Cells[1].Value);
            }
            textBox13.Text = sum.ToString("N0");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            int sum = 0;
            int w = Convert.ToInt32(dataGridView3.Rows.Count);
            int q = 0;
            
                foreach (DataGridViewRow r in dataGridView3.Rows)
                {
                    sum += Convert.ToInt32(r.Cells[1].Value);
                }
                q = Convert.ToInt32(sum / (w-1));
                textBox13.Text = q.ToString("N0");
            
        }

        private void button29_Click(object sender, EventArgs e)
        {
            int sum = 0;
            int w = Convert.ToInt32(dataGridView3.Rows.Count);
            int q = 0;

            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                sum += Convert.ToInt32(r.Cells[1].Value);
            }
            q = Convert.ToInt32(sum / (w - 1));
            textBox13.Text = q.ToString("N0");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            int sum = 0;
            int w = Convert.ToInt32(dataGridView3.Rows.Count);
            int q = 0;

            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                sum += Convert.ToInt32(r.Cells[1].Value);
            }
            q = Convert.ToInt32(sum / (w - 1));
            textBox13.Text = q.ToString("N0");
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int q = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[1].Value);
            int w = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[1].Value);
            int sum = q + w;
            textBox13.Text = q.ToString();
            
        }

        private void tableLayoutPanel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        }
    }

