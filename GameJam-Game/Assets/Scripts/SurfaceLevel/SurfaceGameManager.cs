using System.Collections;
using Nidavellir.Input;
using Nidavellir.Scriptables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nidavellir
{
    public class SurfaceGameManager : MonoBehaviour
    {
        public GameObject[] lifeUI = new GameObject[3];
        public int maxLifeCount = 3;
        public int currentLife;  
        [SerializeField] private Resource resource;

        private bool canLoseLife = true;
        public InputProcessor m_playerInputProcessor;
        
        public AudioSource m_audioSource;
        public AudioClip m_gameOverClip;

        public GameObject playerSprite;

        public GameObject player;
        public GameObject burrow;

        void Start()
        {
            this.resource.ResourceController.ResetValues();
            currentLife = maxLifeCount;
            foreach (var points in lifeUI)
            {
                points.GetComponent<Image>().enabled = true;
            }
        }

        public void LooseLife()
        {
            if (!canLoseLife) return;

            canLoseLife = false;
            currentLife--;
            if (currentLife <= 0)
            {
                if (resource.ResourceController.CurrentValue > PlayerPrefs.GetFloat("HighScore", 0))
                {
                    PlayerPrefs.SetFloat("HighScore", resource.ResourceController.CurrentValue);    
                }

                StartCoroutine(GameLost());
            }
            else
            {
                lifeUI[currentLife - 1].GetComponent<Image>().color = Color.black;
                StartCoroutine(Restart());
            }
        }

        private IEnumerator GameLost()
        {
            m_audioSource.clip = m_gameOverClip;
            m_audioSource.Play();
            yield return new WaitForSeconds(m_gameOverClip.length);
            SceneManager.LoadScene("GameOver");
        }

        private IEnumerator Restart()
        { 
            yield return new WaitForSeconds(0.5f); 
            playerSprite.SetActive(true);
            player.transform.position = burrow.transform.position;
            FindFirstObjectByType<CatSpawner>().canSpawn = true;
            canLoseLife = true;
            m_playerInputProcessor.enabled = true;
        }
    }
}