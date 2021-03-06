using RAGE;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Project.Client
{
    public class Bus : Events.Script
    {
        static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public Bus()
        {
            Events.Add("ui=>server", OnUiToServer);
            Events.Add("server=>ui", OnServerToUi);
        }

        public static void TriggerUi(string ev, object payload = null)
        {
            Browser.Service.Browser.ExecuteJs($"bus.emit(\"{ev}\", {JsonConvert.SerializeObject(payload, Settings)})");
        }

        public static void TriggerServer(string ev, object payload = null)
        {
            RAGE.Events.CallRemote(ev, JsonConvert.SerializeObject(payload));
        }

        static void OnUiToServer(object[] args)
        {
            var payload = JsonConvert.DeserializeObject<Payload>((string)args[0]);
            TriggerServer(payload.ev, payload.payload);
        }

        static void OnServerToUi(object[] args)
        {
            var payload = JsonConvert.DeserializeObject<Payload>((string)args[0]);
            TriggerUi(payload.ev, payload.payload);
        }

        class Payload
        {
            public string ev { get; set; }
            public object payload { get; set; }
        }
    }
}
