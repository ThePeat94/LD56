using System;
using Nidavellir.Input;
using UnityEngine;

namespace Nidavellir
{
    public class GatherFood : MonoBehaviour
    {
        private InputProcessor m_inputProcessor;
        private GameObject m_apple;
        public GameObject m_currentPiece;
        private bool hasCurrentPiece = false;

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
                // TODO: set ressources up
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("collectable"))
            {
                m_apple = null;
            }
        }
    }
}