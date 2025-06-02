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
    public partial class adminlogon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Response.Redirect("adminportal.aspx");

            }
        }

        protected void redirectToPortal(object sender, EventArgs pl)
        {
            Response.Redirect("adminportal.aspx");

        } 

        protected void btnRegister_Click(object sender, EventArgs e)
        {

 

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Connect to the database 
            string str = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();
                string query = "SELECT [NAME], [CLG_NAME], [USRNAME], [ID] FROM [INSTITUE_LOGON] WHERE [USRNAME] = @Username AND [PASS] = @Password";
               
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // User is authenticated; store the username in a session
                            reader.Read();
                            string Name = reader["NAME"].ToString();
                            string collegeName = reader["CLG_NAME"].ToString();
                            int userId = Convert.ToInt32(reader["ID"]);
                            //long mob = Convert.ToInt64(reader["MOBNO"]);
                            // Store user information in session for later use
                            Session["Username"] = username;
                            Session["Name"] = Name;
                            Session["CollegeName"] = collegeName;
                            Session["UserId"] = userId;
                            //Session["mobno"] = mob;


                            LinkButton b1 = (LinkButton) Page.Master.FindControl("logoutbtn");
                            b1.Visible = true;

                            // Redirect to the dashboard or another page
                         
                            loginsuccess.Visible = true;
                            loginsuccess.Text = "Hello ! " + Session["Username"].ToString() + " Go To Your Portal ";
                            lblErrorMessage.Text = "Welcome ! " + Session["Username"].ToString() + " 😊😊😊 ";
                            btnRegister.Visible = false;
                            txtPassword.Visible = false;
                            txtUsername.Visible = false;
                            LinkButton1.Visible=false;

                            reader.Close();

                            // create dabase for this institute 16-09-2023

                            string dbName = RemoveSpecialCharacters(Session["Username"].ToString());
                            string checkDbQuery = "SELECT COUNT(*) FROM sys.databases WHERE name = @dbname";

                            using (SqlCommand checkDbCommand = new SqlCommand(checkDbQuery, connection))
                            {
                                checkDbCommand.Parameters.AddWithValue("@dbname", dbName);
                                int dbCount = (int)checkDbCommand.ExecuteScalar();

                                if (dbCount > 0)
                                {
                                    // The database already exists; handle this situation
                                    // You can choose to create a different database name or take other action
                                    string script = "alert('Database already exists. Choose a different database name.');";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", script, true);
                                }
                                else
                                {
                                    string a = Session["Username"].ToString();
                                    a = RemoveSpecialCharacters(a);

                                    if (!string.IsNullOrEmpty(a))
                                    {
                                        string test = "CREATE DATABASE [" + a + "]";
                                        try
                                        {
                                            using (SqlCommand ls = new SqlCommand(test, connection))
                                            {
                                                ls.ExecuteNonQuery();
                                            }
                                        }
                                        catch (SqlException ex)
                                        {
                                            // Handle the exception, for example, show an alert with the error message
                                            
                                        }
                                    }
                                    else
                                    {
                                        string script = "alert('Something went wrong. Register again and do not use symbols in the username!');";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", script, true);
                                    }
                                }
                            }





                        }
                        else
                        {
                            // Invalid credentials; display an error message
                            lblErrorMessage.Text = "Invalid username or password.";
                            lblErrorMessage.Visible = true;

                        }
                    }
                }

            }
        }


        static string RemoveSpecialCharacters(string input)
        {
            // Use a regular expression to remove special characters and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "");
        }

        protected void redirectlink(object sender, EventArgs e)
        {
            Response.Redirect("resetpass.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");

        }
    }
}