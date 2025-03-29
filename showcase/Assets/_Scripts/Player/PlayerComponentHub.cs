using UnityEngine;

namespace com.game.player
{
    public class PlayerComponentHub : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler m_handler; 

        public void Bootstrap(Player sender)
        {
            PlayerComponentBase.DoInitialize(sender,
                m_handler);
        }
    }
}
