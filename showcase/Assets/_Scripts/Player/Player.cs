using com.absence.utilities;
using UnityEngine;

namespace com.game.player
{
    [DefaultExecutionOrder(-1000)]
    public class Player : Singleton<Player>
    {
        [SerializeField] private PlayerComponentHub m_componentHub;

        public bool IsLocal => true;
        public PlayerComponentHub Hub => m_componentHub;

        protected override void Awake()
        {
            base.Awake();
            if (IsLocal) Hub.Bootstrap(this);
        }
    }
}
