using com.game.utilities.input;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace com.game.player
{
    public class PlayerMovement : PlayerComponentBase
    {
        [SerializeField] private InputActionReference m_moveActionReference;

        InputAction m_moveAction;

        private void Start()
        {
            m_moveAction = InputHelpers.GetAction(m_owner.Hub.InputHandler, m_moveActionReference);

            m_moveAction.performed += OnMove;
            m_moveAction.canceled += OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            //Debug.Log("afrg");wd
            //if (!InputHelpers.IsLocalInput(m_owner.Index, context))
            //    return;

            Vector2 input = context.ReadValue<Vector2>();

            Debug.Log($"Player#{m_owner.Index} {input.x}, {input.y}");
        }
    }
}
