using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio.Rest.Studio.V2.Flow;
using System.IO;

namespace adment.Pages
{
    public partial class teacherpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["staffUSERNAME"] != null)
                {
                    selectedOption(this, null);

                    lblTeacherName.Text = Session["staffNAME"].ToString();
                    lblStaffID.Text = Session["staffID"].ToString();
                    lblStaffDept.Text = Session["staffDEPT"].ToString();
                    lblStaffDesignation.Text = Session["staffDESIG"].ToString();
                    lblStaffGender.Text = Session["staffGENDER"].ToString();
                    lblStaffMobileNumber.Text = Session["staffMOBNO"].ToString();
                    lblStaffDOB.Text = Session["staffDOB"].ToString();
                    ProcessProfileData();
                    string allocatedSchools = Session["SCHOOL"]?.ToString();

                    if (!string.IsNullOrEmpty(allocatedSchools))
                    {
                        string[] options = allocatedSchools.Split(',');

                        DropDownList1.Items.Clear();
                        DropDownList1.Items.Add("select school");

                        foreach (string option in options)
                        {
                            if(option != "")
                            DropDownList1.Items.Add(option);
                        }

                        string formattedSchools = allocatedSchools.Replace(",", "</br> <hr>");
                        formattedSchools = "</br> <hr>" + formattedSchools;
                        lblAllocatedSchool.Text = formattedSchools.ToUpper();

                        createtableforSchool(this, null);
                        CalculateAndDrawChart();
                        ProcessProfileData();
                    }
                    else
                    {
                        // Handle the case when Session["SCHOOL"] is empty
                        // You can provide a default behavior or show a message to the user
                    }
                }
                else
                {
                    Response.Redirect("teacherlogin.aspx");
                }
            }




        }



        protected void selectedOption(object sender, EventArgs e)
        {
            CalculateAndDrawChart();
            if (DropDownList1.SelectedItem != null)
            {
                
                txtSchoolName.Text = DropDownList1.SelectedItem.Text;
                string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = con.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + "; ");
                SqlConnection co = new SqlConnection(k);
                string tableName = RemoveSpecialCharacters(DropDownList1.SelectedItem.Text.ToUpper().Replace(" ", "").Replace("-", "_")); // Replace hyphens with underscores
                string retrieveDataQuery = "SELECT * FROM " + tableName; // Use the modified table name
                SqlCommand cmd = new SqlCommand(retrieveDataQuery, co);
                co.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Populate the form fields with the retrieved data
                        txtSchoolName.Text = reader["NAME"].ToString();
                        txtAddAddress.Text = reader["ADD"].ToString();
                        ddlGrade.SelectedValue = reader["GRADE"].ToString();
                        txtTotalStudents.Text = reader["TOTALSTUDS"].ToString();
                        txtSchoolContact.Text = reader["CONTACT"].ToString();
                    }
                }

                else
                {

                    txtAddAddress.Text ="";
                    txtTotalStudents.Text = "";
                    txtSchoolContact.Text = "";

                }

                // Set the form fields to read-only
                txtSchoolName.ReadOnly = true;
                txtAddAddress.ReadOnly = true;
                ddlGrade.Enabled = false; // Disable the dropdown to make it read-only
                txtTotalStudents.ReadOnly = true;
                txtSchoolContact.ReadOnly = true;
            }
        }


        protected void createtableforSchool(object sender, EventArgs e)
        {
            CalculateAndDrawChart();
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + ";");

                using (SqlConnection con = new SqlConnection(k))
                {
                    con.Open();

                    string tableName = RemoveSpecialCharacters(DropDownList1.SelectedItem.Text.ToUpper().Replace(" ", ""));

                    // Check if the table already exists
                    string checkTableQuery = "IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @SCHOOL) BEGIN " +
                                             "    CREATE TABLE [" + RemoveSpecialCharacters(tableName) + "] (" +
                                             "        NAME NVARCHAR(255), " +
                                             "        [ADD] NVARCHAR(MAX), " +
                                             "        GRADE NVARCHAR(255), " +
                                             "        TOTALSTUDS NVARCHAR(MAX), " +
                                             "        CONTACT NVARCHAR(255) " +
                                             "    ) " +
                                             "END";

                    using (SqlCommand cmd = new SqlCommand(checkTableQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@SCHOOL", RemoveSpecialCharacters(tableName));
                        int tableUpdateResult = cmd.ExecuteNonQuery();

                        if (tableUpdateResult >= 0)
                        {
                            // Table was created or already exists

                            // Perform an INSERT operation here to add data to the table
                        }
                        else
                        {
                            // Table creation/update failed
                            // Handle the failure here
                        }
                    }
                }

                selectedOption(this, null);
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                // Log the exception details for debugging
            }
        }




        protected void ProcessProfileData()
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + ";");

                using (SqlConnection con = new SqlConnection(k))
                {
                    con.Open();

                    // Create or check existence of the profile_data table
                    string createTableQuery = "IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'profile_data') BEGIN " +
                                             "    CREATE TABLE [profile_data] (" +
                                             "        staffid NVARCHAR(255) PRIMARY KEY, " +
                                             "        pathofphoto NVARCHAR(MAX) " +
                                             "    ) " +
                                             "END";

                    using (SqlCommand createTableCmd = new SqlCommand(createTableQuery, con))
                    {
                        createTableCmd.ExecuteNonQuery();
                    }

                    // Check if the staff ID exists in the profile_data table
                    string checkStaffIdQuery = "IF NOT EXISTS (SELECT 1 FROM profile_data WHERE staffid = @StaffId) BEGIN " +
                                               "    INSERT INTO profile_data (staffid, pathofphoto) VALUES (@StaffId, @PathOfPhoto) " +
                                               "END";

                    using (SqlCommand checkStaffIdCmd = new SqlCommand(checkStaffIdQuery, con))
                    {
                        checkStaffIdCmd.Parameters.AddWithValue("@StaffId", Session["staffID"].ToString());
                        checkStaffIdCmd.Parameters.AddWithValue("@PathOfPhoto", "df");

                        checkStaffIdCmd.ExecuteNonQuery();
                    }

                    // Load the path of the photo into the session variable
                    string loadPathQuery = "SELECT pathofphoto FROM profile_data WHERE staffid = @StaffId";

                    using (SqlCommand loadPathCmd = new SqlCommand(loadPathQuery, con))
                    {
                        loadPathCmd.Parameters.AddWithValue("@StaffId", Session["staffID"].ToString());

                        object result = loadPathCmd.ExecuteScalar();
                        if (result != null)
                        {
                            Session["path"] = result.ToString();
                            if (Session["path"].ToString() != "df")
                            imgProfile.ImageUrl = Session["path"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                // Log the exception details for debugging
            }
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    // Check if Session["staff_id"] is not null
                    if (Session["staffID"] != null)
                    {
                        string staffId = Session["staffID"].ToString();

                        // Save the uploaded file to the server
                        string uploadFolderPath = Server.MapPath("~/imgdata/staff/");
                        string fileName = Path.GetFileName(fileUpload.FileName);
                        string filePath = Path.Combine(uploadFolderPath, fileName);
                        fileUpload.SaveAs(filePath);

                        // Update the profile_data table with the new path
                        UpdateProfileData(staffId, filePath , fileName);

                        // Update the session variable
                        Session["path"] = filePath;

                        // Update the profile image
                        imgProfile.ImageUrl = filePath;

                        lblMessage.Text = "Image uploaded successfully!";
                    }
                    else
                    {
                        lblMessage.Text = "Session variable 'staff_id' is null. Please log in again.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error uploading image: " + ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "Please select an image to upload.";
            }
        }


        private void UpdateProfileData(string staffId, string filePath , string fname)
        {
            try
            {
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + ";");

                using (SqlConnection con = new SqlConnection(k))
                {
                    con.Open();

                    string updatePathQuery = "UPDATE profile_data SET pathofphoto = @PathOfPhoto WHERE staffid = @StaffId";

                    using (SqlCommand updatePathCmd = new SqlCommand(updatePathQuery, con))
                    {
                        updatePathCmd.Parameters.AddWithValue("@StaffId", staffId);
                        updatePathCmd.Parameters.AddWithValue("@PathOfPhoto", "~/imgdata/staff/"+fname);

                        updatePathCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                // Log the exception details for debugging
            }
        }


        protected void update_the_data(object sender, EventArgs e)
        {
            string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = con.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + ";");

            using (SqlConnection co = new SqlConnection(k))
            {
                co.Open();

                // Check if a row with the given NAME already exists
                string checkIfExistsQuery = "SELECT COUNT(*) FROM " + RemoveSpecialCharacters( DropDownList1.SelectedItem.Text.ToUpper().Replace(" ", "")) +
                                            " WHERE NAME = @NAME";
                SqlCommand checkCmd = new SqlCommand(checkIfExistsQuery, co);
                checkCmd.Parameters.AddWithValue("@NAME", RemoveSpecialCharacters( txtSchoolName.Text));

                int rowCount = (int)checkCmd.ExecuteScalar();

                if (rowCount > 0)
                {
                    // Row with the given NAME exists, update it
                    string updateDataQuery = "UPDATE " + RemoveSpecialCharacters(DropDownList1.SelectedItem.Text.ToUpper().Replace(" ", "")) + " " +
                                             "SET [ADD] = @ADD, GRADE = @GRADE, TOTALSTUDS = @TOTALSTUDS, CONTACT = @CONTACT " +
                                             "WHERE NAME = @NAME";
                    SqlCommand updateCmd = new SqlCommand(updateDataQuery, co);
                    updateCmd.Parameters.AddWithValue("@NAME", txtSchoolName.Text);
                    updateCmd.Parameters.AddWithValue("@ADD", txtAddAddress.Text);
                    updateCmd.Parameters.AddWithValue("@GRADE", ddlGrade.SelectedValue);
                    updateCmd.Parameters.AddWithValue("@TOTALSTUDS", txtTotalStudents.Text);
                    updateCmd.Parameters.AddWithValue("@CONTACT", txtSchoolContact.Text);

                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    // Row with the given NAME doesn't exist, insert it
                    string insertDataQuery = "INSERT INTO " + RemoveSpecialCharacters(DropDownList1.SelectedItem.Text.ToUpper().Replace(" ", "")) + " " +
                                             "(NAME, [ADD], GRADE, TOTALSTUDS, CONTACT) " +
                                             "VALUES (@NAME, @ADD, @GRADE, @TOTALSTUDS, @CONTACT)";
                    SqlCommand insertCmd = new SqlCommand(insertDataQuery, co);
                    insertCmd.Parameters.AddWithValue("@NAME", txtSchoolName.Text);
                    insertCmd.Parameters.AddWithValue("@ADD", txtAddAddress.Text);
                    insertCmd.Parameters.AddWithValue("@GRADE", ddlGrade.SelectedValue);
                    insertCmd.Parameters.AddWithValue("@TOTALSTUDS", txtTotalStudents.Text);
                    insertCmd.Parameters.AddWithValue("@CONTACT", txtSchoolContact.Text);

                    insertCmd.ExecuteNonQuery();
                }

                // Set the form fields to read-only
                txtSchoolName.ReadOnly = true;
                txtAddAddress.ReadOnly = true;
                ddlGrade.Enabled = false; // Disable the dropdown to make it read-only
                txtTotalStudents.ReadOnly = true;
                txtSchoolContact.ReadOnly = true;
            }
        }



    
 



    protected void enableFields(object sender, EventArgs e)
        {
            if(DropDownList1.SelectedItem.Text != "select school")
            txtSchoolName.ReadOnly = false;
            txtAddAddress.ReadOnly = false;
            ddlGrade.Enabled = true; // Disable the dropdown to make it read-only
            txtTotalStudents.ReadOnly = false;
            txtSchoolContact.ReadOnly = false;

        }

        protected void redirectFields(object sender, EventArgs e) {
            if(DropDownList1.SelectedItem.Text != "select school")
            Session["school_data_feed"] = DropDownList1.Text;
            Response.Redirect("feedData.aspx");
        }


        static string RemoveSpecialCharacters(string input)
        {
            // Use a regular expression to remove special characters and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "");
        }

        protected void CalculateAndDrawChart()
        {
            string con = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = con.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + ";");

            double completedPercentage = 0;
            double remainingPercentage = 0;

            // Check if Session["SCHOOL"] is not null or empty
            if (!string.IsNullOrEmpty(Session["SCHOOL"]?.ToString()))
            {
                SqlConnection a = new SqlConnection(con);

                int rows = GetTotalRowsCount(k, "");
                int total = GetTotalStudentsCount(k, "");

                completedPercentage = (double)rows / total * 100;
                remainingPercentage = 100 - completedPercentage;

                // Rest of the code...
            }

            // Load Google Charts library
            string googleChartsScript = "<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>";
            ClientScript.RegisterStartupScript(this.GetType(), "GoogleChartsLibrary", googleChartsScript);

            // Generate and register JavaScript code to draw the chart
            string script = $@"
        <script type='text/javascript'>
            google.charts.load('current', {{ 'packages': ['corechart'] }});
            google.charts.setOnLoadCallback(function() {{
                var data = google.visualization.arrayToDataTable([
                    ['Task', 'Progress'],
                    ['Completed', {completedPercentage}],
                    ['Remaining', {remainingPercentage}],
                ]);

                var options = {{
                    title: 'Progress Chart',
                    pieHole: 0.4,
                }};

                var chart = new google.visualization.PieChart(document.getElementById('progressChart'));
                chart.draw(data, options);
            }});
        </script>";

            ClientScript.RegisterStartupScript(this.GetType(), "DrawChartScript", script);
        }





        protected int GetTotalRowsCount(string connectionString, string school)
        {



         



            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Construct the table name based on the selected school
                string tableName = RemoveSpecialCharacters(DropDownList1.Text.ToLower().Replace(" ", "") + "studdata");

                // Check if the table exists
                string checkTableQuery = $"IF OBJECT_ID('{tableName}', 'U') IS NULL SELECT 0 ELSE SELECT COUNT(*) FROM {tableName}";

                using (SqlCommand cmd = new SqlCommand(checkTableQuery, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        int rowCount = Convert.ToInt32(result);

                        // Check if the rowCount is 0, which means the table exists but has no rows
                        if (rowCount == 0)
                        {
                            // Handle the case when the table exists but has no rows
                            // You can return 0 or handle it as needed
                            return 1;
                        }

                        return rowCount;

                    }
                    else
                    {
                        // Handle the case when the table doesn't exist
                        // You can show an alert using JavaScript here
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "TableNotExistAlert", "alert('The table does not exist.');", true);

                        // Return a default value or throw an exception as needed
                        return 1; // or throw an exception
                    }
                }
            }
        }

        protected int GetTotalStudentsCount(string connectionString, string school)
        {


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Construct the table name based on the selected school
                string tableName = RemoveSpecialCharacters(DropDownList1.Text.Replace(" ", "").ToUpper());

                if (tableName == "" || tableName == null)
                {
                    tableName = "testing";
                }

                // Check if the table exists
                string checkTableQuery = $"IF OBJECT_ID('{RemoveSpecialCharacters(tableName)}', 'U') IS NULL SELECT 0 ELSE SELECT [TOTALSTUDS] FROM {RemoveSpecialCharacters(tableName)}";

                using (SqlCommand cmd = new SqlCommand(checkTableQuery, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        int totalStuds = Convert.ToInt32(result);

                        // Check if the totalStuds value is 0, which means the table exists but the value is null or 0
                        if (totalStuds == 0)
                        {
                            // Handle the case when the value exists but is 0 or null
                            // You can return 0 or handle it as needed
                            return 1;
                        }

                        return totalStuds;

                    }
                    else
                    {
                        // Handle the case when the table doesn't exist
                        // You can show an alert using JavaScript here
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "TableNotExistAlert", "alert('The table does not exist.');", true);
                        // Return a default value or throw an exception as needed
                        return 1; // or throw an exception
                    }
                }
            }
        }










    }











}
