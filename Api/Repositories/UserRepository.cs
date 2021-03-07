using Api.Database;
using Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class UserRepository
    {
        public Context Context;

        public UserRepository(Context context)
        {
            Context = context;
        }


        public async Task Create(User user)
        {
                Context.Users.Add(user);
                await Context.SaveChangesAsync();
        }

        public async Task<List<User>> List()
        {
                return await Context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> Detail(int id)
        {
                return await Context.Users
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
        }
    }
}
