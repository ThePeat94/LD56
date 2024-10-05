using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nidavellir.Input
{
    public class InputProcessor : MonoBehaviour
    {
        private PlayerInput m_playerInput;

        public Vector2 Movement { get; private set; }

        public bool InteractTriggered => this.m_playerInput.Actions.Interact.triggered;
        public bool ShootTriggered => this.m_playerInput.Actions.Shoot.triggered;
        public bool QuitTriggered => this.m_playerInput.Actions.Quit.triggered;
        public bool BackToMainTriggered => this.m_playerInput.Actions.BackToMenu.triggered;
        public bool RetryTriggered => this.m_playerInput.Actions.Retry.triggered;
        
        public event Action<InputAction.CallbackContext> ElevatorUpTriggered
        {
            add => this.m_playerInput.Elevator.GoUp.performed += value;
            remove => this.m_playerInput.Elevator.GoUp.performed -= value;
        }
        
        public event Action<InputAction.CallbackContext> ElevatorDownTriggered
        {
            add => this.m_playerInput.Elevator.GoDown.performed += value;
            remove => this.m_playerInput.Elevator.GoDown.performed -= value;
        }

        public bool IsBoosting { get; private set; }

        private void Awake()
        {
            this.m_playerInput = new PlayerInput();
            this.m_playerInput.Actions.Boost.started += this.OnBoostStarted;
            this.m_playerInput.Actions.Boost.canceled += this.OnBoostEnded;
        }

        private void Update()
        {
            this.Movement = this.m_playerInput.Actions.Move.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            this.m_playerInput?.Enable();
        }
        
        public void EnableElevatorControls()
        {
            this.m_playerInput?.Elevator.Enable();
        }
        
        public void DisableElevatorControls()
        {
            this.m_playerInput?.Elevator.Disable();
        }

        private void OnDisable()
        {
            this.m_playerInput?.Disable();
            this.Movement = Vector3.zero;
        }

        private void OnBoostEnded(InputAction.CallbackContext ctx)
        {
            this.IsBoosting = false;
        }

        private void OnBoostStarted(InputAction.CallbackContext ctx)
        {
            this.IsBoosting = true;
        }
    }
}