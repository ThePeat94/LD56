using Nidavellir.Input;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class GatherFood : MonoBehaviour
    {
        private InputProcessor m_inputProcessor;
        private GameObject m_apple;
        public GameObject m_currentPiece;
        public bool hasCurrentPiece = false;
        [SerializeField] private Resource m_foodResource;
        public GameObject m_applePiece;
        

        private void Start()
        {
            m_inputProcessor = FindObjectOfType<InputProcessor>();
            m_currentPiece.SetActive(false);
        }

        private void Update()
        {
            if (m_inputProcessor.InteractTriggered && m_apple != null && !hasCurrentPiece)
            {
                 m_apple.GetComponentInParent<AppleManager>().TakeBite(); 
                 m_currentPiece.SetActive(true);
                 hasCurrentPiece = true;
            } else if (m_inputProcessor.InteractTriggered && hasCurrentPiece)
            {
                DropCurrentPiece();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("collectable"))
            {
                m_apple = other.gameObject;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {      
            if (other.gameObject.CompareTag("burrow") && hasCurrentPiece)
            {
                hasCurrentPiece = false;
                m_currentPiece.SetActive(false);
                this.m_foodResource.ResourceController.Add(1);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("collectable"))
            {
                m_apple = null;
            }
        }

        public void RemoveCurrentPiece()
        {
            m_currentPiece.SetActive(false);
            hasCurrentPiece = false;
        }
        
        public void DropCurrentPiece()
        {
            if (hasCurrentPiece == false) return;
            m_currentPiece.SetActive(false);
            var applePiece = Instantiate(m_applePiece, m_currentPiece.transform.position, Quaternion.identity);
            applePiece.AddComponent<AppleManager>().pieces = 1;
            hasCurrentPiece = false;
        }
    }
}