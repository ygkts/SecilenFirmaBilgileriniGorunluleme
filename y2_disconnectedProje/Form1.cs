using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;

namespace y2_disconnectedProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // shipperId'yi combobox'tan alan ve datagrid1 den seçili satıra ait Customer ID ile aynı olan verileri datargiview2 ye yazar. 
        // ve comboboxtan seçilen shipperID ile butona tıklandığında o ID ye ait siparişler ve tarhiler gelir
        SqlConnection conn = new SqlConnection("Data Source = YASEMINGOKTAS; Database = NORTHWND; Trusted_Connection=true;");
        SqlCommand cmd;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter adap = new SqlDataAdapter("Select o.ShipVia, o.CustomerID, c.CompanyName from Shippers s inner join Orders o on s.ShipperID = o.ShipVia inner join Customers c on c.CustomerID=o.CustomerID", conn);
            DataTable tablo = new DataTable();
            adap.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Visible = false;
            cmd = new SqlCommand("Select ShipperID from Shippers", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            
            if (dataGridView1.CurrentCell == null)
            {
                cmd = new SqlCommand("Select  o.CustomerID, o.ShipVia, o.ShipName , o.OrderDate, o.RequiredDate, o.ShippedDate from Shippers s inner join Orders o on s.ShipperID = o.ShipVia inner join Customers c on c.CustomerID=o.CustomerID  where o.ShipVia = @sAD", conn);
                cmd.Parameters.AddWithValue("@sAD", comboBox1.Text);
                int satir = 0;
                dataGridView2.Rows.Clear();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    satir = dataGridView2.Rows.Add();
                    dataGridView2.Rows[satir].Cells[0].Value = dr[0].ToString();
                    dataGridView2.Rows[satir].Cells[1].Value = dr[1].ToString();
                    dataGridView2.Rows[satir].Cells[2].Value = dr[2].ToString();
                    dataGridView2.Rows[satir].Cells[3].Value = dr[3].ToString();
                    dataGridView2.Rows[satir].Cells[4].Value = dr[4].ToString();
                    dataGridView2.Rows[satir].Cells[5].Value = dr[5].ToString();
                }
            }
            else 
            {
                cmd = new SqlCommand("Select  o.CustomerID, o.ShipVia, o.ShipName, o.OrderDate, o.RequiredDate, o.ShippedDate from Shippers s inner join Orders o on s.ShipperID = o.ShipVia inner join Customers c on c.CustomerID=o.CustomerID  where o.ShipVia = @sAD and o.CustomerID = @cID", conn);
                cmd.Parameters.AddWithValue("@sAD", comboBox1.Text);
                cmd.Parameters.AddWithValue("@cID", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                int satir = 0;
                dataGridView2.Rows.Clear();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    satir = dataGridView2.Rows.Add();
                    dataGridView2.Rows[satir].Cells[0].Value = dr[0].ToString();
                    dataGridView2.Rows[satir].Cells[1].Value = dr[1].ToString();
                    dataGridView2.Rows[satir].Cells[2].Value = dr[2].ToString();
                    dataGridView2.Rows[satir].Cells[3].Value = dr[3].ToString();
                    dataGridView2.Rows[satir].Cells[4].Value = dr[4].ToString();
                    dataGridView2.Rows[satir].Cells[5].Value = dr[5].ToString();
                }
                // dataGridView1.CurrentCell = null;
            }
            
            
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = null;
            dataGridView2.Rows.Clear();
            comboBox1.SelectedItem = null;
        }        
    }
}
