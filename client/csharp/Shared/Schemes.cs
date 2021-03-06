using System.Collections.Generic;

namespace Project.Shared
{
    public class Schemes
    {
        public class UiLoginSubmitPayload
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public bool Remember { get; set; }
        }

        public class LobbySelectCharacters : CharacterCustomization
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
        }

        public class LobbySelectorPlay
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class CharacterCustomization
        {
            public int Hair { get; set; }
            public int Color { get; set; }
            public int EyeColor { get; set; }
            public IList<FaceFeature> FaceFeatures { get; set; }
            public IList<HeadOverlay> HeadOverlays { get; set; }
            public (int, int, int, int, int, int, float, float, float, bool) BlendData { get; set; }
        }

        public class UiLobbyCreatorSubmit : CharacterCustomization
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
        }

        public class FaceFeature
        {
            public int Index { get; set; }
            public float Value { get; set; }
        }

        public class HeadOverlay
        {
            public int Index { get; set; }
            public int Value { get; set; }
        }
    }
}
