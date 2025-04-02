using com.game.utilities.cinemachine;
using com.game.utilities.input;
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
        [SerializeField] private InputActionReference m_lookActionReference;

        public CinemachineCamera VCam => m_virtualCamera;
        public CinemachineInputAxisController InputProvider => m_inputProvider;

        public void Initialize(Context context)
        {
            if (context == null)
            {
                m_virtualCamera.Follow = null;
                return;
            }

            m_virtualCamera.Follow = context.Follow;
            m_virtualCamera.OutputChannel =
                CinemachineHelpers.GetOutputChannelForPlayer(context.Owner.Index, false);

            if (context.SplitScreen)
            {
                m_inputProvider.PlayerIndex = context.Owner.Index;
            }

            else
            {
                m_inputProvider.PlayerIndex = -1;
            }

            InputActionReference reference = InputActionReference.Create(InputHelpers.GetAction(context.Owner.Hub.InputHandler, m_lookActionReference));
            m_inputProvider.Controllers[0].Input.InputAction = reference;
            m_inputProvider.Controllers[1].Input.InputAction = reference;
        }
    }
}