using System;
using System.Linq;
using System.Windows.Forms;

namespace OnRentVideoSystem
{
    public partial class Video : Form
    {
        public Video()
        {
            InitializeComponent();
        }
        int id;
        private void Booking_Load(object sender, EventArgs e)
        {
            Connection.GetMovieDate(videoGV);
            id = -1;
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            Booking b = new Booking();
            b.Show();
            this.Hide();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            Customer c = new Customer();
            c.Show();
            this.Hide();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            id = -1;
            Connection.GetMovieDate(videoGV);
        }
        private void button2_Click_2(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            panel5.Visible = true;
            saveBtn.Text = "Add";
            titleTxt.Text = "";
            genreTxt.Text = "";
            costTxt.Text = "";
            ratingTxt.Text = "";
            copiesTxt.Text = "";
            yearPK.Value = DateTime.Now;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (id != -1)
            {
                panel5.Visible = true;
                saveBtn.Text = "Update";
            }
        }
        private void button7_Click_1(object sender, EventArgs e)
        {
            if (id != -1 && titleTxt.Text != "")
            {
                if (titleTxt.Text != "" && ratingTxt.Text != "" && copiesTxt.Text != "" && costTxt.Text != "")
                {
                    Connection.DeleteData(titleTxt, genreTxt, costTxt, ratingTxt, copiesTxt, id.ToString());
                    id = -1;
                }
            }
            Connection.GetMovieDate(videoGV);

        }
        private void bookingGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (videoGV.Columns.Count != 0 && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewRow row = videoGV.Rows[e.RowIndex];
                id = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                titleTxt.Text = row.Cells["Title"].Value.ToString();
                String a=row.Cells["Cost"].Value.ToString();
                costTxt.Text = row.Cells["Cost"].Value.ToString().Remove(a.Length-3,2);
                copiesTxt.Text = row.Cells["Copies"].Value.ToString();
                genreTxt.Text = row.Cells["Genre"].Value.ToString();
                ratingTxt.Text = row.Cells["Ratting"].Value.ToString();
                yearPK.Value = new DateTime(Convert.ToInt32(row.Cells["Year"].Value.ToString()), 1, 1);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (titleTxt.Text != "" && ratingTxt.Text != "" && copiesTxt.Text != "")
            {
                if (saveBtn.Text == "Add")
                {
                    Connection.AddData(titleTxt, genreTxt, costTxt, ratingTxt, copiesTxt, yearPK);
                    id = -1;
                }
                else
                {
                    if (id != -1)
                    {
                        Connection.UpdateData(titleTxt, genreTxt, costTxt, ratingTxt, copiesTxt, yearPK, id.ToString());
                        id = -1;
                    }
                }
                titleTxt.Text = ""; 
                genreTxt.Text = "";
                costTxt.Text = ""; 
                ratingTxt.Text = ""; 
                copiesTxt.Text = "";
                yearPK.Value = DateTime.Now;
                Connection.GetMovieDate(videoGV);

            }
        }
        private void button9_Click_1(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }
        private void conctactTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(DateTime.Now.Year - yearPK.Value.Year);
            if (a >= 5)
                costTxt.Text = "2";
            else
                costTxt.Text = "5";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(DateTime.Now.Year - yearPK.Value.Year);
            if (a >= 5)
                costTxt.Text = "2";
            else
                costTxt.Text = "5";
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}