using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Crypto.Generators;

namespace adment.Pages
{
    public partial class track_activity : System.Web.UI.Page
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
                // Populate dropdown1 with staff IDs from the database
                PopulateStaffDropdown();
            }
        }


        protected void PopulateStaffDropdown()
        {

            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }


            // Use your database connection string
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

            using (SqlConnection con = new SqlConnection(k))
            {
                con.Open();

                // Check if the table exists
                string tableNameToCheck = "SAFF_DATA"; // Change this to the table name you want to check
                string tableExistsQuery = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableNameToCheck}'";

                using (SqlCommand checkCmd = new SqlCommand(tableExistsQuery, con))
                {
                    int tableCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (tableCount > 0)
                    {
                        // The table exists, so now you can fetch staff IDs
                        string query = "SELECT DISTINCT [STAFF_ID] FROM SAFF_DATA";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    dropdown1.Items.Add(new System.Web.UI.WebControls.ListItem(reader["STAFF_ID"].ToString()));
                                }
                            }
                        }
                    }
                    else
                    {
                        // The table does not exist, handle accordingly (e.g., redirect or show an error)
                        Response.Redirect("addStaff.aspx"); // Redirect to an error page
                    }
                }
            }

        }


        protected void Dropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }


            // When staff dropdown selection changes, populate the school dropdown based on selected staff
            PopulateSchoolDropdown();
        }

        protected void PopulateSchoolDropdown()
        {


            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }









            // Clear previous items
            dropdown2.Items.Clear();
            dropdown2.Items.Add(new System.Web.UI.WebControls.ListItem("Select School", ""));

            // Get the selected staff ID
            string selectedStaff = dropdown1.SelectedValue;

            // Use your database connection string
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

            using (SqlConnection con = new SqlConnection(k))
            {
                con.Open();

                // Fetch allocated schools based on the selected staff
                string query = $"SELECT [ALLOCATED_SCHOOL] FROM SAFF_DATA WHERE [STAFF_ID] = @StaffID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StaffID", selectedStaff);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string[] schools = reader["ALLOCATED_SCHOOL"].ToString().Split(',');
                            foreach (string school in schools)
                            {
                                if (school == "" || school == null)
                                {
                                    continue;   
                                }


                                dropdown2.Items.Add(new System.Web.UI.WebControls.ListItem(school.Trim()));
                            }
                        }
                    }
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {


            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }













            // Calculate progress percentages and update the chart
            CalculateAndDrawChart();
        }

        protected void CalculateAndDrawChart()
        {

            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }









            // ... Rest of your code ...


            // Get the selected school from dropdown2
            string selectedSchool = dropdown2.SelectedValue;

            // Use your database connection string
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

            // Fetch the total number of students in the selected school
            int totalStudents = GetTotalStudentsCount(k, selectedSchool);

            // Fetch the number of rows in the corresponding studdata table
            int totalRows = GetTotalRowsCount(k, selectedSchool);



            // Calculate percentages
            double completedPercentage = (double)totalRows / totalStudents * 100;
            double remainingPercentage = 100 - completedPercentage;




            Label2.Text = "<b>" +completedPercentage.ToString() + "%" +  " of  " + "100%" + " work  is Done!  </br></br> </br>" + totalRows.ToString() + " students data is filled  out  of " + totalStudents.ToString() + " students  of school  " + selectedSchool.ToString() + "</b>";
          // Generate and register JavaScript code to draw the chart
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

            protected int GetTotalStudentsCount(string connectionString, string school)
        {
            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Construct the table name based on the selected school
                string tableName = RemoveSpecialCharacters( dropdown2.Text.Replace(" ", "").ToUpper());

                // Check if the table exists
                string checkTableQuery = $"IF OBJECT_ID('{RemoveSpecialCharacters(tableName)}', 'U') IS NULL SELECT 0 ELSE SELECT [TOTALSTUDS] FROM {RemoveSpecialCharacters( tableName)}";

                using (SqlCommand cmd = new SqlCommand(checkTableQuery, con))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        int rowCount = Convert.ToInt32(result);

                        // No need to check if rowCount is 0 here, as it might be a valid case
                        return rowCount;
                    }
                    else
                    {
                        // Handle the case when the table doesn't exist
                        // You can show an alert using JavaScript here
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "TableNotExistAlert", "alert('The table does not exist.');", true);
                        Label1.Visible = true;

                        // Return a default value or throw an exception as needed
                        return 1; // or throw an exception
                    }
                }
            }
        }


        protected int GetTotalRowsCount(string connectionString, string school)
        {



            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }



            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Construct the table name based on the selected school
                string tableName = RemoveSpecialCharacters( school.ToLower().Replace(" ", "") + "studdata");

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
                        Label1.Visible = false;

                    }
                    else
                    {
                        // Handle the case when the table doesn't exist
                        // You can show an alert using JavaScript here
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "TableNotExistAlert", "alert('The table does not exist.');", true);
                        Label1.Visible = true;

                        // Return a default value or throw an exception as needed
                        return 1; // or throw an exception
                    }
                }
            }
        }



        protected void PrintDataLink_Click(object sender, EventArgs e)
        {

            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }













            // Get the selected school from dropdown2
            string selectedSchool = RemoveSpecialCharacters( dropdown2.SelectedValue);

            // Use your database connection string
            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

            // Generate the PDF file with student data
            GeneratePdf(   selectedSchool);
        }

        protected void GeneratePdf(string selectedSchool)
        {


            if (Session["Username"] != null)
            {


            }

            else
            {
                Response.Redirect("adminlogon.aspx");

            }













            // Specify the target directory where PDF files will be saved
            string targetDirectory = Server.MapPath("~/App_Data/PDFs/");

            // Check if the target directory exists before attempting to delete
            if (Directory.Exists(targetDirectory))
            {
                string[] files = Directory.GetFiles(targetDirectory);
                string[] dirs = Directory.GetDirectories(targetDirectory);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                // Delete the directory
                Directory.Delete(targetDirectory);
            }

            // Ensure the target directory exists; create it if not
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            // Create a new PDF document
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);

            // Generate a unique filename for the PDF based on the selected school
            string pdfFileName = $"{selectedSchool}.pdf";
            string filePath = Path.Combine(targetDirectory, pdfFileName);

            try
            {
                // Create a PdfWriter instance to write the PDF content to the file
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));

                // Open the PDF document for writing
                pdfDoc.Open();

                // Add school header
                string schoolHeader = selectedSchool.Replace("studdata", "").ToUpper();
                pdfDoc.Add(new Paragraph(schoolHeader, new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD)));

                string a =  RemoveSpecialCharacters( dropdown2.Text.Replace(" ", "").ToUpper());
                // Use your database connection string
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["Username"].ToString()) + ";");

                // Check if the school table exists before executing the query
                if (DoesTableExist(a, k))
                {
                    // Add school details
                    string schoolDetailsQuery = $"SELECT [NAME], [ADD], [GRADE], [CONTACT] FROM {a}";
                    // Use your database connection string

                    using (SqlConnection con = new SqlConnection(k))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(schoolDetailsQuery, con))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                pdfDoc.Add(new Paragraph("School Details:", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                                pdfDoc.Add(new Paragraph($"Name: {reader["NAME"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"Address: {reader["ADD"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"Grade: {reader["GRADE"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"Contact: {reader["CONTACT"].ToString()}"));
                            }
                        }
                    }

                    // Add teacher details
                    string selectedStaff = dropdown1.SelectedValue;
                    string teacherDetailsQuery = $"SELECT [NAME], [SCHOOL_ADD], [STAFF_DEPT], [SAFF_DESIG], [GENDER], [MOBNO], [DOB] FROM SAFF_DATA WHERE [STAFF_ID] = @StaffID";

                    using (SqlConnection con = new SqlConnection(k))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(teacherDetailsQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@StaffID", selectedStaff);
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                pdfDoc.Add(new Paragraph("Teacher Details:", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                                pdfDoc.Add(new Paragraph($"Name: {reader["NAME"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"School Address: {reader["SCHOOL_ADD"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"Department: {reader["STAFF_DEPT"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"Designation: {reader["SAFF_DESIG"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"Gender: {reader["GENDER"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"Mobile Number: {reader["MOBNO"].ToString()}"));
                                pdfDoc.Add(new Paragraph($"Date of Birth: {reader["DOB"].ToString()}"));
                            }
                        }
                    }

                    // Add a line break
                    pdfDoc.Add(Chunk.NEWLINE);

                    // Create a PDF table with columns for student data
                    PdfPTable pdfTable = new PdfPTable(7);

                    // Add headers to the PDF
                    pdfTable.AddCell(new PdfPCell(new Phrase("Student ID")));
                    pdfTable.AddCell(new PdfPCell(new Phrase("Full Name")));
                    pdfTable.AddCell(new PdfPCell(new Phrase("Address")));
                    pdfTable.AddCell(new PdfPCell(new Phrase("Caste / Category")));
                    pdfTable.AddCell(new PdfPCell(new Phrase("Date of Birth")));
                    pdfTable.AddCell(new PdfPCell(new Phrase("Mobile Number")));
                    pdfTable.AddCell(new PdfPCell(new Phrase("WhatsApp Number")));

                    // Fetch student data from the selected table
                    string tableName = $"{selectedSchool.Replace(" ", "").ToLower()}studdata";

                    // Check if the student data table exists before executing the query
                    if (DoesTableExist(tableName, k))
                    {
                        string studentDataQuery = $"SELECT * FROM {tableName}";

                        using (SqlConnection con = new SqlConnection(k))
                        {
                            con.Open();
                            using (SqlCommand cmd = new SqlCommand(studentDataQuery, con))
                            {
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    // Add a new row for each student
                                    pdfTable.AddCell(new PdfPCell(new Phrase(reader["StudentID"].ToString())));
                                    pdfTable.AddCell(new PdfPCell(new Phrase(reader["FullName"].ToString())));
                                    pdfTable.AddCell(new PdfPCell(new Phrase(reader["Address"].ToString())));
                                    pdfTable.AddCell(new PdfPCell(new Phrase(reader["CasteCategory"].ToString())));
                                    pdfTable.AddCell(new PdfPCell(new Phrase(reader["DOB"].ToString())));
                                    pdfTable.AddCell(new PdfPCell(new Phrase(reader["MobileNumber"].ToString())));
                                    pdfTable.AddCell(new PdfPCell(new Phrase(reader["WhatsAppNumber"].ToString())));
                                }
                            }
                        }

                        // Add the table to the document
                        pdfDoc.Add(pdfTable);
                    }
                    else
                    {
                        // Handle the case where the student data table does not exist
                        pdfDoc.Add(new Paragraph("Staff Not filled any data or Not Completed any School profile ", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                    }
                }
                else
                {
                    // Handle the case where the school table does not exist
                    pdfDoc.Add(new Paragraph("Staff Not filled any data or Not Completed any School profile", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
            }
        
            // Function to check if a table exists in the database
          

            finally
            {
                // Close the PDF document
                pdfDoc.Close();

                
            }

            // Provide the generated PDF file as a download link
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", $"attachment;filename={pdfFileName}");
            Response.TransmitFile(filePath);
            Response.Flush();
            Response.End();

            

        }




        static string RemoveSpecialCharacters(string input)
        {
            // Use a regular expression to remove special characters and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "");
        }


        public bool DoesTableExist(string tableName, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";
                    command.Parameters.AddWithValue("@TableName", tableName);
                    int tableCount = (int)command.ExecuteScalar();
                    return tableCount > 0;
                }
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminportal.aspx");
        }
    }
    }
