using com.absence.utilities;
using UnityEngine;

namespace com.game.player
{
    [DefaultExecutionOrder(-10000)]
    public class Player : StaticInstance<Player>
    {
        [SerializeField] private PlayerComponentHub m_componentHub;
        public PlayerComponentHub Hub => m_componentHub;

        public int Index => Hub.InputHandler.PlayerInput.playerIndex;
        public bool IsLocal => true;

        protected override void Awake()
        {
            base.Awake();

            if (IsLocal) 
                Hub.Bootstrap(this);
        }
    }
}
