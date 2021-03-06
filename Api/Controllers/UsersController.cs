using Api.Database;
using Api.Models;
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
    public class UsersController : ControllerBase
    {
        public UserRepository Repository { get; set; }

        public UsersController(Context context)
        {
            Repository = new UserRepository(context);
        }

        [HttpPost]
        public async Task<User> Create([FromBody] User user)
        {
            await Repository.Create(user);

            return user;
        }

        [HttpGet]
        public async Task<List<User>> List()
        {
            return await Repository.List();
        }

        [HttpGet("{id}")]
        public async Task<User> Detail([FromRoute] int id)
        {
            return await Repository.Detail(id);
        }
    }
}
