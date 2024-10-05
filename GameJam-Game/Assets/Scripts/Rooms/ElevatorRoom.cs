using System;
using Nidavellir.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using InputProcessor = Nidavellir.Input.InputProcessor;

namespace Nidavellir.Rooms
{
    public class ElevatorRoom : Room
    {
        [SerializeField] private ElevatorRoom m_upwardElevator;
        [SerializeField] private ElevatorRoom m_downwardElevator;
        [SerializeField] private Transform m_spawnPoint;

        private PlayerController m_playerController;
        
        public override void PlaceRoom()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<PlayerController>(out var player))
            {
                return;
            }

            this.m_playerController = player;
            var inputProcessor = player.GetComponent<InputProcessor>();
            inputProcessor.EnableElevatorControls();
            inputProcessor.ElevatorUpTriggered += this.OnElevatorUpTriggered;
            inputProcessor.ElevatorDownTriggered += this.OnElevatorDownTriggered;
        }

        private void OnElevatorDownTriggered(InputAction.CallbackContext obj)
        {
            if (this.m_downwardElevator is null)
                return;

            var characterController = this.m_playerController.GetComponent<CharacterController>();
            var inputProcessor = this.m_playerController.GetComponent<InputProcessor>();
            inputProcessor.ElevatorUpTriggered -= this.OnElevatorUpTriggered;
            inputProcessor.ElevatorDownTriggered -= this.OnElevatorDownTriggered;
            characterController.enabled = false;
            this.m_playerController.transform.position = this.m_downwardElevator.m_spawnPoint.position;
            characterController.enabled = true;
        }

        private void OnElevatorUpTriggered(InputAction.CallbackContext obj)
        {
            if (this.m_upwardElevator is null)
                return;
            
            var characterController = this.m_playerController.GetComponent<CharacterController>();
            var inputProcessor = this.m_playerController.GetComponent<InputProcessor>();
            inputProcessor.ElevatorUpTriggered -= this.OnElevatorUpTriggered;
            inputProcessor.ElevatorDownTriggered -= this.OnElevatorDownTriggered;
            characterController.enabled = false;
            this.m_playerController.transform.position = this.m_upwardElevator.m_spawnPoint.position;
            characterController.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerController>(out var player))
            {
                return;
            }
            
            var inputProcessor = player.GetComponent<InputProcessor>();
            inputProcessor.DisableElevatorControls();
            this.m_playerController = null;
        }
    }
}