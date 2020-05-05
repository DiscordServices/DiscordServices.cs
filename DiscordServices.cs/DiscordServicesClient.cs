using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DiscordServices
{
    public class DServicesClient
    {
        public DServicesClient(BaseSocketClient client, string token)
        {
           
            Discord = client;
            this.token = token;

            Timer = new Timer
            {
                Interval = 600000,
                Enabled = false
            };
            Timer.Elapsed += Timer_Elapsed;
        }
        private string token;

        public readonly BaseSocketClient Discord;

        private readonly Timer Timer;

        private HttpClient Http;

        public void Start()
        {
            if (!Timer.Enabled)
            {
                Timer.Start();
                Console.WriteLine("[DServices] Enabled auto posting server count");
            }
        }

        /// <summary> Stop auto posting server count </summary>
        public void Stop()
        {
            if (Timer.Enabled)
            {
                Timer.Stop();
                Console.WriteLine("[DServices] Disabled auto posting server count");
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Discord != null)
                _ = PostServerCount();
        }

        private void SetHttp()
        {
            Http = new HttpClient();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Http.DefaultRequestHeaders.Add("User-Agent", $"DiscordServices.cs - " + Discord.CurrentUser.ToString());
            Http.DefaultRequestHeaders.Add("Authorization", token);
        }

        public async Task PostServerCount()
        {
            bool isError = false;
            if (Http == null)
                SetHttp();

            if (Discord == null)
            {
                Console.WriteLine("[DServices] Cannot post server count, Discord client is null");
                isError = true;
            }
            else if (Discord.CurrentUser == null)
            {
                Console.WriteLine("[DServices] Cannot post server count, CurrentUser is null");
                isError = true;
            }
            if (!isError)
            {
                ServerCount SC = new ServerCount { servers = Discord.Guilds.Count };
                if (Discord is DiscordShardedClient sharded)
                    SC.shards = sharded.Shards.Count;

                string JsonString = JsonConvert.SerializeObject(SC);
                try
                {
                    StringContent Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                    Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = await Http.PostAsync("https://api.discordservices.net/bot/" + Discord.CurrentUser.Id + "/stats", Content);
                    if (Res.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[DServices] Successfully posted server count");
                        
                    }
                    else
                    {
                        Console.WriteLine($"[DServices] Error could not post server count, {(int)Res.StatusCode} {Res.ReasonPhrase}");
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DServices] Error could not post server count, {ex.Message}");
                }
            }
        }

       
        public async Task PostNews(News news)
        {
            if (news == null)
            {
                Console.WriteLine("[DServices] News cannot be null");
                return;
            }
            bool isError = false;
            if (Http == null)
                SetHttp();
            if (Discord == null)
            {
                Console.WriteLine("[DServices] Cannot post news, Discord client is null");
                isError = true;
            }
            else if (Discord.CurrentUser == null)
            {
                Console.WriteLine("[DServices] Cannot post news, CurrentUser is null");
                isError = true;
            }
            if (!isError)
            {
                ServerCount SC = new ServerCount { servers = Discord.Guilds.Count };
                if (Discord is DiscordShardedClient sharded)
                    SC.shards = sharded.Shards.Count;

                string JsonString = JsonConvert.SerializeObject(news);
                try
                {
                    StringContent Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                    Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = await Http.PostAsync("https://api.discordservices.net/bot/" + Discord.CurrentUser.Id + "/news", Content);
                    if (Res.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[DServices] Successfully posted news");

                    }
                    else
                    {
                        Console.WriteLine($"[DServices] Error could not post news, {(int)Res.StatusCode} {Res.ReasonPhrase}");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DServices] Error could not post news, {ex.Message}");
                }
            }
        }

        public async Task PostCommands(List<Command> commands)
        {
            if (commands == null)
            {
                Console.WriteLine("[DServices] Commands cannot be null");
                return;
            }
            bool isError = false;
            if (Http == null)
                SetHttp();
            if (Discord == null)
            {
                Console.WriteLine("[DServices] Cannot post commands, Discord client is null");
                isError = true;
            }
            else if (Discord.CurrentUser == null)
            {
                Console.WriteLine("[DServices] Cannot post commands, CurrentUser is null");
                isError = true;
            }
            if (!isError)
            {
                ServerCount SC = new ServerCount { servers = Discord.Guilds.Count };
                if (Discord is DiscordShardedClient sharded)
                    SC.shards = sharded.Shards.Count;

                string JsonString = JsonConvert.SerializeObject(commands);
                try
                {
                    StringContent Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                    Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage Res = await Http.PostAsync("https://api.discordservices.net/bot/" + Discord.CurrentUser.Id + "/commands", Content);
                    if (Res.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[DServices] Successfully posted commands");

                    }
                    else
                    {
                        Console.WriteLine($"[DServices] Error could not post commands, {(int)Res.StatusCode} {Res.ReasonPhrase}");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DServices] Error could not post commands, {ex.Message}");
                }
            }
        }
    }
}
