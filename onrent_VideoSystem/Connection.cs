﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OnRentVideoSystem
{
    public class Connection
    {
        private static SqlConnection myCon = new SqlConnection("Data Source=DESKTOP-726D3G\\SQLEXPRESS;Initial Catalog=VideoRentalDB;Integrated Security=True");
        static SqlCommand myCmd;
        public static void GetRentedData(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getCustomer", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
        }
        public static void GetMovieDate(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getVideo", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
        }
        public static void GetRentalData(DataGridView gv)
        {
            SqlDataAdapter da = new SqlDataAdapter("getBooking", myCon);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gv.DataSource = dataTable;
            gv.Columns["CID"].Visible = false;
            gv.Columns["VID"].Visible = false;
            gv.Columns["Cost"].Visible = false;
        }
        public static void DeleteData(TextBox a, TextBox b, TextBox c, string id)
        {
            string query = "delete from Customer where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void DeleteData(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, String id)
        {
            string query = "delete from Video where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Text = "";
                e.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public static void DeleteData(String id)
        {
            string query = "delete from Booking where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show("Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void AddData(TextBox a, TextBox b, TextBox c)
        {
            string query = "insert into Customer(Name,Phone,Address) values('" + a.Text + "','" + b.Text + "','" + c.Text + "');";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                myCon.Close();
            }
        }
        public static void AddData(TextBox a, TextBox b, DateTimePicker c, DateTimePicker d)
        {
            int count = 0;
            string q = "select Copies from Video where ID=" + Convert.ToInt32(b.Tag) + ";";
            SqlDataReader dataReader;
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(q, myCon);
                dataReader = myCmd.ExecuteReader();
                dataReader.Read();
                count = dataReader.GetInt32(0);
                dataReader.Close();
                myCon.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                myCon.Close();
            }
            if (count != 0)
            {
                string query = "insert into Booking(Customer_ID,Video_ID,Start,Due,Status) values(" + Convert.ToInt32(a.Tag) + "," + Convert.ToInt32(b.Tag) + ",'" + c.Value.ToString("dd MMMM yy") + "','" + d.Value.ToString("dd MMMM yy") + "','Issue'); update Video set Copies=Copies-1 where ID=" + Convert.ToInt32(b.Tag) + "; ";
                try
                {
                    myCon.Open();
                    myCmd = new SqlCommand(query, myCon);
                    myCmd.ExecuteReader();
                    myCon.Close();
                    MessageBox.Show("Issued Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    myCon.Close();
                }
            }
            else
            {
                MessageBox.Show("Video Copies Not Available...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static void AddData(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, DateTimePicker f)
        {
            string query = "insert into Video(Title,Genre,Cost,Ratting,Copies,Year) values('" + a.Text + "','" + b.Text + "','" + Convert.ToInt32(c.Text) + "','" + d.Text + "'," + Convert.ToInt32(e.Text) + ",'" + f.Text + "');";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
                d.Text = "";
                e.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateData(TextBox a, TextBox b, TextBox c, string id)
        {
            string query = "update Customer set Name='" + a.Text + "',Phone='" + b.Text + "', Address='" + c.Text + "' where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateData(TextBox a, TextBox b, TextBox c, TextBox d, TextBox e, DateTimePicker f, String id)
        {
            string query = "update Video set Title='" + a.Text + "', Genre='" + b.Text + "', Cost='" + Convert.ToInt32(c.Text) + "', Ratting='" + d.Text + "', Copies=" + Convert.ToInt32(e.Text) + ",Year='" + f.Text + "'  where ID=" + Convert.ToInt32(id) + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show(a.Text + " Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                a.Text = "";
                b.Text = "";
                d.Text = "";
                e.Text = "";
                c.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void UpdateData(TextBox a, TextBox b, DateTimePicker c, DateTimePicker d, String id, int i)
        {
            string query = "update Booking set Customer_ID=" + Convert.ToInt32(a.Tag) + ", Video_ID=" + Convert.ToInt32(b.Tag) + ", Start='" + c.Value.ToString("dd MMMM yy") + "',Due='" + d.Value.ToString("dd MMMM yy") + "',Status='Return' where ID=" + Convert.ToInt32(id) + "; update Video set Copies=Copies+1 where ID=" + b.Tag + "; ";
            try
            {
                myCon.Open();
                myCmd = new SqlCommand(query, myCon);
                myCmd.ExecuteReader();
                myCon.Close();
                MessageBox.Show("Total Rent Cost is " + i.ToString() + "$", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}