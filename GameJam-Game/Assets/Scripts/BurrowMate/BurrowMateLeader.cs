using System;
using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using InputProcessor = Nidavellir.Input.InputProcessor;

namespace Nidavellir.Enemy
{
    public class BurrowMateLeader : MonoBehaviour
    {
        private InputProcessor m_inputProcessor;
        public bool CanBeInteractedWith { get; set; } = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!this.CanBeInteractedWith) return;
            
            if (!other.TryGetComponent<PlayerController>(out var player)) return;
            
            this.m_inputProcessor = player.GetComponent<InputProcessor>();
            this.m_inputProcessor.InteractionTriggered += this.OnInteractTriggered;
            this.m_inputProcessor.ShootingTriggered += this.OnShootTriggered;
            
        }

        private void OnShootTriggered(InputAction.CallbackContext obj)
        {
            GameEventBus<BurrowMateGroupDeclinedEvent>.Invoke(this, new());
            this.m_inputProcessor.InteractionTriggered -= this.OnInteractTriggered;
            this.m_inputProcessor.ShootingTriggered -= this.OnShootTriggered;
        }

        private void OnInteractTriggered(InputAction.CallbackContext obj)
        {
            GameEventBus<BurrowMateGroupAcceptedEvent>.Invoke(this, new());
            this.m_inputProcessor.InteractionTriggered -= this.OnInteractTriggered;
            this.m_inputProcessor.ShootingTriggered -= this.OnShootTriggered;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerController>(out var player)) return;

            if (this.m_inputProcessor is null) return;
            
            this.m_inputProcessor.InteractionTriggered -= this.OnInteractTriggered;
            this.m_inputProcessor.ShootingTriggered -= this.OnShootTriggered;
        }
    }
}