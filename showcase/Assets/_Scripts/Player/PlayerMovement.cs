using com.absence.attributes;
using com.game.utilities.checkers;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.player
{
    public class PlayerMovement : PlayerComponentBase
    {
        public enum MovementTarget
        {
            Rigidbody,
            CharacterController,
        }

        [Header("Utilities")]

        [SerializeField] protected bool m_debugMode;
        [SerializeField] protected MovementTarget m_movementTarget = MovementTarget.Rigidbody;
        [SerializeField, Required] protected InputActionReference m_moveActionReference;
        [SerializeField, Required] protected Transform m_orientation;
        [SerializeField, Required] protected CheckerBase m_groundChecker;
        [SerializeField] protected PlayerMovementPipeline m_pipeline;

        [SerializeField, ShowIf(nameof(m_movementTarget), MovementTarget.Rigidbody), Required]
        protected Rigidbody m_rigidbody;

        [SerializeField, ShowIf(nameof(m_movementTarget), MovementTarget.CharacterController), Required]
        protected CharacterController m_characterController;

        [Space, Header("Settings")]

        [SerializeField] protected bool m_useGravity = true;
        [SerializeField] protected float m_defaultMoveSpeed;

        [Space]

        [SerializeField] 
        private float m_groundedGravityMultiplier = 1;

        [SerializeField, ShowIf(nameof(m_movementTarget), MovementTarget.Rigidbody)]
        private float m_groundedDrag = 0;

        [Space]

        [SerializeField] 
        private float m_inAirRisingGravityMultiplier = 1;

        [SerializeField]
        private float m_inAirFallingGravityMultiplier = 1;

        [SerializeField, ShowIf(nameof(m_movementTarget), MovementTarget.Rigidbody)]
        private float m_inAirDrag = 0;

        public bool WasGroundedLastFrame => m_wasGroundedLastFrame;
        public bool IsGrounded => m_isGrounded;
        public PlayerMovementPipeline Pipeline => m_pipeline;

        protected InputAction m_moveAction;
        protected Vector2 m_input;
        protected Vector3 m_projectedInputDirection;
        protected Vector3 m_moveDirection;
        protected float m_gravityMultiplier;
        protected bool m_isGrounded;
        protected bool m_wasGroundedLastFrame;

        private void Awake()
        {
            m_groundChecker.Mode = CheckerBase.RefreshMode.FixedUpdate;
        }

        private void Start()
        {
            m_moveAction = Player.GetAction(m_moveActionReference);
            m_moveAction.performed += OnMove;
        }

        private void FixedUpdate()
        {
            m_wasGroundedLastFrame = m_isGrounded;
            m_isGrounded = m_groundChecker.Result;

            if (m_movementTarget == MovementTarget.Rigidbody)
            {
                m_rigidbody.useGravity = m_useGravity;
                m_rigidbody.drag = m_isGrounded ? m_groundedDrag : m_inAirDrag;

                float gravityMultiplier = m_isGrounded ?
                    m_groundedGravityMultiplier : m_rigidbody.velocity.y > 0f ?
                    m_inAirRisingGravityMultiplier : m_inAirFallingGravityMultiplier;

                float difference = gravityMultiplier - 1f;

                m_rigidbody.AddForce(Physics.gravity * difference, ForceMode.Force);
            }

            m_moveDirection = ApplyOrientationToDirection(m_projectedInputDirection);
            Move(m_input, m_moveDirection);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            //if (!InputHelpers.IsLocalInput(m_owner.Index, context))
            //    return;

            m_input = GetInput(context);
            m_projectedInputDirection = ProjectInputTo3D(m_input);

            if (m_debugMode) 
                Debug.Log($"[Player#{m_owner.Index}] Movement Input: ({m_input.x}, {m_input.y})");
        }

        protected virtual float GetMoveSpeed(float defaultMoveSpeed)
        {
            if (Pipeline == null)
                return defaultMoveSpeed;

            return Pipeline.EnpipeMoveSpeed(defaultMoveSpeed);
        }

        protected virtual void Move(Vector2 input, Vector3 moveDirection)
        {
            switch (m_movementTarget)
            {
                case MovementTarget.Rigidbody:
                    MoveRigidbody(input, moveDirection);
                    break;
                case MovementTarget.CharacterController:
                    MoveCharacterController(input, moveDirection);
                    break;
                default:
                    throw new Exception("Something went wrong trying to move the player!");
            }
        }

        protected virtual void MoveCharacterController(Vector2 input, Vector3 moveDirection)
        {
            
        }

        protected virtual void MoveRigidbody(Vector2 input, Vector3 moveDirection)
        {
            Vector3 movement = moveDirection * GetMoveSpeed(m_defaultMoveSpeed);
            movement.y = m_rigidbody.velocity.y;

            m_rigidbody.velocity = movement;
        }

        protected virtual Vector2 GetInput(InputAction.CallbackContext context)
        {
            return context.ReadValue<Vector2>();
        }

        protected virtual Vector3 ProjectInputTo3D(Vector2 input)
        {
            Vector3 result = new Vector3(input.x, 0f, input.y);
            return result;
        }

        protected virtual Vector3 ApplyOrientationToDirection(Vector3 direction)
        {
            return m_orientation.localToWorldMatrix.MultiplyVector(direction);
        }
    }
}
