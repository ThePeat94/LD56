using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class AppleSpawnManager : MonoBehaviour
    {
        private AppleSpawnPointManager[] _appleSpawnPrefabs;
        
        // Start is called before the first frame update
        void Start()
        {
            var _appleSpawnPrefabObject = GameObject.FindGameObjectsWithTag("spawner");
            var i = 0;
            foreach (var applepoint in _appleSpawnPrefabObject)
            {
                _appleSpawnPrefabs[i] = applepoint.GetComponent<AppleSpawnPointManager>();
                i++;
            }
        }

        // Update is called once per frame
        // void Update()
        // {
        //     foreach (var spawner in _appleSpawnPrefabs)
        //     {
        //         spawner.
        //     }
        // }
    }
}
