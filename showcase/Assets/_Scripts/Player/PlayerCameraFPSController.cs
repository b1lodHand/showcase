using Cinemachine;
using com.absence.attributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.game.player
{
    public class FPSCameraController : CinemachineExtension
    {
        //[SerializeField, Required] private PlayerInputHandler m_inputHandler;
        [SerializeField] float m_verticalClampAngle = 70f;
        [SerializeField] private Vector2 m_sensitivity;

        InputAction m_lookActionReference;
        Vector2 m_inputDelta;

        float m_startEulerX; // Vertical.
        float m_startEulerY; // Horizontal
        bool m_initialized = false;

        private void Start()
        {
            //m_lookActionReference = m_inputHandler.PlayerInputActions.Player.Look;
        }

        private void Clamp(ref CameraState camState)
        {
            Vector3 currentEulers = camState.RawOrientation.eulerAngles;

            float deltaEulerX = currentEulers.x - m_startEulerX;

            deltaEulerX = Mathf.Clamp(deltaEulerX, -m_verticalClampAngle, m_verticalClampAngle);

            deltaEulerX += m_startEulerX;

            camState.RawOrientation = Quaternion.Euler(deltaEulerX, currentEulers.y, currentEulers.z);
        }

        private void RotateCamera(ref CameraState camState, float deltaTime)
        {
            Vector3 currentEulers = camState.RawOrientation.eulerAngles;

            float horizontalRotation = m_inputDelta.x * m_sensitivity.x * deltaTime;
            float verticalRotation = m_inputDelta.y * m_sensitivity.y * deltaTime;

            camState.RawOrientation = Quaternion.Euler(currentEulers.x + verticalRotation, currentEulers.y + horizontalRotation, currentEulers.z);
        }

        private void GetInput()
        {
            m_inputDelta = m_lookActionReference.ReadValue<Vector2>();
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (vcam.Follow == null) return;
            if (stage != CinemachineCore.Stage.Aim) return;

            if (!m_initialized)
            {
                m_startEulerX = state.RawOrientation.eulerAngles.x;
                m_startEulerY = state.RawOrientation.eulerAngles.y;
                m_initialized = true;
            }

            GetInput();
            RotateCamera(ref state, deltaTime);
            //Clamp(ref state);
        }
    }
}