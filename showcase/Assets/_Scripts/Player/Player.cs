using System.Collections.Generic;
using UnityEngine;

namespace com.game.player
{
    [DefaultExecutionOrder(-10000)]
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        public static Dictionary<int, Player> SplitScreenInstances { get; private set; } = new();

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
                if (!SplitScreenInstances.TryAdd(Index, this))
                    SplitScreenInstances[Index] = this;
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
                if (!SplitScreenInstances.TryAdd(Index, null))
                    SplitScreenInstances[Index] = null;
            }

            else
            {
                Instance = null;
            }
        }
    }
}
