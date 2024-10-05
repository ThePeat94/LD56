using Nidavellir.Input;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerSurfaceMovement : MonoBehaviour
    {
        public float speed = 5f;
        private Vector2 m_direction;
        
        private Rigidbody2D m_rb;
        private InputProcessor m_inputProcessor;
        
        void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_inputProcessor = GetComponent<InputProcessor>();
        }

        private void FixedUpdate()
        {
            m_rb.MovePosition(m_rb.position + m_inputProcessor.Movement * (speed * Time.fixedDeltaTime));
        }
    }
}
