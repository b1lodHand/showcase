using com.absence.attributes;
using UnityEngine;

namespace com.game.generics
{
    public class Jump : MonoBehaviour
    {
        public enum JumpTarget
        {
            Rigidbody,
            CharacterController
        }

        public enum JumpDirection
        {
            Up,
            Right,
            Forward,
        }

        [SerializeField] protected JumpTarget m_jumpTarget = JumpTarget.Rigidbody;
        [SerializeField] protected JumpDirection m_jumpDirection = JumpDirection.Up;
        [SerializeField, Required] protected Transform m_orientation;

        [SerializeField, ShowIf(nameof(m_jumpTarget), JumpTarget.Rigidbody), Required] 
        protected Rigidbody m_rigidbody;

        [SerializeField, ShowIf(nameof(m_jumpTarget), JumpTarget.CharacterController), Required] 
        protected CharacterController m_characterController;

        [SerializeField] protected float m_jumpImpulseStrength;
        [SerializeField] protected float m_jumpForceStrength;
        [SerializeField] protected float m_jumpForceDuration;

        bool m_isJumping;
        bool m_shouldApplyJumpForce;
        float m_jumpForceTimer;

        private void Update()
        {
            if (!m_isJumping)
                return;

            if (!m_shouldApplyJumpForce)
                return;

            if (m_jumpForceTimer > 0f) m_jumpForceTimer -= Time.deltaTime;
            else m_shouldApplyJumpForce = false;
        }

        private void FixedUpdate()
        {
            if (!m_isJumping)
                return;

            if (!m_shouldApplyJumpForce)
                return;

            ApplyForce();
        }

        public virtual void StartJumping(bool applyImpulse = true)
        {
            if (m_isJumping)
                return;

            m_isJumping = true;
            m_shouldApplyJumpForce = true;
            m_jumpForceTimer = m_jumpForceDuration;

            if (applyImpulse) 
                ApplyImpulse();
        }

        public virtual void StopJumping()
        {
            if (!m_isJumping)
                return;

            m_isJumping = false;
            m_shouldApplyJumpForce = false;
            m_jumpForceTimer = 0f;
        }

        public virtual void JumpInstant()
        {
            StartJumping(true);
            StopJumping();
        }

        protected virtual void ApplyImpulse()
        {
            switch (m_jumpTarget)
            {
                case JumpTarget.Rigidbody:
                    ApplyImpulseRigidbody();
                    break;
                case JumpTarget.CharacterController:
                    ApplyImpulseCharacterController();
                    break;
                default:
                    break;
            }
        }

        protected virtual void ApplyForce()
        {
            switch (m_jumpTarget)
            {
                case JumpTarget.Rigidbody:
                    ApplyForceRigidbody();
                    break;
                case JumpTarget.CharacterController:
                    ApplyForceCharacterController();
                    break;
                default:
                    break;
            }
        }

        protected virtual void ApplyImpulseCharacterController()
        {
            
        }

        protected virtual void ApplyForceCharacterController()
        {

        }

        protected virtual void ApplyImpulseRigidbody()
        {
            float strength = GetImpulseStrength(m_jumpImpulseStrength);
            Vector3 direction = GetJumpDirection(m_orientation);

            m_rigidbody.AddForce(strength * direction, ForceMode.Impulse);
        }

        protected virtual void ApplyForceRigidbody()
        {
            ApplyForceRigidbody(GetJumpDirection(m_orientation));
        }

        protected virtual void ApplyForceRigidbody(Vector3 lockedImpulseDirection)
        {
            float strength = GetForceStrength(m_jumpForceStrength);
            Vector3 direction = lockedImpulseDirection;

            m_rigidbody.AddForce(strength * direction, ForceMode.Force);
        }

        protected virtual Vector3 GetJumpDirection(Transform orientation)
        {
            Vector3 result;
            switch (m_jumpDirection)
            {
                case JumpDirection.Up:
                    result = orientation.up;
                    break;
                case JumpDirection.Right:
                    result = orientation.right;
                    break;
                case JumpDirection.Forward:
                    result = orientation.forward;
                    break;
                default:
                    result = Vector3.zero;
                    break;
            }

            return result;
        }
        protected virtual float GetImpulseStrength(float defaultImpulseStrength)
        {
            return defaultImpulseStrength;
        }
        protected virtual float GetForceStrength(float defaultForceStrength)
        {
            return defaultForceStrength;
        }
    }
}
