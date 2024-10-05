using UnityEngine.InputSystem;

namespace Nidavellir
{
    public class ClickInputEventArgs : System.EventArgs
    {

        public InputAction.CallbackContext Ctx;
        
        public ClickInputEventArgs(InputAction.CallbackContext ctx)
        {
            this.Ctx = ctx;
        }
    }
}