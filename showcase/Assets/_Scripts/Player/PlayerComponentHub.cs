using UnityEngine;

namespace com.game.player
{
    public class PlayerComponentHub : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler m_inputHandler;
        [SerializeField] private PlayerCameraSelector m_cameraSelector;
        [SerializeField] private PlayerMovement m_movement;

        public PlayerInputHandler InputHandler => m_inputHandler;
        public PlayerCameraSelector Camera => m_cameraSelector;
        public PlayerMovement Movement => m_movement;

        public void Bootstrap(Player sender)
        {
            PlayerComponentBase.DoInitialize(sender,
                m_inputHandler,
                m_cameraSelector,
                m_movement);
        }
    }
}
