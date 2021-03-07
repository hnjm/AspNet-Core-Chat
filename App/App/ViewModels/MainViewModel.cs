using Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;
using Newtonsoft.Json;

namespace App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Uri baseAddress = new Uri("http://10.0.2.2:5000/api/");
        public ObservableCollection<Message> Messages { get; set; }
        public HubConnection HubConnection { get; set; }
        public User User { get; set; }

        public ICommand SendCommand { get; set; }
        public ICommand ConnectCommand { get; set; }

        public Message Message { get; set; }
        public MainViewModel()
        {
            SendCommand = new Command(async () => await Send());
            ConnectCommand = new Command(async () => await Connect());

            Messages = new ObservableCollection<Message>();
            Message = new Message();
            OnPropertyChanged(nameof(Message));

            ConfigureHub();
        }

        private void ConfigureHub()
        {
            HubConnection = new HubConnectionBuilder().WithUrl($"http://10.0.2.2:5000/chat").Build();

            // Registra o recebedor da mensagem
            HubConnection.On<Message>("ReceberMensagem", (message) =>
            {
                Messages.Add(message);
            });
        }

        private async Task Connect()
        {
            //Conecta o usuário ao hub
            await HubConnection.StartAsync();
        }

        private async Task Send()
        {
            try
            {
                Message.SenderId = 1;

                if (string.IsNullOrWhiteSpace(Message.Text))
                    Message.Text = "Laaa";

                // Envia a mensagem
                await HubConnection.InvokeAsync("SendMessage", Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
