using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text.RegularExpressions;

namespace adment.Pages
{
    public partial class feedData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            LoadUploadedFiles();


            if (!IsPostBack)
            {
                if (Session["staffUSERNAME"] != null)
                {

                    Label1.Text = Session["school_data_feed"].ToString().ToUpper();



                }

                else
                {
                    Response.Redirect("teacherlogin.aspx");
                }

            }

            BindGridView();
        }

        protected void feedDatabtn(object sender, EventArgs e)
        {

            if (Session["staffUSERNAME"] != null)
            {

               



            }

            else
            {
                Response.Redirect("teacherlogin.aspx");
            }

            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + ";");

            string tbl_name = RemoveSpecialCharacters(Session["school_data_feed"].ToString());
            tbl_name = tbl_name.Replace(" ", "");
            tbl_name = tbl_name + "studdata";
            // Get values from TextBoxes
            string studentID = txtStudentID.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string casteCategory = txtCasteCategory.Text.Trim();
            string dob = txtDOB.Text.Trim();
            string mobileNumber = txtMobileNumber.Text.Trim();
            string whatsappNumber = txtWhatsAppNumber.Text.Trim();

            // Validate input data here if needed

            // Create a SQL connection and command
            using (SqlConnection con1 = new SqlConnection(k))
            {
                con1.Open();

                // Check if the table already exists, create it if not
                string createTableQuery = $"IF OBJECT_ID('{RemoveSpecialCharacters(tbl_name)}', 'U') IS NULL " +
                                          "BEGIN " +
                                          $"CREATE TABLE {RemoveSpecialCharacters(tbl_name)} (" +
                                          "    StudentID NVARCHAR(50) PRIMARY KEY, " +
                                          "    FullName NVARCHAR(255), " +
                                          "    Address NVARCHAR(255), " +
                                          "    CasteCategory NVARCHAR(255), " +
                                          "    DOB DATE, " +
                                          "    MobileNumber NVARCHAR(20), " +
                                          "    WhatsAppNumber NVARCHAR(20)" +
                                          ") " +
                                          "END";

                using (SqlCommand cmd = new SqlCommand(createTableQuery, con1))
                {
                    cmd.ExecuteNonQuery();
                }

                // Check if the Student ID exists in the table
                string checkStudentQuery = $"SELECT COUNT(*) FROM {RemoveSpecialCharacters(tbl_name)} WHERE StudentID = @StudentID";
                using (SqlCommand cmd = new SqlCommand(checkStudentQuery, con1))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    int existingRecords = (int)cmd.ExecuteScalar();

                    if (existingRecords > 0)
                    {
                        // Student ID already exists, update the record
                        string updateQuery = $"UPDATE {RemoveSpecialCharacters(tbl_name)} " +
                                             "SET FullName = @FullName, " +
                                             "    Address = @Address, " +
                                             "    CasteCategory = @CasteCategory, " +
                                             "    DOB = @DOB, " +
                                             "    MobileNumber = @MobileNumber, " +
                                             "    WhatsAppNumber = @WhatsAppNumber " +
                                             "WHERE StudentID = @StudentID";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, con1))
                        {
                            updateCmd.Parameters.AddWithValue("@StudentID", studentID);
                            updateCmd.Parameters.AddWithValue("@FullName", fullName);
                            updateCmd.Parameters.AddWithValue("@Address", address);
                            updateCmd.Parameters.AddWithValue("@CasteCategory", casteCategory);
                            updateCmd.Parameters.AddWithValue("@DOB", dob);
                            updateCmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                            updateCmd.Parameters.AddWithValue("@WhatsAppNumber", whatsappNumber);

                            int updatedRows = updateCmd.ExecuteNonQuery();
                            if (updatedRows > 0)
                            {
                                // Record updated successfully
                            }
                            else
                            {
                                // Update failed
                            }
                        }
                    }
                    else
                    {
                        // Student ID does not exist, insert a new record
                        string insertQuery = $"INSERT INTO {RemoveSpecialCharacters(tbl_name)} (StudentID, FullName, Address, CasteCategory, DOB, MobileNumber, WhatsAppNumber) " +
                                             "VALUES (@StudentID, @FullName, @Address, @CasteCategory, @DOB, @MobileNumber, @WhatsAppNumber)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, con1))
                        {
                            insertCmd.Parameters.AddWithValue("@StudentID", studentID);
                            insertCmd.Parameters.AddWithValue("@FullName", fullName);
                            insertCmd.Parameters.AddWithValue("@Address", address);
                            insertCmd.Parameters.AddWithValue("@CasteCategory", casteCategory);
                            insertCmd.Parameters.AddWithValue("@DOB", dob);
                            insertCmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                            insertCmd.Parameters.AddWithValue("@WhatsAppNumber", whatsappNumber);

                            int insertedRows = insertCmd.ExecuteNonQuery();
                            if (insertedRows > 0)
                            {
                                // Record inserted successfully
                            }
                            else
                            {
                                // Insert failed
                            }
                        }
                    }
                }
            }

            // Clear TextBoxes or perform any other necessary actions
            ClearInputFields();
            BindGridView();
        }

        // Helper method to clear TextBoxes
        private void ClearInputFields()
        {

            if (Session["staffUSERNAME"] != null)
            {




            }

            else
            {
                Response.Redirect("teacherlogin.aspx");
            }
            txtStudentID.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCasteCategory.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtMobileNumber.Text = string.Empty;
            txtWhatsAppNumber.Text = string.Empty;
        }


        private void BindGridView()
        {

            if (Session["staffUSERNAME"] != null)
            {

                



            }

            else
            {
                Response.Redirect("teacherlogin.aspx");
            }
            if (Session["masterUSERNAME"] == null)
            {

                Response.Redirect("teacherlogin.aspx");
            }

            string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string k = conString.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + ";");

            string tbl_name = RemoveSpecialCharacters(Session["school_data_feed"].ToString());
            tbl_name = tbl_name.Replace(" ", "");
            tbl_name = tbl_name + "studdata";


            using (SqlConnection con1 = new SqlConnection(k))
            {
                con1.Open();

                // Check if the table exists
                string checkTableQuery = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";
                using (SqlCommand checkTableCmd = new SqlCommand(checkTableQuery, con1))
                {
                    checkTableCmd.Parameters.AddWithValue("@TableName", tbl_name);
                    int tableCount = (int)checkTableCmd.ExecuteScalar();

                    // If the table exists, retrieve its data
                    if (tableCount > 0)
                    {
                        // Replace this query with your actual data retrieval query
                        string query = $"SELECT * FROM {RemoveSpecialCharacters(tbl_name)}";

                        using (SqlCommand cmd = new SqlCommand(query, con1))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Bind the data to the GridView
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                    else
                    {
                        // Handle the case where the table does not exist
                        // You can display an error message or take appropriate action
                        // For example, you can set GridView1.DataSource = null; to clear the GridView
                    }
                }
            }

        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (Session["staffUSERNAME"] != null)
            {

      


            }

            else
            {
                Response.Redirect("teacherlogin.aspx");
            }
            // Set the page size based on the selected value in the DropDownList
            int pageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            GridView1.PageSize = pageSize;

            // Rebind the GridView to reflect the new page size
            BindGridView();
        }


        protected void ExportToPdf(object sender, EventArgs e)
        {


            if (Session["staffUSERNAME"] != null)
            {

              



            }

            else
            {
                Response.Redirect("teacherlogin.aspx");
            }
            // Create a MemoryStream to hold the PDF content
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Define the PDF document and set properties
                Document pdfDoc = new Document(PageSize.A4, 4f, 4f, 4f, 4f);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream);

                // Open the document
                pdfDoc.Open();


                // Create a PdfPTable for the header
                PdfPTable headerTable = new PdfPTable(2); // 2 columns for college logo and college name

                // Set the width ratio for the columns
                float[] headerTableWidths = new float[] { 2f, 8f }; // Adjust the values as needed
                headerTable.SetWidths(headerTableWidths);

                // Add the college logo (assuming you have an image file for the logo)
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("/imgdata/header/clglogo.png")); // Adjust the path to your actual logo image
                logo.ScaleAbsolute(50f, 50f); // Adjust the width and height of the logo as needed
                PdfPCell logoCell = new PdfPCell(logo);
                logoCell.Rowspan = 2;
                logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                headerTable.AddCell(logoCell);

                // Add the college name to the header
                PdfPCell collegeNameCell = new PdfPCell(new Phrase("Matoshri Institute Of Technology Dhanore , Yeola", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK))); // Adjust the font size and color
                collegeNameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                collegeNameCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                headerTable.AddCell(collegeNameCell);

                // Add the header table to the document
                pdfDoc.Add(headerTable);

                // Add a blank line after the header for separation
                pdfDoc.Add(new Paragraph(" "));


                // Add information from the Session
                // Add information from the Session
                Paragraph sessionInfo = new Paragraph();

                // Set Font for the session information
                Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.RED); // You can customize the font size and color
                Font boldFont1 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY); // You can customize the font size and color
                string conString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                string k = conString.Replace("base;", RemoveSpecialCharacters(Session["masterUSERNAME"].ToString()) + ";");

                string tbl_name = RemoveSpecialCharacters(Session["school_data_feed"].ToString());
                tbl_name = tbl_name.Replace(" ", "");
                tbl_name.ToUpper();
                string aquery = "select *  From " + tbl_name;
                SqlConnection con = new SqlConnection(k);

                SqlCommand cmd = new SqlCommand(aquery ,con );


                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Session["type"] = dr["GRADE"].ToString();
                }




                // Add the formatted session information to the paragraph
                sessionInfo.Add(new Phrase($"Master Username: {Session["masterUSERNAME"]}\n", boldFont));
                sessionInfo.Add(new Phrase($"Staff Username: {Session["staffUSERNAME"]}\n", boldFont));
                sessionInfo.Add(new Phrase($"Staff Name: {Session["staffNAME"]}\n", boldFont1));
                sessionInfo.Add(new Phrase($"Staff ID: {Session["staffID"]}\n", boldFont1));
                sessionInfo.Add(new Phrase($"Staff Department: {Session["staffDEPT"]}\n", boldFont));
                sessionInfo.Add(new Phrase($"Staff Designation: {Session["staffDESIG"]}\n", boldFont));
                sessionInfo.Add(new Phrase($"Staff Gender: {Session["staffGENDER"]}\n", boldFont));
                sessionInfo.Add(new Phrase($"Staff Mobile Number: {Session["staffMOBNO"]}\n", boldFont));
                sessionInfo.Add(new Phrase($"Staff Date of Birth: {Session["staffDOB"]}\n", boldFont));
                sessionInfo.Add(new Phrase($"Allocated School: {Label1.Text}\n\n", boldFont1));
                sessionInfo.Add(new Phrase($"Student  Type : {Session["type"]}\n\n", boldFont1));

                // Add the session information to the document
                pdfDoc.Add(sessionInfo);
             

                // Create the PDF table
                PdfPTable pdfTable = new PdfPTable(GridView1.HeaderRow.Cells.Count);
                float[] columnWidths = new float[] { 4f, 8f, 8f, 4f, 5f, 8f, 8f }; // Adjust the values as needed
            pdfTable = new PdfPTable(columnWidths);

                // Add GridView header to the PDF
                foreach (TableCell headerCell in GridView1.HeaderRow.Cells)
                {
                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text));
                    pdfCell.BackgroundColor = new BaseColor(GridView1.HeaderStyle.BackColor);
                    pdfTable.AddCell(pdfCell);
                }

                // Add GridView data to the PDF
                foreach (GridViewRow gridViewRow in GridView1.Rows)
                {
                    foreach (TableCell tableCell in gridViewRow.Cells)
                    {
                        PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                        pdfTable.AddCell(pdfCell);

                    }
                }

                // Add the PDF table to the document
                pdfDoc.Add(pdfTable);

                // Close the document
                pdfDoc.Close();

                // Write the PDF content to the response stream
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment;filename={Label1.Text}.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }


        static string RemoveSpecialCharacters(string input)
        {
            // Use a regular expression to remove special characters and spaces
            return Regex.Replace(input, "[^a-zA-Z0-9]+", "");
        }


        string SENDNOTE()
        {
            return "MESSSAGE SENDED";
        }



        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                string username = Session["staffUSERNAME"].ToString();
                string fileName = Path.GetFileName(FileUploadControl.FileName);
                string uploadFolderPath = Server.MapPath("~/Uploads/" + username + "/");
                string filePath = Path.Combine(uploadFolderPath, fileName);

                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                FileUploadControl.SaveAs(filePath);
                LoadUploadedFiles();
            }
        }

        protected void LoadUploadedFiles()
        {
            string username = Session["staffUSERNAME"].ToString();
            string uploadFolderPath = Server.MapPath("~/Uploads/" + username + "/");

            if (Directory.Exists(uploadFolderPath))
            {
                string[] files = Directory.GetFiles(uploadFolderPath);
                FilesListView.DataSource = files;
                FilesListView.DataBind();
            }
        }

        protected void DeleteFile(object sender, CommandEventArgs e)
        {
            string username = Session["staffUSERNAME"].ToString();
            string uploadFolderPath = Server.MapPath("~/Uploads/" + username + "/");
            string filePath = Path.Combine(uploadFolderPath, e.CommandArgument.ToString());

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                LoadUploadedFiles();
            }
        }

        protected void DownloadFile(object sender, CommandEventArgs e)
        {
            string username = Session["staffUSERNAME"].ToString();
            string uploadFolderPath = Server.MapPath("~/Uploads/" + username + "/");
            string filePath = Path.Combine(uploadFolderPath, e.CommandArgument.ToString());

            if (File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + e.CommandArgument.ToString());
                Response.TransmitFile(filePath);
                Response.End();
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("teacherpage.aspx");
        }
    }
}