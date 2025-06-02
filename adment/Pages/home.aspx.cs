using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adment.Pages
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("adminlogon.aspx");
        }
        public void redirectadmin(object sender, EventArgs e)
        {
            Response.Redirect("adminlogon.aspx");

        }
        public void redirectstaff(object sender, EventArgs e)
        {
            Response.Redirect("teacherlogin.aspx");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogon.aspx");

        }
    }
}