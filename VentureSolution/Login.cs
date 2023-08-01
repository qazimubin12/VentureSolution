using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace VentureSolution
{
    public partial class Login : Form
    {
        SqlDataReader dr;
        public static object User_NAME = "";
        public static object Role = "";
        public Login()
        {
            InitializeComponent();
            
        }

        private void Clear()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool found = false;
                object count = 0;
                MainClass.con.Open();
                SqlCommand cmd = new SqlCommand("select * from User where Username = @Username and Password = @Password ", MainClass.con);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    User_NAME = dr["Name"].ToString();
                }
                else
                {
                    found = false;
                    MessageBox.Show("Invalid Details", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Clear();
                    txtUsername.Focus();
                }
                dr.Close();
                MainClass.con.Close();

                MessageBox.Show("Welcome " + User_NAME);
                HomeScreen das = new HomeScreen();
                this.Dispose();
                das.Show();

            }
            catch (Exception ex)
            {
                MainClass.con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = null;
                MainClass.con.Open();
                 cmd = new SqlCommand("select count(*) from User", MainClass.con);
                int Count = int.Parse(cmd.ExecuteScalar().ToString());
                if(Count == 0)
                {
                    cmd = new SqlCommand("insert into User (Username,Password,Company,Position,Name,Image) values (@Username,@Password,@Company,@Position,@Name,@Image)", MainClass.con);
                    cmd.Parameters.AddWithValue("@Username", "Admin");
                    cmd.Parameters.AddWithValue("@Password", "admin123");
                    cmd.Parameters.AddWithValue("@Company", "DEMO");
                    cmd.Parameters.AddWithValue("@Position", "CEO");
                    cmd.Parameters.AddWithValue("@Name", "Demo Name");
                    cmd.Parameters.AddWithValue("@Image", null);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Demo User Added you can change into settings, Credentials are: Username: Admin and Password: admin123");

                }
                MainClass.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
