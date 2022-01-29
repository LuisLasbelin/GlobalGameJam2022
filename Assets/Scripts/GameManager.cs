using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject inventario = null;
    public GameObject personaje;
    public InputManager input;
    public int pasadoInd;
    Scene pasadoScene;
    public int presenteInd;
    Scene presenteScene;
    public bool presenteActivo = true;
    GameManager gameManager;

    void Start() {
        pasadoScene = SceneManager.GetSceneByBuildIndex(pasadoInd);
        presenteScene = SceneManager.GetSceneByBuildIndex(presenteInd);
        presenteActivo = true;
        desactivarObjetos(pasadoScene);

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    #region Salto temporal
    public void SaltoTemporal()
    {
        if (gameManager.inventario != null)
        {

            GameObject objeto = gameManager.inventario;

            if (!presenteActivo)
            {
                SceneManager.MoveGameObjectToScene(objeto, presenteScene);
            }
            else
            {
                SceneManager.MoveGameObjectToScene(objeto, pasadoScene);
            
            }
        }

        // Pasado_Test
        // Presente_Test
        if (!presenteActivo)
        {
            // Pillamos la escena activa
            desactivarObjetos(pasadoScene);

            // Pillamos la otra escena
            activarObjetos(presenteScene);

            // Cambiamos de escena
            SceneManager.SetActiveScene(presenteScene);
            presenteActivo = !presenteActivo;
        }
        else if(presenteActivo)
        {
            // Pillamos la escena activa
            desactivarObjetos(presenteScene);

            // Pillamos la otra escena
            activarObjetos(pasadoScene);
            
            // Cambiamos de escena
            SceneManager.SetActiveScene(pasadoScene);
            presenteActivo = !presenteActivo;
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
    #endregion

    public void recogerObjeto(ObjetoInteraccion _objetoInteraccion)
    {
        // Si tiene el inventario vacio
        if(inventario == null)
        {
            inventario = _objetoInteraccion.gameObject;
            inventario.transform.position = new Vector2(999,999);
            
            _objetoInteraccion.Mostrar(false);
            //objeto.GetComponent<ObjetoInteraccion>().enabled = false;
        }
        // Si ya tiene algo en el inventario
        else
        {
            Debug.Log("Tienes el inventario lleno");
            soltarObjeto();

            inventario = _objetoInteraccion.gameObject;
            inventario.transform.position = new Vector2(999,999);

            _objetoInteraccion.Mostrar(false);
            //objeto.GetComponent<ObjetoInteraccion>().enabled = false;
        }
    }

    public void soltarObjeto()
    {
        if (inventario != null)
        {
            Debug.Log(inventario.name + " soltado!");
            inventario.transform.position = personaje.transform.position;
            inventario.GetComponent<ObjetoInteraccion>().Mostrar(true);
            //objeto.GetComponent<ObjetoInteraccion>().enabled = true;
        
            inventario = null;
        }
    }

    public void usarObjeto(ObjetoInteraccion _objetoInteraccion, ObjetoSO objetivo)
    {
        if (objetivo.activable)
        {
            ObjetoSO _invSo = inventario.GetComponent<ObjetoInteraccion>().so;
            if(objetivo.objetoNecesario.Equals(_invSo.objeto))
            {
                // Resultado del objeto del inventario
                switch (_invSo.tipoUso)
                {
                    case ObjetoSO.tipoUsoEnum.cambioEstado:
                        _invSo.estadoActual = 1;
                        break;
                    case ObjetoSO.tipoUsoEnum.consumible:
                        inventario = null;
                        break;
                    case ObjetoSO.tipoUsoEnum.estatico:
                        break;
                    default:
                        break;
                }
                // Resultado del objetivo
                if(objetivo.contenidoSpawn != null) {
                    Instantiate(objetivo.contenidoSpawn, personaje.transform.position, Quaternion.identity);
                }

                Debug.Log("Activado");
            }
            else
            {
                Debug.Log("No tienes el objeto necesario");
            }
        }
        else 
        {
            Debug.Log("Activado");
        }
    }
}