using System;
using System.Web.UI;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace adment.Pages
{
    public partial class broadcastme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;

        

            // Initialize the Twilio client
            TwilioClient.Init(accountSid, authToken);
        }

        protected void btnSendWhatsApp_Click(object sender, EventArgs e)
        {
          
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
     new PhoneNumber(""));
            messageOptions.From = new PhoneNumber("");

            messageOptions.Body = "";
            var message = MessageResource.Create(messageOptions);
           

            Console.WriteLine(message.Body);

            // ddgds
            Console.WriteLine(message.Body);

        }


    }
}
