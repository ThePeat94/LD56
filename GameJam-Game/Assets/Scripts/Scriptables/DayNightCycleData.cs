using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Day Night Cycle Data", menuName = "Data/Day Night Cycle", order = 0)]
    public class DayNightCycleData : ScriptableObject
    {
        //frame 0: 00:00
        [SerializeField] private int m_startingTimeFrame;
        [SerializeField] private int m_framesPerCycle;
        
        public int StartingTimeFrame => this.m_startingTimeFrame;
        public int FramesPerCycle => this.m_framesPerCycle;
    }
}