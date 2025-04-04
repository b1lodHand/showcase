using com.absence.attributes;
using com.game.utilities.extensiblecomponents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.player
{
    public class PlayerMovement : PlayerComponentBase, IExtensibleComponent<PlayerMovement.ExtensionContext>
    {
        public class ExtensionContext
        {
            public enum ContextType
            {
                MoveSpeed,
            }

            public ContextType Type;
            public float MoveSpeed;
        }

        public enum MovementTarget
        {
            Rigidbody,
            CharacterController,
        }

        [SerializeField] protected bool m_debugMode;
        [SerializeField] protected MovementTarget m_movementTarget = MovementTarget.Rigidbody;
        [SerializeField] protected InputActionReference m_moveActionReference;

        [SerializeField, ShowIf(nameof(m_movementTarget), MovementTarget.Rigidbody), Required]
        protected Rigidbody m_rigidbody;

        [SerializeField, ShowIf(nameof(m_movementTarget), MovementTarget.CharacterController), Required]
        protected CharacterController m_characterController;

        [SerializeField]
        protected float m_defaultMoveSpeed;

        [Space, SerializeField] protected List<ComponentExtensionBase<ExtensionContext>> m_extensionList;

        public List<ComponentExtensionBase<ExtensionContext>> Extensions => m_extensionList;

        protected InputAction m_moveAction;
        protected Vector2 m_input;
        protected Vector3 m_projectedInputDirection;

        private void Start()
        {
            m_moveAction = Player.GetAction(m_moveActionReference);
            m_moveAction.performed += OnMove;
        }

        private void FixedUpdate()
        {
            Move(m_input, m_projectedInputDirection);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            //if (!InputHelpers.IsLocalInput(m_owner.Index, context))
            //    return;

            m_input = GetInput(context);
            m_projectedInputDirection = ProjectInput(m_input);

            if (m_debugMode) 
                Debug.Log($"[Player#{m_owner.Index}] Movement Input: ({m_input.x}, {m_input.y})");
        }

        protected virtual void FetchSpeed(float defaultMoveSpeed)
        {
            
        }

        protected virtual void Move(Vector2 input, Vector3 projectedInputDirection)
        {

        }

        protected virtual Vector2 GetInput(InputAction.CallbackContext context)
        {
            return context.ReadValue<Vector2>();
        }

        protected virtual Vector3 ProjectInput(Vector2 input)
        {
            return new Vector3(input.x, 0f, input.y);
        }
    }
}
