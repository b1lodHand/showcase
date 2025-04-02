using com.absence.attributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.input
{
    public class PlayerInputManagerHelper : MonoBehaviour
    {
        [SerializeField, Readonly] private PlayerInputManager m_target;

        private void Awake()
        {
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

        private void Reset()
        {
            m_target = GetComponent<PlayerInputManager>();
        }
    }
}