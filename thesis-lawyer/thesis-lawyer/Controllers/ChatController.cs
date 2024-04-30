using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using Microsoft.EntityFrameworkCore;
using thesis_lawyer.Data;
using thesis_lawyer.Entities;
using thesis_lawyer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using thesis_lawyer.Extensions;
using IBM.Watson.Assistant.v2;
using Microsoft.AspNetCore.Http;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace thesis_lawyer.Controllers
{
    public class ChatController : Controller
    {
        private readonly string _apiKey = "KQpwSLvnAsVDzTnm_382btKs_B30IhafZyavsejojuCF";
        private readonly string _draftId = "7f5820e4-4b80-41a2-ba68-db3789ebe733";
        private AssistantService _assistantService;
        private static string _sessionId;
        private static string oldSession;
        private Chat _currentChat;
        private readonly UserManager<UserModel> _userManager;
        private readonly ApplicationDbContext _context;

        public ChatController(UserManager<UserModel> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _assistantService = CreateAssistantService();
        }

        public IActionResult chatlawyer(int chatId)
        {
            if (chatId != 0)
            {
                oldSession = _context.Chat.FirstOrDefault(x => x.Id == chatId).SessionId;

            }

            _sessionId = null;
                var chatList = _context.HistoryChats.Where(c => chatId == c.ChatId).Select(x => new
                {
                    Message = x.Message,
                    Category = x.MessageCategory
                });
                Console.WriteLine("lllllllllllllll");
                Console.WriteLine(_sessionId);
                return View(chatList);
            

        }

        private void InitializeDependenciesForOldChat()
        {
            _currentChat = _context.Chat.FirstOrDefault(x => x.SessionId == oldSession);
            _assistantService = CreateAssistantService();
            _sessionId = CreateSession(_assistantService);
            var user = _userManager.GetUserAsync(User).Result;
          //  _currentChat = CreateNewChatForUser(user, _context);
         
            // Store dependencies in session
           // HttpContext.Session.SetObject("AssistantService", _assistantService);
           
            Console.WriteLine(_sessionId);
            HttpContext.Session.SetObject("CurrentChat", _currentChat);
        }

        private void InitializeDependencies()
        {
            
            _assistantService = CreateAssistantService();
            _sessionId = CreateSession(_assistantService);
            var user = _userManager.GetUserAsync(User).Result;
              _currentChat = CreateNewChatForUser(user, _context);
         
            // Store dependencies in session
            // HttpContext.Session.SetObject("AssistantService", _assistantService);
            HttpContext.Session.SetString("SessionId", _sessionId);
            Console.WriteLine(_sessionId);
            HttpContext.Session.SetObject("CurrentChat", _currentChat);
        }

        private AssistantService CreateAssistantService()
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: _apiKey);
            var assistantService = new AssistantService("2021-06-14", authenticator);
            assistantService.SetServiceUrl("https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/7f39927a-d244-4bcc-b08f-1f5367946dcf");
            return assistantService;
        }

        private string CreateSession(AssistantService assistantService)
        {
            var result = assistantService.CreateSession(assistantId: _draftId);
            return result.Result.SessionId;
        }

        private Chat CreateNewChatForUser(UserModel user, ApplicationDbContext context)
        {
            var newChat = new Chat
            {
                User = user,
                SessionId = _sessionId,
                Messages = new List<HistoryChat>()
            };

            context.Chat.Add(newChat);
            context.SaveChanges(); // Save changes immediately to get the chat Id

            return newChat;
        }

        // Action method for sending messages to Watson
        [HttpPost]
        public IActionResult SendMessageToWatson(string message)
        {
            if (!oldSession.IsNullOrEmpty())
            {
                InitializeDependenciesForOldChat();
                oldSession = null;
            }
            else if(oldSession.IsNullOrEmpty()  && _sessionId.IsNullOrEmpty())
            {
                InitializeDependencies();
            }
       
               
      
            // Use _currentChat in your method
            var response = SendMessage(message);

            var user = _userManager.GetUserAsync(User).Result;

            // Retrieve the current chat from the session
            _currentChat = HttpContext.Session.GetObject<Chat>("CurrentChat");

            // Create a new HistoryChat entity and set the ChatId automatically
            var messageDto = new HistoryChat
            {
                ChatId = _currentChat.Id, // Use _currentChat's Id
                Message = message,
                UserForeignKey = user.Id,
                MessageCategory = "sent", // Assuming you have a method to determine message category
            };
            var responseDto = new HistoryChat
            {
                ChatId = _currentChat.Id, // Use _currentChat's Id
                Message = response,
                UserForeignKey = user.Id,
                MessageCategory = "received", // Assuming you have a method to determine message category
            };
Console.WriteLine(_sessionId);
            // Add the message to the database
            _context.HistoryChats.Add(messageDto);
            _context.HistoryChats.Add(responseDto);
            _context.SaveChanges(); // Synchronously save changes

            // Return the response
            return Json(new { response });
        }

        private string SendMessage(string message)
        { try
            
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