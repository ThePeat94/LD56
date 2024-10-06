using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.EventBinding;
using Nidavellir.GameEventBus.Events;
using Nidavellir.Scriptables;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_timeDisplay;
        
        private IEventBinding<FiveIngameMinutesPassedEvent> m_fiveIngameMinutesPassedBinding;
        private IEventBinding<DayPassedEvent> m_dayPassedBinding;
        
        private void Awake()
        {
            this.m_fiveIngameMinutesPassedBinding = new EventBinding<FiveIngameMinutesPassedEvent>(this.OnFiveIngameMinutesPassed);
            this.m_dayPassedBinding = new EventBinding<DayPassedEvent>(this.OnDayPassed);

            GameEventBus<FiveIngameMinutesPassedEvent>.Register(this.m_fiveIngameMinutesPassedBinding);
            GameEventBus<DayPassedEvent>.Register(this.m_dayPassedBinding);
        }
        
        private void OnDestroy()
        {
            GameEventBus<FiveIngameMinutesPassedEvent>.Unregister(this.m_fiveIngameMinutesPassedBinding);
            GameEventBus<DayPassedEvent>.Unregister(this.m_dayPassedBinding);
        }
        
        private void OnFiveIngameMinutesPassed(object sender, FiveIngameMinutesPassedEvent e)
        {
            this.m_timeDisplay.text = $"Day {e.CurrentDay}, {e.Hours}:{e.Minutes:D2}";
        }
        
        private void OnDayPassed(object sender, DayPassedEvent e)
        {
            this.m_timeDisplay.text = $"Day {e.CurrentDay}, {e.Hours}:{e.Minutes:D2}";
        }
    }
}