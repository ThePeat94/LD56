using System;
using UnityEngine;

namespace Nidavellir
{
    public class ClickableObject : MonoBehaviour
    {

        public event EventHandler OnClick;
        
        public void HandleClick()
        {
            Debug.Log("click");
            OnClick?.Invoke(this, System.EventArgs.Empty);
        }
    }
}