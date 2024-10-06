using UnityEngine;

namespace Nidavellir
{
    public class AppleSpawnPointManager : MonoBehaviour
    {
        private GameObject currentApple;

        public bool hasApple()
        {
            return currentApple != null;
        }

        public void SpawnApple(GameObject apple)
        {
            if (currentApple == null)
            {
                currentApple = Instantiate(apple, transform.position, Quaternion.identity);
            }
        }
    }
}