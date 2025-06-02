using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adment.Pages
{
    public partial class deletestaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Username"] != null)
            {
                if(!IsPostBack)
                addusername(this, null);


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }

        }


        protected void addusername(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

            ddlStaffToDelete.Items.Clear();
            ddlStaffToDelete.Items.Add(new System.Web.UI.WebControls.ListItem("Select username please", ""));
            SqlConnection a = new SqlConnection(k);
            a.Open();

            string queryexe = "SELECT USERNAME FROM SAFF_DATA"; // Only select the USERNAME column

            SqlCommand cmd = new SqlCommand(queryexe, a);

            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                string[] options = r["USERNAME"].ToString().Split(',');

                foreach (string option in options)
                {
                    ddlStaffToDelete.Items.Add(option);
                }
            }
        }

        protected void deletestaffrecord(object sender, EventArgs e)
        {
            if (ddlStaffToDelete.SelectedValue != "")
            {
                string selectedUsername = ddlStaffToDelete.SelectedValue;

                try
                {
                    string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

                    using (SqlConnection connection = new SqlConnection(k))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM SAFF_DATA WHERE USERNAME = @Username";

                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@Username", selectedUsername);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {

                                // Record deleted successfully
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "DeletedSuccessfully", "alert('Staff member deleted successfully.');", true);
                            }
                            else
                            {
                                // No record deleted, handle the case where the username does not exist
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "DeletionFailed", "alert('No matching staff member found with the selected username.');", true);
                            }



                            addusername(this, null);

                        }
                    }

                    // Rebind the dropdown list to reflect the changes
                   
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the database operation
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "DeletionError", "alert('An error occurred while deleting the staff member.');", true);
                }
            }
            else
            {
                // Display a message if no username is selected for deletion
                Page.ClientScript.RegisterStartupScript(this.GetType(), "NoStaffSelected", "alert('Please select a staff member to delete.');", true);
            }
        }


        static string RemoveSpecialCharacters(string input)
        {
            // Use a regular expression to remove special characters and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminportal.aspx");
        }
    }
}