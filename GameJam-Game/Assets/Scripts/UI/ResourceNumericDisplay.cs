using System;
using System.Runtime.InteropServices;
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
        [SerializeField] private TextMeshProUGUI m_valueDisplay;

        private float _currentHighScore;
        

        private void Awake()
        {
            this.m_toDisplay.ResourceController.ResourceValueChanged += this.OnResourceValueChanged;
            this.m_toDisplay.ResourceController.MaxValueChanged += this.OnMaximumValueChanged;
            this._currentHighScore = PlayerPrefs.GetFloat("HighScore", 0);
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
            this.m_textDisplay.text = $"{(int)Math.Floor(this.m_toDisplay.ResourceController.CurrentValue)}";
            var highScore = PlayerPrefs.GetFloat("HighScore", 0);
            if (this.m_toDisplay.ResourceController.CurrentValue > highScore)
            {
                highScore = this.m_toDisplay.ResourceController.CurrentValue;
            }
            this.m_valueDisplay.text = $"{(int)Math.Floor(highScore)}";
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