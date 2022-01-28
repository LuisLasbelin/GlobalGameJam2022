using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltoTemporal : MonoBehaviour
{

    public GameObject jugador;
    public GameObject camara;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cambio();
        }
    }

    private void cambio()
    {
        // Pasado_Test
        // Presente_Test
        if (SceneManager.GetActiveScene().name == "Pasado_Test")
        {
            // Pillamos la escena activa
            Scene pasado = SceneManager.GetActiveScene();
            desactivarObjetos(pasado);

            // Pillamos la otra escena
            Scene presente = SceneManager.GetSceneByName("Presente_Test");
            activarObjetos(presente);

            // Activamos la camara y el jugador
            jugador.SetActive(true);
            camara.SetActive(true);

            // Los movemos de escena
            SceneManager.MoveGameObjectToScene(jugador, presente);
            SceneManager.MoveGameObjectToScene(camara, presente);
            
            // Cambiamos de escena
            SceneManager.SetActiveScene(presente);
        }
        else if(SceneManager.GetActiveScene().name == "Presente_Test")
        {
            // Pillamos la escena activa
            Scene presente = SceneManager.GetActiveScene();
            desactivarObjetos(presente);

            // Pillamos la otra escena
            Scene pasado = SceneManager.GetSceneByName("Pasado_Test");
            activarObjetos(pasado);

            // Activamos la camara y el jugador
            jugador.SetActive(true);
            camara.SetActive(true);

            // Los movemos de escena
            SceneManager.MoveGameObjectToScene(jugador, pasado);
            SceneManager.MoveGameObjectToScene(camara, pasado);

            // Cambiamos de escena
            SceneManager.SetActiveScene(pasado);
        }
    }

    public void desactivarObjetos(Scene tiempo)
    {
        // Recoge los objetos
        tiempo.GetRootGameObjects();

        GameObject[] ObjetoAOcultar = tiempo.GetRootGameObjects();

        // Los esconde
        foreach (GameObject objet in ObjetoAOcultar)
        {
            objet.SetActive(false);
        }
    }
    public void activarObjetos(Scene tiempo)
    {
        // Recoge los objetos
        tiempo.GetRootGameObjects();

        GameObject[] ObjetoAMostrar = tiempo.GetRootGameObjects();

        // Los muestra
        foreach (GameObject objet in ObjetoAMostrar)
        {
            objet.SetActive(true);
        }
    }
}
