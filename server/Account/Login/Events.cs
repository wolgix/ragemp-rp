using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace Project.Server.Account.Login
{
    class Events : Script
    {
        [RemoteEvent(Shared.Events.UI_LOGIN_SUBMIT)]
        public void OnUiLoginSubmit(Client player, string data = "json") // For data != null, because JsonConvert throw an error
        {
            Task.Run(() =>
            {
                if (Middlewares.EventsBlocker.Block(player, Shared.Events.UI_LOGIN_SUBMIT, Middlewares.EventsBlocker.Receivers.CEF, 1000) > 1)
                {
                    return;
                }
                // For data != invalid json, because JsonConvert throw an error
                try
                {
                    Shared.Schemes.UiLoginSubmitPayload payload = JsonConvert.DeserializeObject<Shared.Schemes.UiLoginSubmitPayload>(data);
                    Service.LogIn(player, payload);
                }
                catch (JsonException jex)
                {
                    Console.WriteLine(jex.Message);
                }
            });
        }
    }
}
