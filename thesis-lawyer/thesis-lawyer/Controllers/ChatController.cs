using IBM.Watson.Assistant.v2;

using Microsoft.AspNetCore.Mvc;
using System;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using thesis_lawyer.Data;
using thesis_lawyer.Entities;

namespace thesis_lawyer.Controllers
{
    public class ChatController : Controller
    {
        private string _apiKey = "KQpwSLvnAsVDzTnm_382btKs_B30IhafZyavsejojuCF";
        private string _draftId = "7f5820e4-4b80-41a2-ba68-db3789ebe733";
        private AssistantService _assistantService;
        private readonly ApplicationDbContext _context;
        private string _sessionId;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
            IamAuthenticator authenticator = new IamAuthenticator(apikey: _apiKey);
            _assistantService = new AssistantService("2021-06-14", authenticator);
            _assistantService.SetServiceUrl("https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/7f39927a-d244-4bcc-b08f-1f5367946dcf");

            // Create session when ChatController is instantiated
            CreateSession();
        }

        private void CreateSession()
        {
            try
            {
                var result = _assistantService.CreateSession(assistantId: _draftId);
                _sessionId = result.Result.SessionId;
                Console.WriteLine("============SESSION CREATED============");
                
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error creating session: " + ex.Message);
            }
        }

        // Action method to display the chat view
        public IActionResult chatlawyer()
        {
            return View();
        }

        // Action method to send a message to Watson Assistant
        [HttpPost]
        public async Task<IActionResult> SendMessageToWatson(string message)
        {
            var response = SendMessage(message);
            var messageDto = new History
            {
                Id = "1",
                UserId = "tes",
                Message = message,
                SentReceived = false
            };
            _context.ChatHistory.Add(messageDto);
            await _context.SaveChangesAsync();

            Console.WriteLine(message);
            return Json(new { response });
        }

        // Add other controller actions as needed...

        private string SendMessage(string message)
        {
            try
            {
                var result = _assistantService.Message(
                    assistantId: _draftId,
                    sessionId: _sessionId,
                    input: new IBM.Watson.Assistant.v2.Model.MessageInput()
                    {
                        MessageType = "text",
                        Text = message
                    }
                );
                Console.WriteLine(message);

                return result.Result.Output.Generic[0].Text;
            }
            catch (Exception ex)
            {
                // Handle exception
                return "Error: " + ex.Message;
            }
        }
    }
}
