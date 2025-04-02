using com.game.utilities.cinemachine;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.player
{
    public class PlayerCamera : MonoBehaviour
    {
        public class Context
        {
            public Player Owner;
            public Transform Follow;
            public bool SplitScreen;
        }

        [SerializeField] private CinemachineCamera m_virtualCamera;
        [SerializeField] private CinemachineInputAxisController m_inputProvider;

        public CinemachineCamera VCam => m_virtualCamera;
        public CinemachineInputAxisController InputProvider => m_inputProvider;

        InputActionReference m_lookActionReference;
        public void Initialize(Context context)
        {
            if (context == null)
            {
                m_virtualCamera.Follow = null;
                return;
            }

            m_virtualCamera.Follow = context.Follow;

            if (context.SplitScreen)
            {
                m_inputProvider.PlayerIndex = context.Owner.Index;
                m_virtualCamera.OutputChannel = 
                    CinemachineHelpers.GetOutputChannelForPlayer(context.Owner.Index, false);
            }

            else
            {
                m_inputProvider.PlayerIndex = -1;
            }

            m_lookActionReference = InputActionReference.Create(context.Owner.Hub.InputHandler.InputActions.InGame.Look);
            m_inputProvider.Controllers[0].Input.InputAction = m_lookActionReference;
            m_inputProvider.Controllers[1].Input.InputAction = m_lookActionReference;
        }
    }
}