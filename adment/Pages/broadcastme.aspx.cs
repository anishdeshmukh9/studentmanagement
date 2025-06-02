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

            // Your Twilio Account SID and Auth Token
            const string accountSid = "ACd479fbdc22b4d463a780d1f42147fcbe";
            const string authToken = "24cf48cd9639473c3368537434cb7ee7";

            // Initialize the Twilio client
            TwilioClient.Init(accountSid, authToken);
        }

        protected void btnSendWhatsApp_Click(object sender, EventArgs e)
        {
            var accountSid = "ACd479fbdc22b4d463a780d1f42147fcbe";
            var authToken = "24cf48cd9639473c3368537434cb7ee7";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
     new PhoneNumber("+918806149951"));
            messageOptions.From = new PhoneNumber("+12565790358");

            messageOptions.Body = "";
            var message = MessageResource.Create(messageOptions);
           

            Console.WriteLine(message.Body);

            // ddgds
            Console.WriteLine(message.Body);

        }


    }
}
