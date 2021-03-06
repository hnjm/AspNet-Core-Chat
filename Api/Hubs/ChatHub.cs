using Api.Models;
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
            var user = JsonConvert.DeserializeObject<User>(Context.GetHttpContext().Request.Query["user"]);
            Users.Add(user);
            //Ao usar o método All eu estou enviando a mensagem para todos os usuários conectados no meu Hub
            Clients.All.SendAsync("chat", Users, user);
            return base.OnConnectedAsync();
        }


        /// <summary>
        /// Método responsável por encaminhar as mensagens pelo hub
        /// </summary>
        /// <param name="message">Este parâmetro é nosso objeto representando a mensagem e os usuários envolvidos</param>
        /// <returns></returns>
        public async Task SendMessage(Message message)
        {
            //Ao usar o método Client(_connections.GetUserId(chat.destination)) eu estou enviando a mensagem apenas para o usuário destino, não realizando broadcast
            await Clients.Client("1").SendAsync("Receive", message.Sender, message.Text);
        }
    }
}
