using Api.Database;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class MessageRepository
    {
        public Context Context;

        public MessageRepository(Context context)
        {
            Context = context;
        }

        public async Task Create(Message message)
        {
            Context.Messages.Add(message);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Message>> List()
        {
            return await Context.Messages.AsNoTracking().ToListAsync();
        }



    }
}
