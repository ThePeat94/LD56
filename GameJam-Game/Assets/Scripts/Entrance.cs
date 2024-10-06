using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using InputProcessor = Nidavellir.Input.InputProcessor;

namespace Nidavellir
{
    public class Entrance : MonoBehaviour
    {
        [SerializeField] private bool inside;
        private PlayerController _playerController;

        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerController>(out var _))
            {
                return;
            }
            this._playerController = null;
        }
    }
}
