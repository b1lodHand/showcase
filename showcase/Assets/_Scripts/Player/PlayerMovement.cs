using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.player
{
    public class PlayerMovement : PlayerComponentBase
    {
        public enum ExtensionContext
        {
            MoveSpeed,
        }

        [SerializeField] private InputActionReference m_moveActionReference;

        InputAction m_moveAction;

        private void Start()
        {
            m_moveAction = Player.GetAction(m_moveActionReference);
            m_moveAction.performed += OnMove;
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
