using com.game.utilities.input;
using UnityEngine.InputSystem;

namespace com.game.player
{
    public static class ExtensionMethods
    {
        public static InputAction GetActionForPlayer(this PlayerComponentBase playerComponent, InputActionReference directRefernce)
        {
            return InputHelpers.GetAction(playerComponent, directRefernce);
        }

        public static InputAction GetAction(this Player player, InputActionReference directRefernce)
        {
            return InputHelpers.GetAction(player.Hub.InputHandler, directRefernce);
        }
    }
}
