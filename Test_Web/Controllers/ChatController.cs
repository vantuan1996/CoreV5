using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Data.Repository;
using Test_Model.Models;
using Test_Web.Hubs;

namespace Test_Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMessageRepository _MessageRepository;
        public ChatController(IHubContext<ChatHub> hubContext, IMessageRepository MessageRepository)
        {
            _hubContext = hubContext;
            _MessageRepository = MessageRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
      
        public async Task<IActionResult> Send(string sender, string receiver, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var newMessage = new Message
                {
                    Sender = sender,
                    Receiver = receiver,
                    Content = message,
                    SentAt = DateTime.Now
                };

                var result1 = _MessageRepository.Add(newMessage);
            }
           
            return Ok();
        }
    }
}
