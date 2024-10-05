using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class MySceneManager : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
