using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;

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
            var NEXMO_API_KEY = Environment.GetEnvironmentVariable("NEXMO_API_KEY") ?? "NEXMO_API_KEY";
            var NEXMO_API_SECRET = Environment.GetEnvironmentVariable("NEXMO_API_SECRET") ?? "NEXMO_API_SECRET";

            var client = new Nexmo.Api.Client(new Nexmo.Api.Request.Credentials() 
            { 
                ApiKey = NEXMO_API_KEY,
                ApiSecret = NEXMO_API_SECRET
            });

            var results = client.SMS.Send(new SMS.SMSRequest
            {
                from = from,
                to = to,
                text = message
            });            

            if (results.messages.Count >= 1)
            {
                if (results.messages[0].status == "0")
                {
                    ViewBag.result = "Message sent successfully.";
                    Debug.WriteLine("Message sent successfully.");
                }
                else
                {
                    ViewBag.result = $"Message failed with error: { results.messages[0].error_text}";
                    Debug.WriteLine($"Message failed with error: {results.messages[0].error_text}");
                }
            }

            return View("Index");
        }

        [HttpGet("webhooks/inbound-sms")]
        public ActionResult Receive([FromQuery]SMS.SMSInbound response)
        {

            if (null != response.to && null != response.msisdn)
            {
                Debug.WriteLine("------------------------------------");
                Debug.WriteLine("INCOMING TEXT");
                Debug.WriteLine("From: " + response.msisdn);
                Debug.WriteLine($"To: {response.to}");
                Debug.WriteLine("Message: " + response.text);                
                Debug.WriteLine($"Id: {response.messageId}");
                Debug.WriteLine($"Time Stamp: {response.timestamp}");                
                Debug.WriteLine("------------------------------------");
                return NoContent();

            }
            else
            {
                Debug.WriteLine("------------------------------------");
                Debug.WriteLine("Endpoint was hit.");
                Debug.WriteLine("------------------------------------");
                return NoContent();

            }
        }
    }
}