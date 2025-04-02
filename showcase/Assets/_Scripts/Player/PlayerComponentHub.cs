using UnityEngine;

namespace com.game.player
{
    public class PlayerComponentHub : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler m_inputHandler;
        [SerializeField] private PlayerCameraSelector m_cameraSelector;

        public PlayerInputHandler InputHandler => m_inputHandler;
        public PlayerCameraSelector Camera => m_cameraSelector;

        public void Bootstrap(Player sender)
        {
            PlayerComponentBase.DoInitialize(sender,
                m_inputHandler,
                m_cameraSelector);
        }
    }
}
