using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adment.Source
{
    public partial class adment : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Username"] != null  || Session["staffUSERNAME"] != null)
            {

                logoutbtn.Visible =  true;
            }

            else
            {
                logoutbtn.Visible=false;
            }

        }

        protected  void logout(object sender, EventArgs e)
        {

            Session["Username"] = null;

            Session["masterUSERNAME"] = null;

            Session["staffUSERNAME"] = null;

            logoutbtn.Visible = false;



        }



        protected void GoBack_Click(object sender, EventArgs e)
        {
            string url = Request.UrlReferrer?.ToString();

            // Check if the URL is not null, then redirect to the previous page
            if (!string.IsNullOrEmpty(url))
            {
                Response.Redirect(url);
            }
            else
            {
                // If the URL is null, you can redirect to a default page or homepage
                Response.Redirect("~/Default.aspx");
            }
        }
    }

        


    
}