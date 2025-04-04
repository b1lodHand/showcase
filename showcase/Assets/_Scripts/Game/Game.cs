using UnityEngine;

namespace com.game
{
    public static class Game
    {
        public static bool Initialized { get; set; }
        public static bool Paused { get; set; }
        public static GameLobbyType LobbyType { get; set; }

        public static bool Pause()
        {
            if (Paused)
                return false;

            Paused = true;

            return true;
        }

        public static bool Resume()
        {
            if (!Paused)
                return false;

            Paused = false;

            return true;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Bootstrap()
        {
            Initialized = false;
            LobbyType = GameLobbyType.Singleplayer;
        }
    }
}
