using Core.Data.Entities;
using Api.Repositories;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Hubs
{
    public class ChatHub: Hub
    {
        public readonly List<User> Users;

        public override Task OnConnectedAsync()
        {
            // Conecta o usuário ao hub
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(Message message)
        {
            // Envia uma mensagem para todos os usuários registrados
            await Clients.All.SendAsync("ReceberMensagem", message);
        }
    }
}
