using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adment.Pages
{
    public partial class adminportal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }

            if (!IsPostBack)
            {
                
                if (Session["Username"] != null)
                {
                    string username = Session["Username"].ToString();
                    lblAdminName.Text = username;
                    lblAdminNameWelcome.Text = username;

                   
                    if (Session["Name"] != null)
                    {
                        string instituteName = Session["Name"].ToString();
                        lblInstituteName.Text = instituteName;
                    }

                    if (Session["CollegeName"] != null)
                    {
                        string collegeName = Session["CollegeName"].ToString();
                        lblCollegeName.Text = collegeName;
                    }
                }
                else
                {
                    
                    Response.Redirect("adminlogon.aspx");
                }
            }

        }

        protected void showpanel(object sender, EventArgs df)
        {


            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }
            panelprof.Visible = true;
           
            txtUsername.Text = Session["Username"].ToString();
            txtFirstName.Text = Session["Name"].ToString();
            txtclgName.Text = Session["CollegeName"].ToString();
           // txtmobno.Text = Session["mobno"].ToString();
        }
        protected void hidepanel(object sender, EventArgs fo)
        {


            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }




            // Retrieve
            string firstName = txtFirstName.Text;
    string instituteName = txtclgName.Text;
    string mobileNumber = txtmobno.Text;
    string newPassword = txtPassword.Text;
            string USRNAME = txtUsername.Text;

    

    // Create a SQL connection
    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        //  update query
        string updateQuery = "UPDATE INSTITUE_LOGON " +
                             "SET NAME = @Name, CLG_NAME = @InstituteName, MOBNO = @MobileNumber, PASS = @NewPassword " +
                             "WHERE USRNAME = @UserNAME";

        // Create a SqlCommand 
        using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
        {
            cmd.Parameters.AddWithValue("@Name", firstName);
            cmd.Parameters.AddWithValue("@InstituteName", instituteName);
            cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);
            cmd.Parameters.AddWithValue("@NewPassword", newPassword);
            cmd.Parameters.AddWithValue("@UserNAME",USRNAME );

            // Execute 
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                // successfully
                string script = "alert('Update successful!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "SuccessAlert", script, true);
                Session.Clear();
                Session.Abandon();
                Response.Redirect("adminportal.aspx");

                
            }
            else
            {
                
            }
        }
    }




            panelprof.Visible = false;
}

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
    }
