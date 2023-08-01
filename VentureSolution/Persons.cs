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

namespace VentureSolution
{
    public partial class Persons : Form
    {
        int pedit = 0;
        public Persons()
        {
            InitializeComponent();
        }

        private void ShowPersons(DataGridView dgv, DataGridViewColumn ID, DataGridViewColumn Name, DataGridViewColumn Type, DataGridViewColumn Contact, DataGridViewColumn Address, string data = null)
        {
            try
            {
                SqlCommand cmd = null;
                MainClass.con.Open();
                if (data != "")
                {
                    cmd = new SqlCommand("select * from PersonsTable  where Name  like '%" + data + "%' or Type like '%" + data + "%'", MainClass.con);
                }
                else
                {
                    cmd = new SqlCommand("select * from PersonsTable order by Name", MainClass.con);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Name.DataPropertyName = dt.Columns["Name"].ToString();
                ID.DataPropertyName = dt.Columns["PersonID"].ToString();
                Type.DataPropertyName = dt.Columns["Type"].ToString();
                Contact.DataPropertyName = dt.Columns["Contact"].ToString();
                Address.DataPropertyName = dt.Columns["Address"].ToString();
                dgv.DataSource = dt;
                MainClass.con.Close();
            }
            catch (Exception ex)
            {
                MainClass.con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void Persons_Load(object sender, EventArgs e)
        {
            button1.Location = new Point(1150, 0);
            ShowPersons(DGVPersons, PersonIDGV, NameGV, TypeGV, ContactGV, AddressGV);
        }

        private void Clear()
        {
            txtContact.Text = "";
            txtAddress.Text = "";
            txtName.Text = "";
            txtSearch.Text = "";
            cboType.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pedit == 0)
            {
                if (txtName.Text == "" || cboType.Text == "")
                {
                    MessageBox.Show("Please Input Details");
                }
                else
                {
                    try
                    {
                        MainClass.con.Open();
                        SqlCommand cmd = new SqlCommand("insert into PersonsTable (Name,Type,Contact,Address) values(@Name,@Type,@Contact,@Address)", MainClass.con);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Type", cboType.Text);
                        cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.ExecuteNonQuery();
                        MainClass.con.Close();
                        MessageBox.Show("Person Inserted Successfully.");
                        Clear();
                        ShowPersons(DGVPersons, PersonIDGV, NameGV, TypeGV, ContactGV, AddressGV);
                    }
                    catch (Exception ex)
                    {
                        MainClass.con.Close();
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            else
            {
                if (pedit == 1)
                {
                    try
                    {
                        MainClass.con.Open();
                        SqlCommand cmd = new SqlCommand("update PersonsTable set Name = @Name, Type = @Type ,Contact= @Contact, Address = @Address where PersonID = @PersonID", MainClass.con);
                        cmd.Parameters.AddWithValue("@PersonID", lblID.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Type", cboType.Text);
                        cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.ExecuteNonQuery();
                        MainClass.con.Close();
                        MessageBox.Show("Person Updated Successfully.");
                        btnSave.Text = "SAVE";
                        btnSave.BackColor = Color.FromArgb(39, 174, 96);
                        Clear();
                        ShowPersons(DGVPersons, PersonIDGV, NameGV, TypeGV, ContactGV, AddressGV);
                    }
                    catch (Exception ex)
                    {
                        MainClass.con.Close();
                        MessageBox.Show(ex.Message);
                    }

                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pedit = 0;
            if (btnSave.BackColor == Color.Orange)
            {
                btnSave.Text = "SAVE";
                btnSave.BackColor = Color.FromArgb(39, 174, 96);
                Clear();
            }
            else
            {
                if (txtContact.Text == "" &&  txtAddress.Text == "" && txtName.Text == "")
                {

                    this.Dispose();
                    HomeScreen hs = new HomeScreen();
                    hs.Show();
                }
                else
                {
                    Clear();
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pedit = 1;
            lblID.Text = DGVPersons.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = DGVPersons.CurrentRow.Cells[2].Value.ToString();
            cboType.Text = DGVPersons.CurrentRow.Cells[1].Value.ToString();
            txtContact.Text = DGVPersons.CurrentRow.Cells[3].Value.ToString();
            txtAddress.Text = DGVPersons.CurrentRow.Cells[4].Value.ToString();
            btnSave.Text = "UPDATE";
            btnSave.BackColor = Color.Orange;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DGVPersons != null)
            {
                if (DGVPersons.Rows.Count > 0)
                {
                    if (DGVPersons.SelectedRows.Count == 1)
                    {
                        try
                        {
                            MainClass.con.Open();
                            SqlCommand cmd = new SqlCommand("delete from PersonsTable where PersonID = @PersonID", MainClass.con);
                            cmd.Parameters.AddWithValue("@PersonID", DGVPersons.CurrentRow.Cells[0].Value.ToString());
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record Deleted Successfully");
                            MainClass.con.Close();
                            ShowPersons(DGVPersons, PersonIDGV, NameGV, TypeGV, ContactGV, AddressGV);
                        }
                        catch (Exception ex)
                        {
                            MainClass.con.Close();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ShowPersons(DGVPersons, PersonIDGV, NameGV, TypeGV, ContactGV, AddressGV,txtSearch.Text.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            HomeScreen hs = new HomeScreen();
            hs.Show();
        }
    }
}
