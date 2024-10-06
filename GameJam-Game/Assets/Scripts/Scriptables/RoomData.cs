using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Room Data", menuName = "Data/Room", order = 0)]
    public class RoomData : ScriptableObject
    {
        [SerializeField] private ResourceData m_initialHealthData;
        [SerializeField] private string m_name;
        [SerializeField] private string m_description;
        
        public ResourceData InitialHealthData => this.m_initialHealthData;
        public string Name => this.m_name;
        public string Description => this.m_description;
    }
}