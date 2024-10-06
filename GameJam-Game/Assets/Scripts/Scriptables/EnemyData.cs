using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private GameObject m_prefab;
        [SerializeField] private string m_type;
        
    }
}