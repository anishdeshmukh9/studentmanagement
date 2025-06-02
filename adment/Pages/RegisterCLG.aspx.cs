using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adment.Pages
{
    public partial class RegisterCLG : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings[0].ConnectionString;

        protected void runme(object sender, EventArgs e)
        {
            // Response.Redirect("adminlogon.aspx"); 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void registerverify(object sender, EventArgs e)
        {

            txtPasswordv.Enabled = false;
            txtConfirmPasswordv.Enabled = false;
            txtActivationKeyv.Enabled = false;

            string str = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection obj = new SqlConnection(str);

            if (obj.State == System.Data.ConnectionState.Closed)
            {
                obj.Open();
            }

            string query = "SELECT COUNT(*) FROM INSTITUE_LOGON WHERE USRNAME = @Username";
            SqlCommand cmd = new SqlCommand(query, obj);
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                // Username already exists
                usernameerror.Visible = true;
                btnRegister.Visible = false;
            }
            else
            {
                // Username is available
                txtUsername.Enabled = false;
                usernameerror.Visible = false;
                btnRegister.Visible = true;
                btnverify.Visible = false;
                txtAdminNamev.Enabled = true;
                txtInstituteName.Enabled = true;
                txtPasswordv.Enabled = true;
                txtConfirmPasswordv.Enabled = true;
                txtActivationKeyv.Enabled = true;

            }
        }

        protected void registerme(object sender, EventArgs ep)
        {

            txtAdminNamev.Enabled = true;
            txtInstituteName.Enabled = true;
            txtPasswordv.Enabled = true;
            txtConfirmPasswordv.Enabled = true;
            txtActivationKeyv.Enabled = true;
            // Insert data into the table
            string str = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection obj = new SqlConnection(str);

            if (obj.State == System.Data.ConnectionState.Closed)
            {
                obj.Open();
            }

            // Insert data 
            string insertQuery = "INSERT INTO INSTITUE_LOGON (CLG_NAME , NAME,  USRNAME, PASS, MOBNO ,email) VALUES (@Name, @CollegeName, @Username, @Password, @MobileNumber , @email)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, obj);

            insertCmd.Parameters.AddWithValue("@Name", txtInstituteName.Text);
            insertCmd.Parameters.AddWithValue("@CollegeName", txtAdminName.Text);
            insertCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            insertCmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            insertCmd.Parameters.AddWithValue("@MobileNumber", txtMobileNumber.Text);
            insertCmd.Parameters.AddWithValue("@email" , txtEmail.Text);
            try
            {


                SqlCommand cd = new SqlCommand("SELECT * FROM ACTIVATION WHERE [KEY] = @KEYdata", obj);
                cd.Parameters.AddWithValue("@KEYdata", txtActivationKey.Text);

                SqlDataReader dr = cd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Close();
                    insertCmd.ExecuteNonQuery();

                
                    SqlCommand cd2 = new SqlCommand("DELETE FROM ACTIVATION WHERE [KEY] = @KEYdata", obj);
                    cd2.Parameters.AddWithValue("@KEYdata", txtActivationKey.Text);
                    cd2.ExecuteNonQuery(); // Execute the DELETE query

                    Response.Redirect("adminlogon.aspx");
                }
                else
                {
                    activationerror.Visible = true;
                }

}
            catch (Exception c)
            {


            }
                obj.Close();

                
            }

        }


    }
