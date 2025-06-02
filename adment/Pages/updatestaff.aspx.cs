using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web.UI.WebControls;


namespace adment.Pages
{
    public partial class updatestaff : System.Web.UI.Page
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
                BindGridView();
                addusername(this, null);
                // Disable all fields by default (except Staff ID dropdown)

                txtUpdateAllocatedSchool.Enabled = false;
                txtUpdatePassword.Enabled = false;
                txtUpdateConfirmPassword.Enabled = false;
                txtUpdateName.Enabled = false;
                ddlUpdateStaffDept.Enabled = false;
                ddlUpdateStaffDesignation.Enabled = false;
                ddlUpdateGender.Enabled = false;
                txtUpdateMobileNumber.Enabled = false;
                txtUpdateDOB.Enabled = false;

                // Disable the Update button initially
                btnUpdateStaff.Enabled = false;

                loadids(this,null);
            }
        }

        protected void btnEnableFields_Click(object sender, EventArgs e)
        {
            // Check if a Staff ID is selected
            if (DropDownList1.SelectedValue != "")
            {
                // Enable all the fields

                txtUpdateAllocatedSchool.Enabled = true;
                txtUpdatePassword.Enabled = true;
                txtUpdateConfirmPassword.Enabled = true;
                txtUpdateName.Enabled = true;
                ddlUpdateStaffDept.Enabled = true;
                ddlUpdateStaffDesignation.Enabled = true;
                ddlUpdateGender.Enabled = true;
                txtUpdateMobileNumber.Enabled = true;
                txtUpdateDOB.Enabled = true;

                // Enable the Update button
                btnUpdateStaff.Enabled = true;
            }
            else
            {
                // Display a message if no Staff ID is selected
                ScriptManager.RegisterStartupScript(this, GetType(), "NoStaffIDSelected", "alert('Please select a Staff  USERNAME before enabling fields.');", true);
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
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

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
        static string RemoveSpecialCharacters(string input)
        {
            // Use a regular expression to remove special characters and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "");
        }



        protected void addusername(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

            DropDownList1.Items.Clear();
            DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem("Select username please", ""));
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
                    DropDownList1.Items.Add(option);
                }
            }
        }


        protected void gotodelete(object sender, EventArgs e)
        {
            Response.Redirect("Delete Staff ");
        }


        protected void adddatatofield(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

            SqlConnection a = new SqlConnection(k);
            a.Open();

            // Get the selected value from the DropDownList
            string selectedUsername = DropDownList1.SelectedValue;

            string queryexe = "SELECT * FROM SAFF_DATA WHERE [USERNAME] = @SelectedUsername";

            SqlCommand cmd = new SqlCommand(queryexe, a);
            cmd.Parameters.AddWithValue("@SelectedUsername", selectedUsername);

            SqlDataReader r = cmd.ExecuteReader();

            if (r.Read())
            {
                txtUpdateMobileNumber.Text = r["MOBNO"].ToString();
                txtUpdateName.Text = r["NAME"].ToString();
                txtUpdateDOB.Text = r["DOB"].ToString();
                txtUpdatePassword.Text = r["PASS"].ToString();
                txtUpdateConfirmPassword.Text = r["PASS"].ToString();
                updateStaffid.Text = r["STAFF_ID"].ToString();
            }
        }



        protected void pushdata(object sender, EventArgs e)
        {
            // Check if a Staff ID is selected
            if (DropDownList1.SelectedValue != "")
            {
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

                using (SqlConnection a = new SqlConnection(k))
                {
                    a.Open();

                    // Construct the UPDATE query
                    string updateQuery = "UPDATE SAFF_DATA SET " +
                        "ALLOCATED_SCHOOL = ALLOCATED_SCHOOL + @NewSchool, " +
                        "PASS = @Password, " +
                        "NAME = @Name, " +
                        "STAFF_DEPT = @StaffDept, " +
                        "SAFF_DESIG = @StaffDesignation, " +  // Check if the column name is 'SAFF_DESIG' in your database
                        "GENDER = @Gender, " +
                        "MOBNO = @MobileNumber, " +
                        "DOB = @DateOfBirth " +
                        "WHERE USERNAME = @SelectedUsername";

                    SqlCommand cmd = new SqlCommand(updateQuery, a);

                    // Set the parameters for the UPDATE query
                    cmd.Parameters.AddWithValue("@NewSchool", "," + txtUpdateAllocatedSchool.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtUpdatePassword.Text);
                    cmd.Parameters.AddWithValue("@Name", txtUpdateName.Text);
                    cmd.Parameters.AddWithValue("@StaffDept", ddlUpdateStaffDept.SelectedValue);
                    cmd.Parameters.AddWithValue("@StaffDesignation", ddlUpdateStaffDesignation.SelectedValue);
                    cmd.Parameters.AddWithValue("@Gender", ddlUpdateGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@MobileNumber", txtUpdateMobileNumber.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", txtUpdateDOB.Text);
                    cmd.Parameters.AddWithValue("@SelectedUsername", DropDownList1.SelectedValue);

                    // Execute the UPDATE query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        BindGridView();

                        // Data updated successfully
                        ScriptManager.RegisterStartupScript(this, GetType(), "UpdateSuccess", "alert('Staff data updated successfully.');", true);
                    }
                    else
                    {
                        // Data update failed
                        ScriptManager.RegisterStartupScript(this, GetType(), "UpdateFail", "alert('Failed to update staff data.');", true);
                        BindGridView();

                    }
                }
            }
            else
            {
                // Display a message if no Staff ID is selected
                ScriptManager.RegisterStartupScript(this, GetType(), "NoStaffIDSelected", "alert('Please select a Staff USERNAME before updating.');", true);
            }
            BindGridView();

        }

        protected void cvUpdateAllocatedSchool_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string allocatedSchool = txtUpdateAllocatedSchool.Text;

            // Check if the allocatedSchool contains a comma
            if (allocatedSchool.Contains(","))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }



        protected void loadids(object sender, EventArgs e)
        {
            BindGridView();

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

                using (SqlConnection con = new SqlConnection(k))
                {
                    con.Open();

                    string query = "SELECT [STAFF_ID] FROM [SAFF_DATA]";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Clear existing items in ddlStaffId
                            ddlStaffId.Items.Clear();

                            //ddlStaffId.Items.Add("Select Staff ID ");
                            // Add a default item if needed
                            ddlStaffId.Items.Add(new ListItem("Select Staff ID", ""));

                            while (reader.Read())
                            {
                                // Add staff ID options to ddlStaffId
                                string staffId = reader["STAFF_ID"].ToString();
                                ddlStaffId.Items.Add(new ListItem(staffId, staffId));
                            }
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

        protected void loadschools(object sender, EventArgs e)
        {


            try
            {
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

                using (SqlConnection con = new SqlConnection(k))
                {
                    con.Open();

                    // Get the selected staff ID from ddlStaffId
                    string selectedStaffId = ddlStaffId.SelectedValue;
                    Label10.Text = ddlStaffId.Text;
                    // Query to get the allocated schools for the selected staff ID
                    string query = "SELECT [ALLOCATED_SCHOOL] FROM [SAFF_DATA] WHERE [STAFF_ID] = @StaffId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@StaffId", selectedStaffId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Clear existing items in ddlSchool
                            ddlSchool.Items.Clear();

                            
                            // Add a default item if needed
                            ddlSchool.Items.Add(new ListItem("Select School", ""));

                            while (reader.Read())
                            {
                                // Get allocated schools as a comma-separated string
                                string allocatedSchools = reader["ALLOCATED_SCHOOL"].ToString();

                                // Split the schools and add them as options to ddlSchool
                                string[] schools = allocatedSchools.Split(',');
                                foreach (string school in schools)
                                {
                                    ddlSchool.Items.Add( new ListItem(school,school));
                                }
                            }
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

        protected void ddlStaffId_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Code to load schools based on selected staff ID and update ddlSchool
            loadschools(this , null);
            BindGridView();

            // Manually trigger the update of the UpdatePanel
            deletePanel.Update();
   
        }


        protected void UpdateSchools(string staffId, string selectedSchool)
        {
            try
            {
                // Get the current allocated schools for the staff from the database
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

                using (SqlConnection con = new SqlConnection(k))
                {
                    con.Open();

                    string selectQuery = "SELECT ALLOCATED_SCHOOL FROM SAFF_DATA WHERE STAFF_ID = @StaffId";
                    using (SqlCommand cmdSelect = new SqlCommand(selectQuery, con))
                    {
                        cmdSelect.Parameters.AddWithValue("@StaffId", staffId);
                        object result = cmdSelect.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            string allocatedSchools = result.ToString();

                            // Remove the selected school from the list
                            List<string> schoolsList = allocatedSchools.Split(',').Select(s => s.Trim()).ToList();
                            schoolsList.RemoveAll(s => s.Equals(selectedSchool, StringComparison.OrdinalIgnoreCase));

                            // Update the SAFF_DATA table with the new list of allocated schools
                            string updatedSchools = string.Join(",", schoolsList);
                            string updateQuery = "UPDATE SAFF_DATA SET ALLOCATED_SCHOOL = @UpdatedSchools WHERE STAFF_ID = @StaffId";

                            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, con))
                            {
                                cmdUpdate.Parameters.AddWithValue("@UpdatedSchools", updatedSchools);
                                cmdUpdate.Parameters.AddWithValue("@StaffId", staffId);
                                cmdUpdate.ExecuteNonQuery();
                            }
                            BindGridView();

                            // Delete associated tables
                            DeleteSchoolTables(selectedSchool, con);
                            

                            // Display an alert with the deleted school name
                            string alertMessage = $"School {selectedSchool} has been deleted.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteSchoolAlert", $"alert('{alertMessage}');", true);
                        }
                    }
                }

                // Refresh the dropdown list of schools after the update
                loadschools(null, null);
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                // Log the exception details for debugging
            }
        }

        private void DeleteSchoolTables(string schoolName, SqlConnection con)
        {
            try
            {
                // Construct table names in uppercase and lowercase
                string tableNameUpper = RemoveSpecialCharacters( schoolName.ToUpper());
                string tableNameLower = RemoveSpecialCharacters( RemoveSpecialCharacters(schoolName.ToLower() + "studdata"));

                // Check if tables exist before attempting to delete
                string checkTableQuery = "IF OBJECT_ID(@TableName, 'U') IS NOT NULL DROP TABLE " + tableNameUpper;

                // Flag to check if tables were deleted
                bool tablesDeleted = false;

                using (SqlCommand cmdCheckTable = new SqlCommand(checkTableQuery, con))
                {
                    cmdCheckTable.Parameters.AddWithValue("@TableName", tableNameUpper);
                    tablesDeleted = cmdCheckTable.ExecuteNonQuery() > 0;
                }

                checkTableQuery = "IF OBJECT_ID(@TableName, 'U') IS NOT NULL DROP TABLE " + tableNameLower;

                using (SqlCommand cmdCheckTable = new SqlCommand(checkTableQuery, con))
                {
                    cmdCheckTable.Parameters.AddWithValue("@TableName", tableNameLower);
                    tablesDeleted |= cmdCheckTable.ExecuteNonQuery() > 0;
                }


                BindGridView();

                // Display an alert based on whether tables were deleted
               
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                // Log the exception details for debugging
            }
        }






        protected void btnDeleteSchool_Click(object sender, EventArgs e)
        {
            BindGridView();

            UpdateSchools(ddlStaffId.SelectedItem.Text, ddlSchool.SelectedItem.Text);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminportal.aspx");
        }
    }
}