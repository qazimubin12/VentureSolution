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
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //public static string s = "Server=tcp:VentureSolution.database.windows.net,1433;Initial Catalog=VentureSolution;Persist Security Info=False;User ID=possystem;Password=MubinDon123@#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static string s = File.ReadAllText(path + "\\posconnect");
        public static SqlConnection con = new SqlConnection(s);

        public static void HideAllTabsOnTabControl(TabControl theTabControl)
        {
            theTabControl.Appearance = TabAppearance.FlatButtons;
            theTabControl.ItemSize = new Size(0, 1);
            theTabControl.SizeMode = TabSizeMode.Fixed;
        }

        public static void UpdateInventory(Int32 ProductID, Double Qty, Double CostPrice)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Inventory set Qty = @Qty, Rate = @Rate where ProductID = @ProductID", MainClass.con);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Rate", CostPrice);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MainClass.con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public static void FillPersons(ComboBox cmb, string type)
        {
            DataTable dgpersons = new DataTable();
            dgpersons.Columns.Add("PersonID");
            dgpersons.Columns.Add("Name");
            dgpersons.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select PersonID,Name from PersonsTable where Type = '" + type + "'");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow persons in dt.Rows)
                        {
                            dgpersons.Rows.Add(persons["PersonID"], persons["Name"]);
                        }
                    }

                }
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "PersonID";
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


        public static void FillCategories(ComboBox cmb)
        {
            DataTable dtCategoryName = new DataTable();
            dtCategoryName.Columns.Add("CategoryID");
            dtCategoryName.Columns.Add("Category");
            dtCategoryName.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select CategoryID, Category from CategoriesTable");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow category in dt.Rows)
                        {
                            dtCategoryName.Rows.Add(category["CategoryID"], category["Category"]);
                        }
                    }

                }
                cmb.DisplayMember = "Category";
                cmb.ValueMember = "CategoryID";
                cmb.DataSource = dtCategoryName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dtCategoryName;
            }

        }

        public static void FillUnits(ComboBox cmb)
        {
            DataTable dgUnits = new DataTable();
            dgUnits.Columns.Add("UnitID");
            dgUnits.Columns.Add("Unit");
            dgUnits.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select UnitID, Unit from UnitsTable");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow units in dt.Rows)
                        {
                            dgUnits.Rows.Add(units["UnitID"], units["Unit"]);
                        }
                    }

                }
                cmb.DisplayMember = "Unit";
                cmb.ValueMember = "UnitID";

                cmb.DataSource = dgUnits;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgUnits;
            }
        }


        public static void FillBrands(ComboBox cmb)
        {
            DataTable dgBrands = new DataTable();
            dgBrands.Columns.Add("BrandID");
            dgBrands.Columns.Add("Brand");
            dgBrands.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select BrandID, Brand from BrandsTable");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow Brands in dt.Rows)
                        {
                            dgBrands.Rows.Add(Brands["BrandID"], Brands["Brand"]);
                        }
                    }

                }
                cmb.DisplayMember = "Brand";
                cmb.ValueMember = "BrandID";

                cmb.DataSource = dgBrands;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgBrands;
            }
        }


        public static void FillSupplier(ComboBox cmb)
        {
            DataTable dgSupplier = new DataTable();
            dgSupplier.Columns.Add("PersonID");
            dgSupplier.Columns.Add("Name");
            dgSupplier.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select PersonID, Name from PersonsTable where Type = 'Supplier'");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow supplier in dt.Rows)
                        {
                            dgSupplier.Rows.Add(supplier["PersonID"], supplier["Name"]);
                        }
                    }

                }
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "PersonID";
                cmb.DataSource = dgSupplier;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgSupplier;
            }
        }

        public static void FillCustomer(ComboBox cmb)
        {
            DataTable dgCustomer = new DataTable();
            dgCustomer.Columns.Add("PersonID");
            dgCustomer.Columns.Add("Name");
            dgCustomer.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select PersonID, Name from PersonsTable where Type = 'Customer'");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow customer in dt.Rows)
                        {
                            dgCustomer.Rows.Add(customer["PersonID"], customer["Name"]);
                        }
                    }

                }
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "PersonID";
                cmb.DataSource = dgCustomer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgCustomer;
            }
        }


        public static void FillProducts(ComboBox cmb)
        {
            DataTable dgProducts = new DataTable();
            dgProducts.Columns.Add("ProductID");
            dgProducts.Columns.Add("ProductName");
            dgProducts.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select ProductID, ProductName from ProductsTable");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow products in dt.Rows)
                        {
                            dgProducts.Rows.Add(products["ProductID"], products["ProductName"]);
                        }
                    }

                }
                cmb.DisplayMember = "ProductName";
                cmb.ValueMember = "ProductID";
                cmb.DataSource = dgProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgProducts;
            }
        }


        public static void FillFinishedProducts(ComboBox cmb)
        {
            DataTable dgProducts = new DataTable();
            dgProducts.Columns.Add("ProductID");
            dgProducts.Columns.Add("ProductName");
            dgProducts.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select ProductID, ProductName from ProductsTable where Type = 'Finished Goods'");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow products in dt.Rows)
                        {
                            dgProducts.Rows.Add(products["ProductID"], products["ProductName"]);
                        }
                    }

                }
                cmb.DisplayMember = "ProductName";
                cmb.ValueMember = "ProductID";
                cmb.DataSource = dgProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgProducts;
            }
        }

        public static void FillProductsPOS(ComboBox cmb)
        {
            DataTable dgProducts = new DataTable();
            dgProducts.Columns.Add("ProductID");
            dgProducts.Columns.Add("ProductName");
            dgProducts.Rows.Add("0", "-----Select-----");
            try
            {
                DataTable dt = Retrieve("select p.ProductID, p.ProductName from ProductsTable p ");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow products in dt.Rows)
                        {
                            dgProducts.Rows.Add(products["ProductID"], products["ProductName"]);
                        }
                    }

                }
                cmb.DisplayMember = "ProductName";
                cmb.ValueMember = "ProductID";
                cmb.DataSource = dgProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cmb.DataSource = dgProducts;
            }
        }

        public static void ShowProducts(DataGridView dgv, DataGridViewColumn ID, DataGridViewColumn Product, DataGridViewColumn Qty, string data = null)
        {
            try
            {
                SqlCommand cmd = null;
                MainClass.con.Open();

                if (data != "")
                {
                    cmd = new SqlCommand("select p.ProductID, p.ProductName, i.Qty from ProductsTable p inner join Inventory i on i.ProductID = p.ProductID   where p.ProductName like '%" + data + "%' and  i.Qty > 0", MainClass.con);
                }
                else
                {
                    cmd = new SqlCommand("select p.ProductID, p.ProductName, i.Qty from ProductsTable p inner join Inventory i on i.ProductID = p.ProductID where i.Qty > 0", MainClass.con);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ID.DataPropertyName = dt.Columns["ProductID"].ToString();
                Product.DataPropertyName = dt.Columns["ProductName"].ToString();
                Qty.DataPropertyName = dt.Columns["Qty"].ToString();
                dgv.DataSource = dt;
                MainClass.con.Close();
            }
            catch (Exception ex)
            {
                MainClass.con.Close();
                MessageBox.Show(ex.Message);
            }
        }



        public static void UpdateInventory(Int32 ProductID, float Qty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Inventory set Qty = @Qty where ProductID = @ProductID", MainClass.con);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MainClass.con.Close();
                MessageBox.Show(ex.Message);
            }
        }


        public static void ShowSaleReciept(ReportDocument rd, string proc, string param1 = "", object val1 = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(proc, MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (param1 != "")
                {
                    cmd.Parameters.AddWithValue(param1, val1);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rd.Load(Application.StartupPath + "\\Reports\\SalesReciept.rpt");
                rd.SetDataSource(dt);
                rd.PrintToPrinter(1, false, 0, 0);

       

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static void ShowSaleRecieptSavedCustomer(ReportDocument rd,  string proc, string param1 = "", object val1 = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(proc, MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (param1 != "")
                {
                    cmd.Parameters.AddWithValue(param1, val1);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rd.Load(Application.StartupPath + "\\Reports\\SalesReciept.rpt");
                rd.SetDataSource(dt);
                rd.PrintToPrinter(1, false, 0, 0);
                //crv.ReportSource = rd;
                //crv.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public static void ShowPurchaseReceipt(ReportDocument rd, string proc, string param1 = "", object val1 = null)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(proc, MainClass.con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (param1 != "")
                {
                    cmd.Parameters.AddWithValue(param1, val1);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rd.Load(Application.StartupPath + "\\Reports\\PurchaseReciept.rpt");
                rd.SetDataSource(dt);
                rd.PrintToPrinter(1, false, 0, 0);

                //crv.ReportSource = rd;
                //crv.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
