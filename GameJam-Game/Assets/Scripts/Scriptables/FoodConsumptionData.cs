using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Food Consumption Data", menuName = "Data/Food Consumption", order = 0)]
    public class FoodConsumptionData : ScriptableObject
    {
        [SerializeField] private int m_framesBetweenConsumption;
        [SerializeField] private float m_consumptionPerMate;
        
        public int FramesBetweenConsumption => this.m_framesBetweenConsumption;
        public float ConsumptionPerMate => this.m_consumptionPerMate;
    }
}