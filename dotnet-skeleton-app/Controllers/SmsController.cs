using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nexmo.Api.Utility;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;

namespace dotnet_skeleton_app.Controllers
{    
    public class SmsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(string to, string from, string message)
        {
            var VONAGE_API_KEY = Environment.GetEnvironmentVariable("VONAGE_API_KEY") ?? "VONAGE_API_KEY";
            var VONAGE_API_SECRET = Environment.GetEnvironmentVariable("VONAGE_API_SECRET") ?? "VONAGE_API_SECRET";
            var client = new VonageClient(Credentials.FromApiKeyAndSecret(VONAGE_API_KEY, VONAGE_API_SECRET));
            var request = new SendSmsRequest
            {
                From = from,
                To = to,
                Text = message
            };
            try
            {
                var results = client.SmsClient.SendAnSms(request);
                ViewBag.result = $"Message sent successfully, ID:{results.Messages[0].MessageId}";
            }
            catch(VonageSmsResponseException ex)
            {
                ViewBag.result = $"Message failed with error: { ex.Response.Messages[0].ErrorText}";                
            }            

            return View("Index");
        }

        [HttpGet("webhooks/inbound-sms")]
        public ActionResult Receive()
        {
            var response = WebhookParser.ParseQuery<InboundSms>(Request.Query);
            if (null != response.To && null != response.Msisdn)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("INCOMING TEXT");
                Console.WriteLine("From: " + response.Msisdn);
                Console.WriteLine($"To: {response.To}");
                Console.WriteLine("Message: " + response.Text);
                Console.WriteLine($"Id: {response.MessageId}");
                Console.WriteLine($"Time Stamp: {response.Timestamp}");
                Console.WriteLine("------------------------------------");
                return NoContent();

            }
            return NoContent();
        }
    }
}