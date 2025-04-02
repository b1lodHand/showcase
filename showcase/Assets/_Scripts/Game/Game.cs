using UnityEngine;

namespace com.game
{
    public static class Game
    {
        public static bool Initialized { get; set; }
        public static GameLobbyType LobbyType { get; set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Bootstrap()
        {
            Initialized = false;
            LobbyType = GameLobbyType.SplitScreen;
        }
    }
}
