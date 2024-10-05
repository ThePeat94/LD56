using UnityEngine;

namespace Nidavellir.Scriptables
{
    public class AttributeData : MonoBehaviour
    {
        [SerializeField] private string m_name;
        [SerializeField] private string m_description;
        [SerializeField] private Sprite m_icon;
        [SerializeField] private int m_maxValue;
        
        public string Name => this.m_name;
        public string Description => this.m_description;
        public Sprite Icon => this.m_icon;
        public int MaxValue => this.m_maxValue;
    }
}