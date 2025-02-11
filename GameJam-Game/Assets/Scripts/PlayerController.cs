﻿using Nidavellir.Input;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    [RequireComponent(typeof(CharacterController), typeof(InputProcessor))]
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController s_instance;

        [SerializeField] private PlayerData m_playerData;

        private Vector3 m_moveDirection;
        private CharacterController m_characterController;
        private InputProcessor m_inputProcessor;


        public static PlayerController Instance => s_instance;
    
    
        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_characterController = this.GetComponent<CharacterController>();
        }
    
        // Update is called once per frame
        void Update()
        {
            this.Move();
        }
    
        protected void Move()
        {
            this.m_moveDirection = new Vector3(this.m_inputProcessor.Movement.x, Physics.gravity.y, 0f);
            this.m_characterController.Move(this.m_moveDirection * Time.deltaTime * this.m_playerData.MovementSpeed);
        }
        
        private void Rotate()
        {
            var targetDir = this.m_moveDirection;
            targetDir.y = 0f;

            if (targetDir == Vector3.zero)
                targetDir = this.transform.forward;
    
            this.RotateTowards(targetDir);
        }

        private void RotateTowards(Vector3 dir)
        {
            var lookRotation = Quaternion.LookRotation(dir.normalized);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, this.m_playerData.RotationSpeed * Time.deltaTime);
        }
    }
}
