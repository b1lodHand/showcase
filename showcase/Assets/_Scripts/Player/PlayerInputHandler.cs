using com.game.input;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine;
using com.absence.attributes;

namespace com.game.player
{
    [RequireComponent(typeof(PlayerInput))]
    [DefaultExecutionOrder(-1000)]
    public class PlayerInputHandler : PlayerComponentBase
    {
        [SerializeField, Readonly] private PlayerInput m_target;

        public PlayerInput PlayerInput => m_target;
        public EventSystem EventSystem => EventSystem.current;
        public InputActionAsset InputActionAsset => m_actionAsset;
        public PlayerInputActions InputActions => m_inputActions;

        InputActionAsset m_actionAsset;
        PlayerInputActions m_inputActions;

        private void Awake()
        {
            m_actionAsset = m_target.actions;
            m_inputActions = new();

            if (Game.LobbyType == GameLobbyType.SplitScreen)
                SetupForSplitScreen();

            if (IsLocal) 
                Apply();
        }

        void SetupForSplitScreen()
        {
            m_target.camera = m_owner.Hub.Camera.NativeCamera;
        }

        void Apply()
        {
            //m_inputActions.Player.Move.performed += OnMoveInput;
            //m_inputActions.Player.Move.canceled += OnStop;
            //m_inputActions.Player.Jump.performed += OnJumpInput;
            //m_inputActions.Player.Emotes.performed += OnEmoteInput;
            //m_inputActions.Player.Sprint.started += OnSprintInput_Start;
            //m_inputActions.Player.Sprint.canceled += OnSprintInput_Cancel;
            //m_inputActions.Player.SwitchCamera.performed += OnSwitchCameraInput;
            //m_inputActions.Player.LockCamera.started += OnLockCameraInput_Start;
            //m_inputActions.Player.LockCamera.canceled += OnLockCameraInput_Cancel;
            //m_inputActions.Player.Interact.performed += OnInteractionInput;
        }

        void Revert()
        {
            //m_inputActions.Player.Move.performed -= OnMoveInput;
            //m_inputActions.Player.Move.canceled -= OnStop;
            //m_inputActions.Player.Jump.performed -= OnJumpInput;
            //m_inputActions.Player.Emotes.performed -= OnEmoteInput;
            //m_inputActions.Player.Sprint.started -= OnSprintInput_Start;
            //m_inputActions.Player.Sprint.canceled -= OnSprintInput_Cancel;
            //m_inputActions.Player.SwitchCamera.performed -= OnSwitchCameraInput;
            //m_inputActions.Player.LockCamera.started -= OnLockCameraInput_Start;
            //m_inputActions.Player.LockCamera.canceled -= OnLockCameraInput_Cancel;
            //m_inputActions.Player.Interact.performed -= OnInteractionInput;
        }

        //private void OnInteractionInput(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveInteractionInput();
        //}

        //private void OnSwitchCameraInput(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveSwitchCameraInput();
        //}

        //private void OnStop(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveMoveInput(Vector2.zero);
        //}

        //private void OnMoveInput(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveMoveInput(context.ReadValue<Vector2>());
        //}

        //private void OnSprintInput_Cancel(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveSprintInput(false);
        //}

        //private void OnSprintInput_Start(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveSprintInput(true);
        //}

        //private void OnEmoteInput(InputAction.CallbackContext context)
        //{
        //    float emoteIndex = context.ReadValue<float>();
        //    PlayerInputEventChannel.ReceiveEmoteInput(emoteIndex);
        //}

        //private void OnJumpInput(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveJumpInput();
        //}

        //private void OnLockCameraInput_Cancel(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveLockCameraInput(false);
        //}

        //private void OnLockCameraInput_Start(InputAction.CallbackContext context)
        //{
        //    PlayerInputEventChannel.ReceiveLockCameraInput(true);
        //}

        private void Reset()
        {
            m_target = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            m_inputActions.Enable();
        }

        private void OnDisable()
        {
            m_inputActions.Disable();
        }

        private void OnDestroy()
        {
            Revert();
            m_inputActions.Disable();
        }
    }
}
