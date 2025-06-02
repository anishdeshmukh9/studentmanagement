using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adment.Pages
{
    public partial class resetpass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "supadment@gmail.com";
            string smtpPassword = "deufoizunlislbcq"; // Replace with your Gmail password

            // Sender's email address (your Gmail address)
            string fromEmail = "supadment@gmail.com";

            // Recipient's email address (the email address entered by the user)
            string toEmail = txtEmail.Text;

            // Email subject and body
            string subject = "Your password";



            string str = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection obj = new SqlConnection(str))
            {
                obj.Open();

                string que = "SELECT * FROM INSTITUE_LOGON WHERE [email] = @Email";

                using (SqlCommand cmd = new SqlCommand(que, obj))
                {
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        try
                        {
                            reader.Read();

                            string body = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Password Reset - ADMENT</title><style>" +
              ".email-banner { background: linear-gradient(to right, #ff6a00, #ee0979); color: white; padding: 20px; text-align: center; }" +
              ".email-content { padding: 20px; }" +
              ".section-heading { font-size: 24px; font-weight: bold; margin-bottom: 20px; }" +
              ".reset-info { font-size: 18px; margin-bottom: 20px; }" +
              ".development-card { border: 1px solid #ccc; margin: 10px; padding: 20px; }" +
              "</style></head><body>" +
              "<div class=\"email-banner\"><h1 class=\"display-4\">ADMENT</h1><p class=\"lead\">Your Ultimate Admission Management Solution</p></div>" +
              "<div class=\"email-content\"><div class=\"section-heading\">Password Reset</div>" +
              "<p class=\"reset-info\">Hello [User],</p><p class=\"reset-info\">We received a request to reset your password. Your new password is:</p>" +
              "<p class=\"reset-info\" style=\"font-weight: bold; font-size: 20px;\">" + reader["PASS"].ToString() + "</p>" +
              "<p class=\"reset-info\">For security reasons, please change your password after logging in.</p>" +
              "<div class=\"development-card\"><p class=\"project-guide\">🧑‍💻 Developed By Computer Department of @mitpoly yeola ✨</p></div></div></body></html>";

                            using (SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort))
                            {
                                smtpClient.EnableSsl = true;
                                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                                using (MailMessage mailMessage = new MailMessage(fromEmail, toEmail))
                                {
                                    {
                                        mailMessage.Subject = subject;
                                        mailMessage.Body = body;
                                        mailMessage.IsBodyHtml = true; // Change to true if sending HTML content

                                        smtpClient.Send(mailMessage);

                                        lblSuccess.Visible = true;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            // Convert the exception details to a string
                            string exceptionDetails = ex.ToString();

                            // Register a startup script to display an alert
                            string script = $"alert('An error occurred:\\n{exceptionDetails.Replace("'", "\\'")}');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);

                        }

}
                    else
                    {
                        reader.Close();
                        lblError.Visible = true;
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
}