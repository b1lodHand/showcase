using UnityEngine;

namespace com.game.player
{
    public abstract class PlayerComponentBase : MonoBehaviour
    {
        protected Player m_owner;
        public Player Player => m_owner;

        public bool IsLocal => m_owner.IsLocal;

        public static void DoInitialize(Player owner, params PlayerComponentBase[] targets)
        {
            foreach (PlayerComponentBase target in targets)
            {
                target.m_owner = owner;
                target.OnInitialize();
            }
        }

        protected virtual void OnInitialize() { }
    }
}
