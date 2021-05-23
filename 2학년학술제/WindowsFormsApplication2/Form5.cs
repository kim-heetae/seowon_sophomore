using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form5 : Form
    {
        public Form5(DataGridView dgv)
        {
            InitializeComponent();
            dataGridView1.DataSource = dgv.DataSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.printDocument1.Print();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.Format = "N0";
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.AutoResizeColumns();
            label7.Text = DateTime.Now.ToString();
            label7.TextAlign = ContentAlignment.MiddleCenter;
            label4.TextAlign = ContentAlignment.MiddleRight;
            label6.TextAlign = ContentAlignment.MiddleRight;
            label5.TextAlign = ContentAlignment.MiddleRight;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int width;
            int height;
            int realwidth = 100;
            int realheight = 100;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                width = dataGridView1.Columns[i].Width;
                height = dataGridView1.Rows[i].Height;
                e.Graphics.FillRectangle(Brushes.AliceBlue, realwidth, realheight, width, height);
                e.Graphics.DrawRectangle(Pens.Black, realwidth, realheight, width, height);
                e.Graphics.DrawString(dataGridView1.Columns[i].HeaderText, dataGridView1.Font, Brushes.Black, realwidth, realheight);
                realwidth = realwidth + width;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                realwidth = 100;
                realheight = realheight + dataGridView1.Rows[i].Height;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    width = dataGridView1.Columns[j].Width;
                    height = dataGridView1.Rows[j].Height;
                    e.Graphics.FillRectangle(Brushes.AliceBlue, realwidth, realheight, width, height);
                    e.Graphics.DrawRectangle(Pens.Black, realwidth, realheight, width, height);
                    e.Graphics.DrawString(dataGridView1.Rows[i].Cells[j].Value.ToString(), dataGridView1.Font, Brushes.Black, realwidth, realheight);
                    realwidth = realwidth + width;
                }
            }
            printDialog1.Document = printDocument1;
            printDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
