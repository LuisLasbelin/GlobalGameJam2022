using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public string Presente1, Pasado1, ManagerScene;
    public void StartGame() {
        Object.DontDestroyOnLoad(gameObject);
        
        SceneManager.LoadScene(ManagerScene, LoadSceneMode.Single);
        SceneManager.LoadScene(Presente1, LoadSceneMode.Additive);
        SceneManager.LoadScene(Pasado1, LoadSceneMode.Additive);
    }
}
