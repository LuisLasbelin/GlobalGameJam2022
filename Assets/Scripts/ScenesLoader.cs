using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public string Presente1, Pasado1, ManagerScene, Menu, Credits;
    public void StartGame() {
        Object.DontDestroyOnLoad(gameObject);
        
        SceneManager.LoadScene(ManagerScene, LoadSceneMode.Single);
        SceneManager.LoadScene(Presente1, LoadSceneMode.Additive);
        SceneManager.LoadScene(Pasado1, LoadSceneMode.Additive);
    }

    public void VolverMenu() {
        SceneManager.LoadScene(Menu, LoadSceneMode.Single);
        Debug.Log("Menu");
    }

    public void Endgame() {
        SceneManager.LoadScene(Credits, LoadSceneMode.Single);
    }

    public void Exit() {
        Application.Quit();
    }
}
