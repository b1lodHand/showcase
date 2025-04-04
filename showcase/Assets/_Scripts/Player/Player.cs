using System.Collections.Generic;
using UnityEngine;

namespace com.game.player
{
    [DefaultExecutionOrder(-101)]
    public class Player : MonoBehaviour
    {
        static Player s_instance;
        public static Player Instance
        {
            get
            {
                if (Game.LobbyType == GameLobbyType.SplitScreen)
                    Debug.LogWarning("Using 'Player.Instance in a split-screen game will return the last player joined. So is not recommended.");

                return s_instance;

            }

            private set
            {
                s_instance = value;
            }
        }

        static Dictionary<int, Player> s_splitScreenInstances = new();
        public static Dictionary<int, Player> SplitScreenInstances
        {
            get
            {
                if (Game.LobbyType != GameLobbyType.SplitScreen)
                    throw new System.Exception("You shouldn't use 'Player.SplitScreenInstances' for non-splitscreen games. Use 'Player.Instance' instead.");

                return s_splitScreenInstances;
            }

            private set
            {
                s_splitScreenInstances = value;
            }
        }

        [SerializeField] private PlayerComponentHub m_componentHub;
        public PlayerComponentHub Hub => m_componentHub;

        public int Index => Hub.InputHandler.PlayerInput.playerIndex;
        public bool IsLocal => true;

        private void Awake()
        {
            if (IsLocal) 
                Hub.Bootstrap(this);
        }

        private void OnEnable()
        {
            if (!Application.isPlaying)
                return;

            if (Game.LobbyType == GameLobbyType.SplitScreen)
            {
                if (!s_splitScreenInstances.TryAdd(Index, this))
                    s_splitScreenInstances[Index] = this;
            }

            else
            {
                Instance = this;
            }
        }

        private void OnDisable()
        {
            if (!Application.isPlaying)
                return;

            if (Game.LobbyType == GameLobbyType.SplitScreen)
            {
                if (s_splitScreenInstances.ContainsKey(Index))
                    s_splitScreenInstances.Remove(Index);
            }

            else
            {
                Instance = null;
            }
        }
    }
}
