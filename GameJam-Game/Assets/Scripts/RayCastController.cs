using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InputProcessor = Nidavellir.Input.InputProcessor;

namespace Nidavellir
{
    public class RayCastController : MonoBehaviour
    {
        private InputProcessor _playerController;
        
        // Start is called before the first frame update
        void Start()
        {
            this._playerController = FindFirstObjectByType<InputProcessor>();
            this._playerController.OnPlacePerformed += OnClickPerformed;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnClickPerformed(object sender, System.EventArgs args)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var vector = new Vector3(mousePos.x, mousePos.y, 0);
            
            var hit = Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out var h);
            if (hit)
            {
                var comp = h.transform.GetComponent<ClickableObject>();
                if (!comp)
                {
                    return;
                }
                
                comp.HandleClick();
            }
        }
    }
}
