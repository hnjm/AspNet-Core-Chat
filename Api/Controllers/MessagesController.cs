using Api.Database;
using Core.Data.Entities;
using Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public MessageRepository Repository { get; set; }

        public MessagesController(Context context)
        {
            Repository = new MessageRepository(context);
        }

        [HttpPost]
        public async Task<Message> Create([FromBody] Message message)
        {
            await Repository.Create(message);
            return message;
        }

        [HttpGet]
        public async Task<List<Message>> List()
        {
            return await Repository.List();
        }
    }
}
