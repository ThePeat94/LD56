using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.EventBinding;
using Nidavellir.GameEventBus.Events;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.Burrow
{
    public class BurrowController : MonoBehaviour
    {
        [SerializeField] private Resource m_foodResource;
        [SerializeField] private Resource m_matesResource;
        
        private IEventBinding<FoodRoomPlacedEvent> m_foodRoomPlacedBinding;
        private IEventBinding<SleepingRoomPlacedEvent> m_sleepingRoomPlacedBinding;
        
        private void Awake()
        {
            this.m_foodRoomPlacedBinding = new EventBinding<FoodRoomPlacedEvent>((_, args) => this.OnFoodRoomPlaced(args));
            this.m_sleepingRoomPlacedBinding = new EventBinding<SleepingRoomPlacedEvent>((_, args) => this.OnSleepingRoomPlaced(args));
            
            GameEventBus<FoodRoomPlacedEvent>.Register(this.m_foodRoomPlacedBinding);
            GameEventBus<SleepingRoomPlacedEvent>.Register(this.m_sleepingRoomPlacedBinding);
        }
        
        private void OnFoodRoomPlaced(FoodRoomPlacedEvent e)
        {
            this.m_foodResource.ResourceController.ApplyDeltaToMaximumValue(e.FoodAmount);
        }
        
        private void OnSleepingRoomPlaced(SleepingRoomPlacedEvent e)
        {
            this.m_matesResource.ResourceController.ApplyDeltaToMaximumValue(e.SpaceAmount);
        }
    }
}