using System;
using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.Events;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] private DayNightCycleData m_dayNightCycleData;
        
        private int m_currentTimeFrame;
        private int m_pastDaysCount;

        private int m_framesPerFiveIngameMinutes;
        
        public int CurrentTimeFrame => this.m_currentTimeFrame;
        public int PastDaysCount => this.m_pastDaysCount;
        
        private void Start()
        {
            var framesPerIngameMinute = Mathf.CeilToInt(this.m_dayNightCycleData.FramesPerCycle / 24f / 60f);
            this.m_framesPerFiveIngameMinutes = (framesPerIngameMinute * 5);
        }

        private void FixedUpdate()
        {
            this.m_currentTimeFrame++;
            if (this.m_currentTimeFrame >= this.m_dayNightCycleData.FramesPerCycle)
            {
                this.m_currentTimeFrame = 0;
                this.m_pastDaysCount++;
                GameEventBus<DayPassedEvent>.Invoke(this, new(0, 0, this.m_pastDaysCount));
            }
            
            if (this.m_currentTimeFrame % this.m_framesPerFiveIngameMinutes == 0)
            {
                var currentDayHours = this.m_currentTimeFrame / (this.m_dayNightCycleData.FramesPerCycle / 24);
                var currentDayMinutes = this.m_currentTimeFrame / (this.m_dayNightCycleData.FramesPerCycle / 24 / 60) % 60;
                GameEventBus<FiveIngameMinutesPassedEvent>.Invoke(this, new(currentDayHours, currentDayMinutes, this.m_pastDaysCount));
            }
        }
    }
}