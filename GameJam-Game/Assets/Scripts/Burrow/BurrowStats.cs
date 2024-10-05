using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.Burrow
{
    public class BurrowStats : MonoBehaviour
    {
        [SerializeField] private BurrowData m_initialData;
        
        public BurrowData InitialData => this.m_initialData;
    }
}