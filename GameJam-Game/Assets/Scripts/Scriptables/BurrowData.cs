using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Burrow", menuName = "Data/Burrow", order = 0)]
    public class BurrowData : ScriptableObject
    {
        [SerializeField] private AttributeData m_foodAttribute;
        [SerializeField] private AttributeData m_matesAttribute;
        
        public AttributeData FoodAttribute => this.m_foodAttribute;
        public AttributeData MatesAttribute => this.m_matesAttribute;
    }
}