using UnityEngine;
using UnityEngine.SceneManagement;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "Scene loader", menuName = "Manager/Scene loader", order = 0)]
    public class SceneLoader : ScriptableObject
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}