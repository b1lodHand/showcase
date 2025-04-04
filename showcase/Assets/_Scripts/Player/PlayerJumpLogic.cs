using com.absence.attributes;
using com.game.generics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.player
{
    [RequireComponent(typeof(Jump))]
    public class PlayerJumpLogic : MonoBehaviour
    {
        [SerializeField, Readonly] private Jump m_jumpScript;
        [SerializeField, Required] private Player m_target;
        [SerializeField, Required] private InputActionReference m_jumpActionReference;

        [SerializeField] private bool m_variableJumpEnabled;
        [SerializeField] private bool m_inAirJumpEnabled;

        [SerializeField, ShowIf(nameof(m_inAirJumpEnabled)), Min(0)]
        private int m_maxInAirJumps;

        InputAction m_jumpAction;
        int m_jumpCount;
        int m_totalJumps;
        bool m_jumpPressed;
        bool m_wasGroundedLastFrame;
        bool m_isGrounded;

        private void Start()
        {
            m_totalJumps = GetTotalJumps();

            m_jumpAction = m_target.GetAction(m_jumpActionReference);
            m_jumpAction.started += OnJumpPressed;
            m_jumpAction.canceled += OnJumpUnpressed;
        }

        private void FixedUpdate()
        {
            m_wasGroundedLastFrame = m_target.Hub.Movement.WasGroundedLastFrame;
            m_isGrounded = m_target.Hub.Movement.IsGrounded;

            if (m_isGrounded && !m_wasGroundedLastFrame)
                m_jumpCount = 0;
        }

        private void OnJumpPressed(InputAction.CallbackContext context)
        {
            m_jumpPressed = true;

            if ((!m_isGrounded) && (!m_inAirJumpEnabled))
                return;

            if (m_jumpCount >= m_totalJumps)
                return;

            Jump();
            m_jumpCount++;
        }

        private void OnJumpUnpressed(InputAction.CallbackContext context)
        {
            m_jumpPressed = false;

            if (m_variableJumpEnabled)
                m_jumpScript.StopJumping();
        }

        void Jump()
        {
            if (m_variableJumpEnabled) m_jumpScript.StartJumping();
            else m_jumpScript.JumpInstant();
        }

        int GetTotalJumps()
        {
            if (!m_inAirJumpEnabled)
                return 1;

            if (m_maxInAirJumps < 0)
                return int.MaxValue;

            return 1 + m_maxInAirJumps;
        }

        private void Reset()
        {
            m_jumpScript = GetComponent<Jump>();
        }
    }
}
