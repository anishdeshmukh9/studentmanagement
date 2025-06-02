
++++++++++++++++++++++++++++++++++++++++using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;



namespace adment.Pages
{
    public partial class addStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {

                if (Session["Username"] != null)
                {

                    // 

                    // Initialize the Gender dropdown list
                   // ddlGender.Items.Add(new ListItem("Select Gender", ""));
                  //  ddlGender.Items.Add(new ListItem("Male", "Male"));
                  //  ddlGender.Items.Add(new ListItem("Female", "Female"));

                    // Initialize the Department dropdown list
                  //  ddlStaffDept.Items.Add(new ListItem("Select Department", ""));
                   // ddlStaffDept.Items.Add(new ListItem("Computer", "Computer"));
                  //  ddlStaffDept.Items.Add(new ListItem("Electrical", "Electrical"));
                  //  ddlStaffDept.Items.Add(new ListItem("Mechanical", "Mechanical"));
                  //  ddlStaffDept.Items.Add(new ListItem("Civil", "Civil"));
                  //  ddlStaffDept.Items.Add(new ListItem("Electronics", "Electronics"));
                  //  ddlStaffDept.Items.Add(new ListItem("Fashion", "Fashion"));
                  //  ddlStaffDept.Items.Add(new ListItem("Interior", "Interior"));
                   // ddlStaffDept.Items.Add(new ListItem("Other", "Other"));











                    //
                    string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    string k = con.Replace("base;", RemoveSpecialCharacters( Session["Username"].ToString()) + ";");
                    SqlDataSource1.ConnectionString = k;
                    SqlConnection co = new SqlConnection(k);
                    co.Open(); // Correct method name is Open

                    // Check if the table already exists
                    string checkTableQuery = "IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SAFF_DATA') BEGIN " +
    "CREATE TABLE SAFF_DATA (" +
    "    USERNAME NVARCHAR(255), " +        
    "    PASS NVARCHAR(255), " +            
    "    ALLOCATED_SCHOOL NVARCHAR(MAX), " +
    "    SCHOOL_ADD NVARCHAR(MAX), " +      
    "    NAME NVARCHAR(255), " +          
    "    STAFF_ID INT, " +
    "    STAFF_DEPT NVARCHAR(255), " +    
    "    SAFF_DESIG NVARCHAR(255), " +     
    "    GENDER NVARCHAR(255), " +        
    "    MOBNO NVARCHAR(20), " +           
    "    DOB DATE, " +
    "    ID INT PRIMARY KEY IDENTITY(1,1)" +
    ") END";
                    SqlCommand cmd = new SqlCommand(checkTableQuery, co);

                    cmd.ExecuteNonQuery();

                }
                else
                {

                    Response.Redirect("adminlogon.aspx");
                }
            }


            if (!IsPostBack)
            {
                BindGridView(); // Initial data binding when the page loads
               
            }   

        }


        protected void btnAddStaffData_Click(object sender, EventArgs e)
        {


            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }
            string username = txtUsername.Text.Trim();
            int staffid = Convert.ToInt32(txtStaffID.Text);

            // Check if the username already exists
            if (!IsUsernameAvailable(username))
            {
                // Username already exists, show an error alert
                ScriptManager.RegisterStartupScript(this, GetType(), "UsernameExistsAlert", "alert('Username already exists. Please choose a different username.');", true);
                return;
            }

            if (!IsIDAvailable(staffid))
            {
                // Username already exists, show an error alert
                ScriptManager.RegisterStartupScript(this, GetType(), "IDExistsAlert", "alert('STAFF ID already exists. Please choose a different ID.');", true);
                return;
            }


            // If username is available, proceed with INSERT operation
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters( Session["Username"].ToString()) + ";");
            SqlConnection co = new SqlConnection(k);
            using (SqlConnection con = new SqlConnection(k))
            {
                con.Open();
                string insertQuery = "INSERT INTO SAFF_DATA (USERNAME, PASS, ALLOCATED_SCHOOL, SCHOOL_ADD, NAME, STAFF_ID, STAFF_DEPT, SAFF_DESIG, GENDER, MOBNO, DOB) " +
                     "VALUES (@Username, @Password, @AllocatedSchool, 'na' , @Name, @StaffID, @StaffDept, @StaffDesignation, @Gender, @MobileNumber, @DOB)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 255).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = txtPassword.Text.Trim();
                    cmd.Parameters.Add("@AllocatedSchool", SqlDbType.NVarChar, 255).Value = txtAllocatedSchool.Text.Trim();
                   // cmd.Parameters.Add("@SchoolAddress", SqlDbType.NVarChar, 255).Value = txtSchoolAddress.Text.Trim();
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = txtName.Text.Trim();
                    cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Convert.ToInt32(txtStaffID.Text.Trim());
                    cmd.Parameters.Add("@StaffDept", SqlDbType.NVarChar, 255).Value = ddlStaffDept.SelectedValue.ToString();
                    cmd.Parameters.Add("@StaffDesignation", SqlDbType.NVarChar, 255).Value = ddlStaffDesignation.SelectedValue.ToString();
                    cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 255).Value = ddlGender.SelectedValue.ToString();
                    cmd.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 20).Value = txtMobileNumber.Text.Trim();
                    cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(txtDOB.Text);
                     cmd.ExecuteNonQuery();
                     BindGridView();
                
                }
            }
        }

        private bool IsUsernameAvailable(string username)
        {




            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }











            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters( Session["Username"].ToString()) + ";");
      
            using (SqlConnection con = new SqlConnection(k))
            {
                con.Open();
                string checkUsernameQuery = "SELECT COUNT(*) FROM SAFF_DATA WHERE USERNAME = @Username";

                using (SqlCommand cmd = new SqlCommand(checkUsernameQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = (int)cmd.ExecuteScalar();

                    // If count is 0, the username is available; otherwise, it already exists
                    return count == 0;
                }
            }
        }

        private bool IsIDAvailable(int staff)
        {


            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }












            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("adment;", RemoveSpecialCharacters( Session["Username"].ToString()) + ";");

            using (SqlConnection con = new SqlConnection(k))
            {
                con.Open();
                string checkUsernameQuery = "SELECT COUNT(*) FROM SAFF_DATA WHERE STAFF_ID  = @Username";

                using (SqlCommand cmd = new SqlCommand(checkUsernameQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Username", staff);
                    int count = (int)cmd.ExecuteScalar();

                    // If count is 0, the username is available; otherwise, it already exists
                    return count == 0;
                }
            }
        }
        protected void BindGridView()
        {




            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }









            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters( Session["Username"].ToString()) + ";");

            using (SqlConnection con = new SqlConnection(k))
            {
                con.Open();
                // Query to retrieve data from your database
                string selectQuery = "SELECT * FROM SAFF_DATA";

                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow s in dt.Rows)
                    {
                        string school = Convert.ToString(s["ALLOCATED_SCHOOL"]);
                        string d = school.Replace(",", "</br> <hr>");
                        d = "<hr>" + d;
                        s["ALLOCATED_SCHOOL"] = d.ToString();
                    }
                    gvStaffList.DataSource = dt;
                    gvStaffList.DataBind();
                   
                }
            }
        }

        protected string GetProfileImagePath(object staffId)
        {
            string imagePath = string.Empty;

            // Query the profile_data table to get the image path based on staff ID
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

            using (SqlConnection con = new SqlConnection(k))
            {
                con.Open();
                string selectImagePathQuery = "SELECT [pathofphoto] FROM [admentin_mitpoly1171admin].[admentin_admin].[profile_data] WHERE [staffid] = @StaffId";
                using (SqlCommand cmd = new SqlCommand(selectImagePathQuery, con))
                {
                    cmd.Parameters.AddWithValue("@StaffId", staffId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        imagePath = result.ToString();
                    }
                    else
                    {
                        imagePath = "~/imgdata/staff/teacher.png";
                    }
                }
            }

            return imagePath;
        }

        protected void btnPrintToPdf_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "GridViewExport.pdf";
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename={fileName}");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document pdfDoc = new Document())
                    {
                        PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();

                        // Create a PdfPTable with the same number of columns as GridView
                        PdfPTable table = new PdfPTable(gvStaffList.Columns.Count);

                        // Add headers to the table
                        foreach (DataControlField column in gvStaffList.Columns)
                        {
                            table.AddCell(column.HeaderText);
                        }

                        // Add data from GridView to the table
                        for (int i = 0; i < gvStaffList.Rows.Count; i++)
                        {
                            foreach (DataControlField column in gvStaffList.Columns)
                            {
                                int columnIndex = gvStaffList.Columns.IndexOf(column);

                                // Check if the column is visible before adding the cell
                                if (columnIndex != -1 && column.Visible)
                                {
                                    if (column is TemplateField)
                                    {
                                        // For TemplateField, access the controls within the ItemTemplate
                                        TableCell cell = gvStaffList.Rows[i].Cells[columnIndex];
                                        StringBuilder cellText = new StringBuilder();

                                        foreach (Control control in cell.Controls)
                                        {
                                            if (control is Label)
                                            {
                                                cellText.Append(((Label)control).Text);
                                            }
                                            else if (control is Literal)
                                            {
                                                cellText.Append(((Literal)control).Text);
                                            }
                                            // Add more conditions based on the controls used in the template
                                        }

                                        table.AddCell(cellText.ToString());
                                    }
                                    else
                                    {
                                        // For other types of fields (e.g., BoundField), directly add the cell's text
                                        table.AddCell(gvStaffList.Rows[i].Cells[columnIndex].Text);
                                    }
                                }
                            }
                        }

                        pdfDoc.Add(table);
                    } // Ensure that the Document is closed here

                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine(ex.ToString());
                // Handle exceptions here
            }
        }








        static string RemoveSpecialCharacters(string input)
        {
            // Use a regular expression to remove special characters and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "");
        }

        static string Rem(string input)
        {
            // Use a regular expression to remove special characters (excluding comma) and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9,]+", "");
        }


        protected void gotoupdate(object sender, EventArgs e)
        {
            Response.Redirect("updatestaff.aspx");
        }

        protected void gotodelete(object sender, EventArgs e)
        {
            Response.Redirect("deletestaff.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminportal.aspx");
        }
    }
}