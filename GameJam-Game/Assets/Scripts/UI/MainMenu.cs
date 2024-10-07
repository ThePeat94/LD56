using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_startMenu;
        [SerializeField] private GameObject m_credits;
        [SerializeField] private Slider m_musicVolumeSlider;
        [SerializeField] private Slider m_sfxVolumeSlider;
        [SerializeField] private GameObject m_optionsPanel;

        [SerializeField] private GameObject m_gameTransition;
        [SerializeField] private GameObject m_shiverImage;
        
        private void Awake()
        {
            this.m_musicVolumeSlider.onValueChanged.AddListener(this.MusicVolumeSliderChanged);
            this.m_sfxVolumeSlider.onValueChanged.AddListener(this.SfxVolumeSliderChanged);
        }

        private void Start()
        {
            this.m_musicVolumeSlider.value = GlobalSettings.Instance.MusicVolume;
            this.m_sfxVolumeSlider.value = GlobalSettings.Instance.SfxVolume;
            
            this.m_gameTransition.SetActive(false);
        }

        public void BackFromCreditsToStart()
        {
            this.m_startMenu.SetActive(true);
            this.m_credits.SetActive(false);
        }

        public void BackToStartFromOptions()
        {
            this.m_optionsPanel.SetActive(false);
            this.m_startMenu.SetActive(true);
        }

        public void MusicVolumeSliderChanged(float volume)
        {
            GlobalSettings.Instance.MusicVolume = volume;
        }

        public void OpenLink(string url)
        {
            Application.OpenURL(url);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        public void SfxVolumeSliderChanged(float volume)
        {
            GlobalSettings.Instance.SfxVolume = volume;
        }

        public void ShowCredits()
        {
            this.m_startMenu.SetActive(false);
            this.m_credits.SetActive(true);
        }

        public void ShowOptions()
        {
            this.m_optionsPanel.SetActive(true);
            this.m_startMenu.SetActive(false);
        }

        public void StartGame()
        {
            this.m_gameTransition.SetActive(true);
            this.m_startMenu.SetActive(false);
            StartCoroutine(this.StartGameDelayed());
            this.m_shiverImage.transform.DOShakePosition(1f, 5)
                .SetLoops(-1);
        }

        private IEnumerator StartGameDelayed()
        {
            yield return new WaitForSeconds(15);
            SceneManager.LoadScene("mainGameScene");
        }
    }
}