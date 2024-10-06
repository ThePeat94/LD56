using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class FoodConsumer : MonoBehaviour
    {
        [SerializeField] private Resource m_foodResource;
        [SerializeField] private Resource m_mateResource;

        [SerializeField] private FoodConsumptionData m_foodConsumptionData;
        
        private float m_currentConsumptionFrameCooldown;
        
        private void FixedUpdate()
        {
            this.m_currentConsumptionFrameCooldown--;
            if (this.m_currentConsumptionFrameCooldown > 0)
                return;

            this.m_currentConsumptionFrameCooldown = this.m_foodConsumptionData.FramesBetweenConsumption;
            
            this.m_foodResource.ResourceController.ForceUseResource(this.m_mateResource.ResourceController.CurrentValue * this.m_foodConsumptionData.ConsumptionPerMate);
        }
        
    }
}