using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.Events;
using UnityEngine;

namespace Nidavellir.Input
{
    public class DebugInputProcessor : MonoBehaviour
    {
        private PlayerInput m_playerInput;
        private void Awake()
        {
            this.m_playerInput = new PlayerInput();
            
            this.m_playerInput.Debug.SimulateAddFoodStorage.performed += ctx => this.SimulateAddFoodStorage();
            this.m_playerInput.Debug.SimulateAddSleepingRoom.performed += ctx => this.SimulateAddSleepingRoom();
        }

        private void OnEnable()
        {
            this.m_playerInput.Enable();
        }

        private void SimulateAddSleepingRoom()
        {
            GameEventBus<SleepingRoomPlacedEvent>.Invoke(this, new(10));
        }

        private void SimulateAddFoodStorage()
        {
            GameEventBus<FoodRoomPlacedEvent>.Invoke(this, new(10));
        }
    }
}