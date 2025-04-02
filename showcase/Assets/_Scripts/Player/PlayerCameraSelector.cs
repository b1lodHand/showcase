using com.absence.attributes;
using UnityEngine;

namespace com.game.player
{
    public class PlayerCameraSelector : PlayerComponentBase
    {
        [SerializeField, Readonly] private PlayerCamera m_currentCamera;
        [SerializeField] private PlayerCamera m_defaultCamera;
        [SerializeField, Required] private Transform m_cameraFollowTransform;
        public PlayerCamera Current => m_currentCamera;

        bool m_initialized;

        private void Start()
        {
            if (!m_initialized)
                ForceInitializeWith(m_defaultCamera);
        }

        public void ForceInitializeWith(PlayerCamera target)
        {
            bool hasCurrent = m_currentCamera != null;
            bool hasChange = m_currentCamera != target;

            if ((!hasCurrent) && target == null)
                return;

            if (!hasChange)
                return;

            if (hasCurrent)
                m_currentCamera.Initialize(null);

            m_currentCamera = target;
            m_currentCamera.Initialize(CreateDefaultContext());

            m_initialized = true;
        }

        PlayerCamera.Context CreateDefaultContext()
        {
            return new PlayerCamera.Context()
            {
                Follow = m_cameraFollowTransform
            };
        }
    }
}
