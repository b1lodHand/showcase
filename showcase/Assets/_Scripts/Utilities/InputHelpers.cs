using com.game.player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace com.game.utilities.input
{
    public static class InputHelpers
    {
        public static bool IsLocalInput(int targetPlayerIndex, InputAction.CallbackContext context)
        {
            if (Game.LobbyType != GameLobbyType.SplitScreen)
                return true;

            InputUser? user = InputUser.FindUserPairedToDevice(context.control.device);

            if (!user.HasValue)
            {
                Debug.LogWarning("There are no users associated with this input control. Returning false.");
                return false;
            }

            return user.Value.index == targetPlayerIndex;
        }

        public static InputAction GetAction(PlayerInputHandler inputHandler, InputActionReference directReference)
        {
            if (Game.LobbyType != GameLobbyType.SplitScreen)
                return directReference.action;

            return inputHandler.InputActionAsset.FindAction(directReference.action.id);
        }
    }
}
