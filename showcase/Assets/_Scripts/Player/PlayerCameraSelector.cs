using com.absence.attributes;
using com.game.utilities.cinemachine;
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace com.game.player
{
    public class PlayerCameraSelector : PlayerComponentBase
    {
        [SerializeField, Readonly] private PlayerCamera m_currentCamera;
        [SerializeField] private PlayerCamera m_defaultCamera;
        [SerializeField, Required] private Camera m_nativeCamera;
        [SerializeField, Required] private CinemachineBrain m_brain;
        [SerializeField, Required] private Transform m_cameraFollowTransform;
        public PlayerCamera Current => m_currentCamera;
        public Camera NativeCamera => m_nativeCamera;
        public CinemachineBrain Brain => m_brain;
        public event Action<PlayerCamera, PlayerCamera> OnCameraChange;

        bool m_initialized;
        
        private void Awake()
        {
            m_brain.ChannelMask = CinemachineHelpers.GetOutputChannelForPlayer(m_owner.Index, false);

            if (!m_initialized)
                ForceInitializeWith(m_defaultCamera);
        }

        public void ReinitializeWithDefault()
        {
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

            PlayerCamera previousCamera = m_currentCamera;
            m_currentCamera = target;
            m_currentCamera.Initialize(CreateDefaultContext());

            OnCameraChange?.Invoke(previousCamera, target);
            m_initialized = true;
        }

        PlayerCamera.Context CreateDefaultContext()
        {
            return new PlayerCamera.Context()
            {
                Owner = m_owner,
                Follow = m_cameraFollowTransform,
                SplitScreen = Game.LobbyType == GameLobbyType.SplitScreen,
            };
        }
    }
}
