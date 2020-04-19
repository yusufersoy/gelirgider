using System;
using System.Collections;
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
    public partial class login : Form
    {
       
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string BaglantiAdresi = @"Data Source=.\SQLExpress;Initial Catalog=netproje;Integrated Security=True";
            SqlConnection con = new SqlConnection(BaglantiAdresi);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "exec loginuser @username , @password;";
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@username";
            param.Value = textBox1.Text;
            cmd.Parameters.Add(param);
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@password";
            param1.Value = textBox2.Text;
            cmd.Parameters.Add(param1);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == false)
            {
                MessageBox.Show("Kullanıcı Adı Veya Şifreniz Yalnış Lütfen Tekrar Deneyiniz !");
                dr.Close();
                con.Close();
            }
            else
            {
                while (dr.Read())
                {
                   
                        Program.myCache.Set("name", dr["name"]);
                    Program.myCache.Set("id", dr["id"]);
                    var myForm = new dashboard();
                    this.Hide();
                    myForm.Show();
                   
                   

                }
                dr.Close();
                con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
