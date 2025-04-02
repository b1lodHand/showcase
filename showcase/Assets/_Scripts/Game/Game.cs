using UnityEngine;

namespace com.game
{
    public static class Game
    {
        public static bool Initialized { get; set; }
        public static bool Paused { get; set; }
        public static GameLobbyType LobbyType { get; set; }

        public static void Pause()
        {
            if (Paused)
                return;

            Paused = true;
        }

        public static void Resume()
        {
            if (!Paused)
                return;

            Paused = false;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Bootstrap()
        {
            Initialized = false;
            LobbyType = GameLobbyType.SplitScreen;
        }
    }
}
