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
        
        public float boostDuration = 1f; // How long boost lasts
        public float boostCooldown = 5f; // Cooldown after boosting

        private float boostTimer = 0f; // Timer for managing boost duration
        private float cooldownTimer = 0f; // Timer for managing boost cooldown
        private bool isBoosting = false; // State flag to check if player is boosting

        void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_inputProcessor = GetComponent<InputProcessor>();
            blood.SetActive(false);
        }

        private void FixedUpdate()
        {
            var actualSpeed = speed;
         
            // Check if boosting
            if (m_inputProcessor.IsBoosting && cooldownTimer <= 0f && !isBoosting)
            {
                // Start boosting if cooldown is over and not already boosting
                isBoosting = true;
                boostTimer = boostDuration; // Reset the boost timer
            }

            // If player is boosting, use the boost speed and reduce boostTimer
            if (isBoosting)
            {
                actualSpeed = boostMultiplier * speed;
                boostTimer -= Time.fixedDeltaTime;

                // If boost duration is over, stop boosting and start cooldown
                if (boostTimer <= 0f)
                {
                    isBoosting = false;
                    cooldownTimer = boostCooldown; // Start cooldown
                }
            }
            
            if (m_inputProcessor.IsBoosting && cooldownTimer <= 0f && !isBoosting)
            {
               actualSpeed = boostMultiplier * speed;
               
            }
            
            m_rb.MovePosition(m_rb.position + m_inputProcessor.Movement * (actualSpeed * Time.fixedDeltaTime));
            
            // Reduce cooldown timer if it's greater than 0
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.fixedDeltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "cat")
            {
                blood.SetActive(true);
                m_inputProcessor.enabled = false;
            }
        }
    }
}