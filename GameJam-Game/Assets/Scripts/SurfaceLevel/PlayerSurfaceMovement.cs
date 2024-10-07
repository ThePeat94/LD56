using Microsoft.Unity.VisualStudio.Editor;
using Nidavellir.Input;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Nidavellir
{
    public class PlayerSurfaceMovement : MonoBehaviour
    {
        public float speed = 5f;
        public float boostMultiplier = 5f;

        private Vector2 m_direction;

        private Rigidbody2D m_rb;
        private InputProcessor m_inputProcessor;

        public float boostDuration = 1f;
        public float boostCooldown = 5f;

        private float boostTimer = 0f;
        private float cooldownTimer = 0f;
        private bool isBoosting = false;

        public TMP_Text boostText;
        public Image boostImage;

        public SurfaceGameManager gameManager;

        public Animator spriteAnimator;

        private AudioSource audioSource;
        public AudioClip lifeLost;
        public AudioClip boost;
        public AudioClip boostFail;

        public GameObject sprite;

        void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            m_inputProcessor = GetComponent<InputProcessor>(); 
            boostImage.color = Color.white;
            boostText.text = "";
        }
 
        private void FixedUpdate()
        {
            var actualSpeed = speed;

            if (m_inputProcessor.IsBoosting && cooldownTimer <= 0f && !isBoosting)
            {
                isBoosting = true;
                GetComponent<GatherFood>().DropCurrentPiece();
                boostTimer = boostDuration;
                audioSource.clip = boost;
                audioSource.Play();   
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

            if (m_inputProcessor.Movement == Vector2.down)
            {
                spriteAnimator.SetInteger("direction", 0);
            }
            else if (m_inputProcessor.Movement == Vector2.up)
            {
                spriteAnimator.SetInteger("direction", 3);
            }
            else if (m_inputProcessor.Movement == Vector2.left)
            {
                spriteAnimator.SetInteger("direction", 1);
            }
            else if (m_inputProcessor.Movement == Vector2.right)
            {
                spriteAnimator.SetInteger("direction", 2);
            }
            else if (m_inputProcessor.Movement == Vector2.zero)
            {
                spriteAnimator.SetInteger("direction", 5);
            }

            if (cooldownTimer > 0f)
            {
                if (m_inputProcessor.IsBoosting)
                {
                    boostImage.color = Color.black;
                    boostText.color = Color.red; 
                    audioSource.PlayOneShot(boostFail, 0.1f); 
                }
                else
                {
                    boostImage.color = Color.black;
                    boostText.color = Color.white;
                    boostText.fontStyle = FontStyles.Normal;
                }

                boostText.text = cooldownTimer.ToString("0");
                cooldownTimer -= Time.fixedDeltaTime;
            }
            else
            {
                boostImage.color = Color.white;
                boostText.text = "";
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "cat")
            {
                Destroy(other.gameObject.transform.root.gameObject);
                FindFirstObjectByType<CatSpawner>().canSpawn = false;
                sprite.SetActive(false);
                m_inputProcessor.enabled = false;
                gameManager.LooseLife();
                audioSource.clip = lifeLost;
                audioSource.Play();
                GetComponent<GatherFood>().RemoveCurrentPiece();
            }
        }
    }
}