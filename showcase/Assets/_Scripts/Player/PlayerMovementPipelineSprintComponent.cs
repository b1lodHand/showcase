using com.game.utilities.componentpipelines;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static com.game.player.PlayerMovementPipeline;

namespace com.game.player
{
    public class PlayerMovementPipelineSprintComponent : ComponentPipelineComponentBase<PlayerMovement, PipelineContext>
    {
        public enum SprintModifierType
        {
            Multiplier,
            Incremention,
            FixedValue,
        }

        [Space]

        [SerializeField] private InputActionReference m_sprintActionReference;
        [SerializeField] private SprintModifierType m_modifierType = SprintModifierType.Multiplier;
        [SerializeField] private float m_value;

        InputAction m_sprintAction;
        bool m_isSprinting = false;

        private void Start()
        {
            m_sprintAction = m_target.GetActionForPlayer(m_sprintActionReference);
            m_sprintAction.performed += OnSprintPressed;
            m_sprintAction.canceled += OnSprintUnpressed;
        }

        private void OnSprintPressed(InputAction.CallbackContext context)
        {
            m_isSprinting = true;
        }

        private void OnSprintUnpressed(InputAction.CallbackContext context)
        {
            m_isSprinting = false;
        }

        public override PipelineContext Enpipe(PlayerMovement target, PipelineContext context)
        {
            if (context.Type == PipelineContext.ContextType.MoveSpeed)
                context.MoveSpeed = EvaluateMoveSpeed(context.MoveSpeed);

            return context;
        }

        float EvaluateMoveSpeed(float moveSpeed)
        {
            if (!m_isSprinting)
                return moveSpeed;

            switch (m_modifierType)
            {
                case SprintModifierType.Multiplier:
                    return moveSpeed * m_value;
                case SprintModifierType.Incremention:
                    return moveSpeed + m_value;
                case SprintModifierType.FixedValue:
                    return m_value;
                default:
                    return m_value;
            }
        }
    }
}
