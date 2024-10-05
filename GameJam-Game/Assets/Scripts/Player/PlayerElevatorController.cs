using System;
using Nidavellir.Input;
using UnityEngine;

namespace Nidavellir.Player
{
    public class PlayerElevatorController : MonoBehaviour
    {
        private InputProcessor m_inputProcessor;

        public void Init()
        {
            
        }

        private void Start()
        {
            this.m_inputProcessor.EnableElevatorControls();
            
        }

        private void OnDestroy()
        {
            this.m_inputProcessor.DisableElevatorControls();
        }
    }
}