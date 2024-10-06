using System.Collections;
using Nidavellir.Input;
using Nidavellir.Scriptables;
using TMPro;
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
        public GameObject blood;
        public TMP_Text lifeText;
        [SerializeField] private Resource resource;

        private bool canLoseLife = true;
        public InputProcessor m_playerInputProcessor;

        void Start()
        {
            currentLife = maxLifeCount;
            foreach (var points in lifeUI)
            {
                points.GetComponent<Image>().enabled = true;
            }

            lifeText.enabled = false;
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
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                lifeUI[currentLife - 1].GetComponent<Image>().color = Color.black;
                StartCoroutine(Restart());
            }
        }

        private IEnumerator Restart()
        {
            lifeText.enabled = true;
            lifeText.text = "3";
            yield return new WaitForSeconds(0.5f);
            lifeText.text = "2";
            yield return new WaitForSeconds(0.5f);
            lifeText.text = "1";
            yield return new WaitForSeconds(0.5f);
            lifeText.enabled = false;
            blood.SetActive(false);
            FindFirstObjectByType<CatSpawner>().canSpawn = true;
            canLoseLife = true;
            m_playerInputProcessor.enabled = true;
        }
    }
}