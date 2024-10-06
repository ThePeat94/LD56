using Nidavellir.EventArgs;
using Nidavellir.Scriptables;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class ResourceNumericDisplay : MonoBehaviour
    {
        [SerializeField] private Resource m_toDisplay;
        [SerializeField] private TextMeshProUGUI m_textDisplay;
        

        private void Awake()
        {
            this.m_toDisplay.ResourceController.ResourceValueChanged += this.OnResourceValueChanged;
            this.m_toDisplay.ResourceController.MaxValueChanged += this.OnMaximumValueChanged;
        }
        
        private void OnDestroy()
        {
            this.m_toDisplay.ResourceController.ResourceValueChanged -= this.OnResourceValueChanged;
            this.m_toDisplay.ResourceController.MaxValueChanged -= this.OnMaximumValueChanged;
        }

        private void Start()
        {
            this.UpdateText();
        }

        private void UpdateText()
        {
            this.m_textDisplay.text = $"{this.m_toDisplay.ResourceController.CurrentValue:F2}/{this.m_toDisplay.ResourceController.MaxValue}";
        }

        private void OnMaximumValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.UpdateText();
        }

        private void OnResourceValueChanged(object sender, ResourceValueChangedEventArgs e)
        {
            this.UpdateText();
        }
    }
}