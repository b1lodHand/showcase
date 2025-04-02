using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.player
{
    public class PlayerCamera : MonoBehaviour
    {
        public class Context
        {
            public Transform Follow;
        }

        [SerializeField] private CinemachineInputProvider m_inputProvider;
        [SerializeField] private CinemachineBrain m_brain;
        [SerializeField] private CinemachineVirtualCamera m_virtualCamera;
        [SerializeField] private Camera m_camera;

        public Camera Camera => m_camera;
        public CinemachineVirtualCamera VCam => m_virtualCamera;
        public CinemachineBrain Brain => m_brain;
        public CinemachineInputProvider InputProvider => m_inputProvider;

        InputActionReference m_lookActionReference;

        private void Awake()
        {
            m_lookActionReference = InputActionReference.Create(Player.Instance.Hub.InputHandler.InputActions.InGame.Look);
            m_inputProvider.XYAxis = m_lookActionReference;
        }

        public void Initialize(Context context)
        {
            if (context == null)
            {
                m_virtualCamera.Follow = null;
                return;
            }

            m_virtualCamera.Follow = context.Follow;
        }
    }
}