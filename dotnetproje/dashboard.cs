using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Caching.Memory;
namespace dotnetproje
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }
        private DataTable dataTable = new DataTable();
        string connString = @"Data Source=.\SQLExpress;Initial Catalog=netproje;Integrated Security=True";
        double total=0;
        double harcanan = 0;
        public void dashboard_Load(object sender, EventArgs e)
        {

            label5.Text = "Hoşgeldiniz, Sn. "+Program.myCache.Get("name").ToString();
            string query = "select id, convert(DOUBLE PRECISION, amount) as 'Miktar', explanation as 'Açıklama', type as 'İşlem Tipi' from transactions where userid=" + Program.myCache.Get("id");

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            SqlCommand cmd2 = new SqlCommand("DELETE From [transactions] WHERE [id] = @id", conn);

            da.DeleteCommand = cmd2;
            // this will query your database and return the result to your datatable

            da.Fill(dataTable);

            DataSet ds = new DataSet();

            da.Fill(ds, "ss");

            dataGridView1.DataSource = ds.Tables["ss"];
            
            conn.Close();
            da.Dispose();
            total = 0;
            dataGridView1.Columns[0].Visible = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if(row.Cells["İşlem Tipi"].Value.ToString() == "Gelen Para")
                {
                    total += Convert.ToDouble(row.Cells["Miktar"].Value.ToString());
                }
                else
                {
                    total= total- Convert.ToDouble(row.Cells["Miktar"].Value.ToString());
                    harcanan += Convert.ToDouble(row.Cells["Miktar"].Value.ToString());
                }
                //More code here
            }
            label2.Text = total.ToString() + " ₺";
            label4.Text = harcanan.ToString() + " ₺";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.dashboard_Load(sender,e);
            
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var myForm = new ekle();
    
            myForm.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
             string connString = @"Data Source=.\SQLExpress;Initial Catalog=netproje;Integrated Security=True";
            string id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE From [transactions] WHERE [id] = "+id;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            this.dashboard_Load(sender, e);

            this.Refresh();
        }
    }
}
