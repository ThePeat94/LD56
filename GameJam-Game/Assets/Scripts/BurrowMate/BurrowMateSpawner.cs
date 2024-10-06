using System;
using System.Collections;
using System.Collections.Generic;
using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.EventBinding;
using Nidavellir.GameEventBus.Events;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir.Enemy
{
    public class BurrowMateSpawner : MonoBehaviour
    {
        [SerializeField] private BurrowMate m_burrowMatePrefab;
        [SerializeField] private int m_minBurrowMateCount;
        [SerializeField] private int m_maxBurrowMateCount;
        [SerializeField] private Transform m_spawnPoint;

        [SerializeField] private int m_minFrameSpawnTime;
        [SerializeField] private int m_maxFrameSpawnTime;
        [SerializeField] private int m_initialFrameSpawnTime;
        [SerializeField] private int m_frameCooldownBetweenSpawns;
        
        [SerializeField] private Transform[] m_burrowMateEndPoints;
        [SerializeField] private Transform m_entrancePoint;
        
        
        private BurrowMateGroup m_currentBurrowMateGroup;
        
        private int m_currentFrameSpawnTime;
        private int m_frameSpawnCooldown;
        private int m_delaySpawnCooldown;
        
        private Coroutine m_spawnRoutine;
        
        private IEventBinding<BurrowMateGroupAcceptedEvent> m_burrowMateGroupAcceptedEventBinding;
        private IEventBinding<BurrowMateGroupDeclinedEvent> m_burrowMateGroupDeclinedEventBinding;
        private void Start()
        {
            this.m_frameSpawnCooldown = UnityEngine.Random.Range(this.m_minFrameSpawnTime, this.m_maxFrameSpawnTime + 1);
            
            this.m_burrowMateGroupAcceptedEventBinding = new EventBinding<BurrowMateGroupAcceptedEvent>(this.OnBurrowMateGroupAccepted);
            this.m_burrowMateGroupDeclinedEventBinding = new EventBinding<BurrowMateGroupDeclinedEvent>(this.OnBurrowMateGroupDeclined);
            
            GameEventBus<BurrowMateGroupAcceptedEvent>.Register(this.m_burrowMateGroupAcceptedEventBinding);
            GameEventBus<BurrowMateGroupDeclinedEvent>.Register(this.m_burrowMateGroupDeclinedEventBinding);
        }

        private void OnBurrowMateGroupDeclined(object sender, BurrowMateGroupDeclinedEvent e)
        {
            foreach (var burrowMate in this.m_currentBurrowMateGroup.BurrowMates)
            {
                burrowMate.GetComponent<NpcMover>().StartMove(this.m_spawnPoint.position, () =>
                {
                    Destroy(burrowMate.gameObject);
                });
            }
            this.m_currentBurrowMateGroup = null;
        }

        private void OnBurrowMateGroupAccepted(object sender, BurrowMateGroupAcceptedEvent e)
        {
            foreach (var burrowMate in this.m_currentBurrowMateGroup.BurrowMates)
            {
                burrowMate.GetComponent<NpcMover>().StartMove(this.m_entrancePoint.position, () =>
                {
                    Destroy(burrowMate.gameObject);
                });
            }
            
            GameEventBus<BurrowMateGroupAddedEvent>.Invoke(this, new(this.m_currentBurrowMateGroup.BurrowMates.Count));
            this.m_currentBurrowMateGroup = null;
        }

        private void FixedUpdate()
        {
            if (this.m_spawnRoutine == null && this.m_currentBurrowMateGroup is null)
            {
                this.m_currentFrameSpawnTime++;
            }
            else if (this.m_currentBurrowMateGroup is not null)
            {
                this.m_delaySpawnCooldown--;
            }
            
            if (this.m_currentFrameSpawnTime >= this.m_frameSpawnCooldown && this.m_spawnRoutine == null)
            {
                this.m_spawnRoutine = StartCoroutine(this.SpawnBurrowMates());
            }
        }
        
        private IEnumerator SpawnBurrowMates()
        {
            this.m_currentFrameSpawnTime = 0;
            var burrowMateCount = UnityEngine.Random.Range(this.m_minBurrowMateCount, this.m_maxBurrowMateCount + 1);
            this.m_currentBurrowMateGroup = new BurrowMateGroup();
            for (var i = 0; i < burrowMateCount; i++)
            {
                this.m_delaySpawnCooldown = this.m_frameCooldownBetweenSpawns;
                while (this.m_delaySpawnCooldown > 0)
                {
                    yield return null;
                }
                var burrowMate = Instantiate(this.m_burrowMatePrefab, this.m_spawnPoint.position, Quaternion.identity);
                this.m_currentBurrowMateGroup.BurrowMates.Add(burrowMate);
                Action leaderCallback = null;
                if (i == 0)
                {
                    var leader = burrowMate.AddComponent<BurrowMateLeader>();
                    leader.CanBeInteractedWith = false;
                    leaderCallback = () =>
                    {
                        leader.CanBeInteractedWith = true;
                    };
                }
                
                var endPoint = this.m_burrowMateEndPoints[i];
                burrowMate.GetComponent<NpcMover>().StartMove(endPoint.position, leaderCallback);
            }

            this.m_currentFrameSpawnTime = 0;
            this.m_frameSpawnCooldown = UnityEngine.Random.Range(this.m_minFrameSpawnTime, this.m_maxFrameSpawnTime + 1);
            this.m_spawnRoutine = null;
        }

        private class BurrowMateGroup
        {
            public List<BurrowMate> BurrowMates { get; } = new();
        }
    }
}