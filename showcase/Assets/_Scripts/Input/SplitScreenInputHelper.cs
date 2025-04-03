using com.absence.attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace com.game.input
{
    public class SplitScreenInputHelper : MonoBehaviour
    {
        [SerializeField, Readonly] private PlayerInputManager m_target;

        public EventSystem EventSystem
        {
            get
            {
                if (enabled)
                    return MultiplayerEventSystem.current;
                else
                    return null;
            }
        }

        private void Start()
        {
            if (Game.LobbyType != GameLobbyType.SplitScreen)
            {
                m_target.DisableJoining();
                m_target.splitScreen = false;
                enabled = false;
                return;
            }

            m_target.EnableJoining();
            m_target.splitScreen = true;

            m_target.onPlayerJoined += OnPlayerJoined;
            m_target.onPlayerLeft += OnPlayerLeft;
        }

        private void OnPlayerJoined(PlayerInput input)
        {
            Debug.Log($"Player#{input.playerIndex} joined!");
        }

        private void OnPlayerLeft(PlayerInput input)
        {
            Debug.Log($"Player#{input.playerIndex} left!");
        }

        [Button("Toggle Split-Screen")]
        void ToggleSplitScreen()
        {
            if (!Application.isPlaying)
                return;

            m_target.splitScreen = !m_target.splitScreen;
        }

        private void Reset()
        {
            m_target = GetComponent<PlayerInputManager>();
        }
    }
}