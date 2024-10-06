using System;
using DG.Tweening;
using UnityEngine;

namespace Nidavellir.Enemy
{
    public class NpcMover : MonoBehaviour
    {
        [SerializeField] private float m_movementSpeed;
        
        private Vector3 m_target;
        
        public void StartMove(Vector3 target, Action moveComplete = null)
        {
            this.m_target = target;

            var sequence = DOTween.Sequence();
            sequence.Append(this.transform.DORotate(new Vector3(0, 0, 90), 0.5f));
            sequence.Append(this.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
            sequence.Append(this.transform.DORotate(new Vector3(0, 0, -90), 0.5f));
            sequence.Append(this.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
            sequence.SetLoops(-1);
            
            var timeToTravel = Vector3.Distance(this.transform.position, this.m_target) / this.m_movementSpeed;
            this.transform.DOMove(this.m_target, timeToTravel).SetEase(Ease.Linear).OnComplete(() =>
            {
                this.transform.position = this.m_target; 
                sequence.Complete();
                sequence.Kill();
                moveComplete?.Invoke();
            });
        }
    }
}