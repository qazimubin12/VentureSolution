using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VentureSolution
{
    class MainClass
    {
        public static string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\VentureDB.mdf;Integrated Security=True";

        public static SqlConnection con = new SqlConnection(s);

        public static void HideAllTabsOnTabControl(TabControl theTabControl)
        {
            theTabControl.Appearance = TabAppearance.FlatButtons;
            theTabControl.ItemSize = new Size(0, 1);
            theTabControl.SizeMode = TabSizeMode.Fixed;
        }


        public static void FillPersons(ComboBox cmb, string type)
        {
            DataTable dgpersons = new DataTable();
            dgpersons.Columns.Add("ID");
            dgpersons.Columns.Add("Name");
            dgpersons.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select ID,Name from Contacts where Type = '" + type + "'");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow persons in dt.Rows)
                        {
                            dgpersons.Rows.Add(persons["ID"], persons["Name"]);
                        }
                    }

                }
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "ID";
                cmb.DataSource = dgpersons;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgpersons;
            }
        }
    
    public static DataTable Retrieve(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, MainClass.con);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        public static void showWindow(Form OpenWin, Form clsWin, Form MDIWin)
        {
            clsWin.Close();
            OpenWin.MdiParent = MDIWin;
            OpenWin.WindowState = FormWindowState.Maximized;
            OpenWin.Show();
        }


        public static void showWindow(Form OpenWin, Form MDIWin)
        {
            OpenWin.MdiParent = MDIWin;
            OpenWin.WindowState = FormWindowState.Maximized;
            OpenWin.Show();
        }




        public static void FillProducts(ComboBox cmb)
        {
            DataTable dgProducts = new DataTable();
            dgProducts.Columns.Add("ID");
            dgProducts.Columns.Add("Name");
            dgProducts.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select ID, Name from Products");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow products in dt.Rows)
                        {
                            dgProducts.Rows.Add(products["ID"], products["Name"]);
                        }
                    }

                }
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "ID";
                cmb.DataSource = dgProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgProducts;
            }
        }

        public static void ShowProducts(DataGridView dgv, DataGridViewColumn ID, DataGridViewColumn Product,string data = "")
        {
            try
            {
                SqlCommand cmd = null;
                MainClass.con.Open();

                if (data != "")
                {
                    cmd = new SqlCommand("select ID, Name from Products  where p.ProductName like '%" + data + "%'", MainClass.con);
                }
                else
                {
                    cmd = new SqlCommand("select ID, Name from Products", MainClass.con);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ID.DataPropertyName = dt.Columns["ID"].ToString();
                Product.DataPropertyName = dt.Columns["Product"].ToString();
                dgv.DataSource = dt;
                MainClass.con.Close();
            }
            catch (Exception ex)
            {
                MainClass.con.Close();
                MessageBox.Show(ex.Message);
            }
        }



        
    }
}
