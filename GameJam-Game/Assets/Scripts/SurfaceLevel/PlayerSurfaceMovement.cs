using System;
using Nidavellir.Input;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerSurfaceMovement : MonoBehaviour
    {
        public float speed = 5f;
        public float boostMultiplier = 5f;
        
        private Vector2 m_direction;

        private Rigidbody2D m_rb;
        private InputProcessor m_inputProcessor;

        public GameObject blood;
        
        public float boostDuration = 1f;
        public float boostCooldown = 5f; 

        private float boostTimer = 0f;
        private float cooldownTimer = 0f; 
        private bool isBoosting = false; 
        
        public SurfaceGameManager gameManager;

        void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_inputProcessor = GetComponent<InputProcessor>();
            blood.SetActive(false);
        }

        private void FixedUpdate()
        {
            var actualSpeed = speed;
         
            if (m_inputProcessor.IsBoosting && cooldownTimer <= 0f && !isBoosting)
            {
                isBoosting = true;
                boostTimer = boostDuration;  
            }

            if (isBoosting)
            {
                actualSpeed = boostMultiplier * speed;
                boostTimer -= Time.fixedDeltaTime;

                if (boostTimer <= 0f)
                {
                    isBoosting = false;
                    cooldownTimer = boostCooldown;  
                }
            }
            
            if (m_inputProcessor.IsBoosting && cooldownTimer <= 0f && !isBoosting)
            {
               actualSpeed = boostMultiplier * speed;
               
            }
            
            m_rb.MovePosition(m_rb.position + m_inputProcessor.Movement * (actualSpeed * Time.fixedDeltaTime));
            
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.fixedDeltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "cat")
            { 
                Destroy(other.gameObject);
                FindFirstObjectByType<CatSpawner>().canSpawn = false;
                blood.SetActive(true);
                m_inputProcessor.enabled = false;
                gameManager.LooseLife();
            }
        }
    }
}