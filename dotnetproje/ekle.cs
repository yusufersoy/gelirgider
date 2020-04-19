using Microsoft.Extensions.Caching.Memory;
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

namespace dotnetproje
{
    public partial class ekle : Form
    {
        public ekle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = @"Data Source=.\SQLExpress;Initial Catalog=netproje;Integrated Security=True";
            string id = Program.myCache.Get("id").ToString();
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO transactions (amount, type,explanation,userid) VALUES ("+textBox1.Text+", '"+comboBox1.SelectedItem.ToString()+"' , '"+ richTextBox1.Text+ "' , "+ id.ToString() +")";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            if(comboBox1.SelectedItem.ToString()=="Gelen Para")
            {
                MessageBox.Show(textBox1.Text + " TL'lik Geliriniz Eklendi.");
             
            }
            else
            {
                MessageBox.Show(textBox1.Text + " TL'lik Gideriniz Eklendi.");
            }

        }
    }
}
