using GTANetworkAPI;
using System.Linq;
using System;

namespace Project.Server.Character.Lobby.Creator
{
    public static class Service
    {
        // TODO пройти валидацию
        // TODO вернуть ответ
        // TODO если у игрока 1 персонаж, то пропустить выбор персонажа в лобби
        public static void CreateCharacter(Client player, Shared.Schemes.UiLobbyCreatorSubmit payload)
        {
            using (var db = new Database())
            {
                Account.Attachment accountAttachment = player.GetData(Account.Resources.ATTACHMENT_KEY);

                string fullName = $"{payload.FirstName} {payload.LastName}";

                var character = db.Characters.SingleOrDefault(
                    v => $"{v.FirstName} {v.LastName}".ToLower() == fullName.ToLower()
                );

                if (character is Character.Entity)
                {
                    // TODO notify
                    Console.WriteLine($"{fullName} already existing");
                    return;
                }

                character = new Character.Entity(payload.FirstName, payload.LastName, payload.Sex, payload);
                character.Account = accountAttachment.Entity;

                accountAttachment.Entity.Characters.Add(character);

                db.Accounts.Update(accountAttachment.Entity);
                db.SaveChanges();

                Bus.TriggerUi(player, Shared.Events.LOBBY_CREATOR_SHOW, false);

                if (Character.Service.GetCharactersCount(accountAttachment.Entity) > 1)
                {
                    Lobby.Service.Start(player);
                }
                else
                {
                    Lobby.Selector.Service.Play(player, character);
                }
            }
        }
    }
}
