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
    public partial class teacherlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["staffUSERNAME"] != null )
            {

                Response.Redirect("teacherpage.aspx");

                //  do nothing  here 
            }
           


        }


        protected void logTeacher(object sender, EventArgs ex)
        {
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection a = new SqlConnection(conString);
            a.Open();
            string qwe = "SELECT  * FROM  INSTITUE_LOGON WHERE USRNAME = @data ";

            SqlCommand cmd = new SqlCommand(qwe, a);
            cmd.Parameters.AddWithValue("@data",txtAdminUsername.Text);

            SqlDataReader r = cmd.ExecuteReader();
            if (r.HasRows) {

                string databaseName = RemoveSpecialCharacters( txtAdminUsername.Text);
                string k1 = conString.Replace("base;", RemoveSpecial(databaseName) + ";");

                string que = "SELECT * FROM SAFF_DATA WHERE  USERNAME = @USERNAME AND PASS= @PASSWORD";

                SqlConnection co = new SqlConnection(k1);
                cmd = new SqlCommand(que, co);
                cmd.Parameters.AddWithValue("@USERNAME", txtUsername.Text);
                cmd.Parameters.AddWithValue("@PASSWORD", txtPassword.Text);
               
                co.Open();
                SqlDataReader dr = cmd.ExecuteReader();
               
                if (dr.HasRows)
                {
                   

                    dr.Read();
                    string script = "alert(' successful!');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "SuccessAlert", script, true);

                    string username = dr["USERNAME"].ToString();

                    string MasterUsername = txtAdminUsername.Text;

                    Session["masterUSERNAME"] = MasterUsername;
                    Session["staffUSERNAME"] = username;
                    Session["staffNAME"] = dr["NAME"].ToString();
                    Session["staffID"] = dr["STAFF_ID"].ToString();
                    Session["staffDEPT"] = dr["STAFF_DEPT"].ToString();
                    Session["staffDESIG"] = dr["SAFF_DESIG"].ToString();
                    Session["staffGENDER"] = dr["GENDER"].ToString();
                    Session["staffMOBNO"] = dr["MOBNO"].ToString();
                    Session["staffDOB"] = dr["DOB"].ToString();
                    Session["SCHOOL"] = dr["ALLOCATED_SCHOOL"].ToString();
                    Response.Redirect("teacherpage.aspx");
                    dr.Close();
                    
                }

                else
                {
                    string script = "alert('invalid password or username');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "SuccessAlert", script, true);
                }


            }
            else
            {

                string script = "alert('invalid Institute Username !');";
                ScriptManager.RegisterStartupScript(this, GetType(), "SuccessAlert", script, true);
            }











            


        }

        protected void at(object sender, EventArgs e)
        {

            Response.Redirect("home.aspx");

        }


        static string RemoveSpecialCharacters(string input)
        {
            // Use a regular expression to remove special characters (excluding comma) and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9,]+", "");
        }

        static string RemoveSpecial(string input)
        {
            // Use a regular expression to remove special characters and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "");
        }
    }
}