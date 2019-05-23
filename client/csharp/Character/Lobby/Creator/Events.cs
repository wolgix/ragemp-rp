using RAGE;
using Newtonsoft.Json;

namespace Project.Client.Character.Creator
{
    public class Events : RAGE.Events.Script
    {
        public Events()
        {
            RAGE.Events.Add(Shared.Events.UI_LOBBY_CREATOR_INIT, OnUiLobbyCreatorInit);

            RAGE.Events.Add(Shared.Events.UI_LOBBY_CREATOR_CUSTOMIZE, OnUiLobbyCreatorCustomize);
        }

        public void OnUiLobbyCreatorInit(object[] args)
        {
            Service.SendInitialDataToUi();
        }

        public void OnUiLobbyCreatorCustomize(object[] args)
        {
            var payload = JsonConvert.DeserializeObject<Schemes.CustomizePayload>((string)args[0]);

            Service.Customize(payload);
        }
    }
}
