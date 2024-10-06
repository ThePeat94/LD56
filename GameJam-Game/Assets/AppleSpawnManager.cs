using UnityEngine;

namespace Nidavellir
{
    public class AppleSpawnManager : MonoBehaviour
    {
        private AppleSpawnPointManager[] _appleSpawner;
        public GameObject applePrefab;

        private bool shouldSpawn = true;

        private int lastNumber;

        void Start()
        {
            var _appleSpawnPrefabObjects = GameObject.FindGameObjectsWithTag("spawner");

            _appleSpawner = new AppleSpawnPointManager[_appleSpawnPrefabObjects.Length];

            for (int i = 0; i < _appleSpawnPrefabObjects.Length; i++)
            {
                _appleSpawner[i] = _appleSpawnPrefabObjects[i].GetComponent<AppleSpawnPointManager>();
            }

            var random = Random.Range(0, _appleSpawner.Length);
            lastNumber = random;
            _appleSpawner[random].SpawnApple(applePrefab);
            shouldSpawn = false;
        }

        // Update is called once per frame
        void Update()
        {
            shouldSpawn = true;
            for (int i = 0; i < _appleSpawner.Length; i++)
            {
                if (_appleSpawner[i].hasApple()) shouldSpawn = false;
            }

            if (shouldSpawn)
            {
                var random = Random.Range(0, _appleSpawner.Length);
                if (random != lastNumber)
                {
                    lastNumber = random;
                    _appleSpawner[random].SpawnApple(applePrefab);
                    shouldSpawn = false;
                }
            }
        }
    }
}