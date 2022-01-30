using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject inventario = null;
    public Image inventarioUi;
    public Sprite spriteVacio;
    public GameObject personaje;
    public InputManager input;
    public int pasadoInd;
    Scene pasadoScene;
    public int presenteInd;
    Scene presenteScene;
    public bool presenteActivo = true;
    public Animator fundidoAnim;
    public ObjetoInteraccion puerta;
    public string creditsScene;
    public GameObject bocadillo;
    public TextMeshProUGUI displayText;

    void Start() {
        pasadoScene = SceneManager.GetSceneByBuildIndex(pasadoInd);
        presenteScene = SceneManager.GetSceneByBuildIndex(presenteInd);
        presenteActivo = true;
        //desactivarObjetos(pasadoScene);
    }

    #region Salto temporal
    public void SaltoTemporal(Transform portalSalida)
    {
        if (inventario != null)
        {

            GameObject objeto = inventario;

            if (!presenteActivo)
            {
                SceneManager.MoveGameObjectToScene(objeto, presenteScene);
            }
            else
            {
                SceneManager.MoveGameObjectToScene(objeto, pasadoScene);
            
            }
        }

        fundidoAnim.SetTrigger("Next");

        PlayerController playerController = personaje.GetComponent<PlayerController>();
        playerController.Viaje(portalSalida);

        // Pasado_Test
        // Presente_Test
        /*
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
        */
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

            // esta mierda es larguisima, pero basicamente recoge el sprite del objeto que
            // llevas en el inventario
            Sprite _sprite = inventario.GetComponent<ObjetoInteraccion>().spriteObj.GetComponent<SpriteRenderer>().sprite;
            inventarioUi.sprite = _sprite;
        }
        // Si ya tiene algo en el inventario
        else
        {
            Debug.Log("Tienes el inventario lleno");
            soltarObjeto();

            inventario = _objetoInteraccion.gameObject;
            inventario.transform.position = new Vector2(999,999);

            _objetoInteraccion.Mostrar(false);
            // esta mierda es larguisima, pero basicamente recoge el sprite del objeto que
            // llevas en el inventario
            Sprite _sprite = inventario.GetComponent<ObjetoInteraccion>().spriteObj.GetComponent<SpriteRenderer>().sprite;
            inventarioUi.sprite = _sprite;
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
        
            LimpiarInventario();
        }
    }

    public void activarDialogos(DialogManager dialogos)
    {
        GameManager gameManager = GetComponent<GameManager>();
        dialogos.EmpezarDialogo(gameManager);
    }

    public void usarObjeto(ObjetoInteraccion _objetoInteraccion, ObjetoSO objetivo, DialogManager dialogos)
    {
        if(objetivo.tiene_usos)
        {
            if(objetivo.numero_de_usos > _objetoInteraccion.usos)
            {
                Debug.Log("Hola buenos dias");
                activarUso(_objetoInteraccion, objetivo, dialogos);
            }
        }
        else
        {
            activarUso(_objetoInteraccion, objetivo, dialogos);
        }
    }
    public void activarUso(ObjetoInteraccion _objetoInteraccion, ObjetoSO objetivo, DialogManager dialogos)
    {
        Debug.Log("Interaccion con " + _objetoInteraccion.gameObject.name);
        if (objetivo.activable)
        {
            // Si tienes un objeto en el inventario
            if (inventario != null)
            {

                ObjetoSO _invSo = inventario.GetComponent<ObjetoInteraccion>().so;
                if (objetivo.objetoNecesario.Equals(_invSo.objeto))
                {
                    // Resultado del objeto del inventario
                    switch (_invSo.tipoUso)
                    {
                        case ObjetoSO.tipoUsoEnum.cambioEstado:
                            _objetoInteraccion.AumentarEstado();
                            SumarUso(_objetoInteraccion);
                            break;
                        case ObjetoSO.tipoUsoEnum.consumible:
                            Debug.Log("Hey escucha");
                            // Elimina la referencia del objeto llevado
                            LimpiarInventario();
                            SumarUso(_objetoInteraccion);
                            _objetoInteraccion.AumentarEstado();
                            break;
                        case ObjetoSO.tipoUsoEnum.estatico:
                            SumarUso(_objetoInteraccion);
                            break;
                        default:
                            break;
                    }
                    // Resultado del objetivo
                    if (objetivo.contenidoSpawn != null)
                    {
                        Instantiate(objetivo.contenidoSpawn, personaje.transform.position, Quaternion.identity);
                    }

                    Debug.Log(objetivo.objeto.ToString());

                    if (objetivo.objeto == ObjetoSO.objetoEnum.fuego)
                    {

                        _objetoInteraccion.transform.parent.transform.position = new Vector2(2000, 2000);
                    }
                    Debug.Log("Objeto activado");
                }
                else
                {
                    activarDialogos(dialogos);
                    Debug.Log("No tienes el objeto necesario");
                }

                Debug.Log("Activado");
            }
            // Si no tienes objeto en el inventario
            else if(objetivo.objetoNecesario == ObjetoSO.objetoEnum.Ninguno){
                
                // Resultado del objetivo
                if (objetivo.contenidoSpawn != null)
                {
                    Instantiate(objetivo.contenidoSpawn, personaje.transform.position, Quaternion.identity);
                    SumarUso(_objetoInteraccion);
                }
            }
            else if(objetivo.objetoNecesario != ObjetoSO.objetoEnum.Ninguno)
            {
                activarDialogos(dialogos);
                Debug.Log("No tienes el objeto necesario");
            }
        }

        // Lector
        if(objetivo.objeto == ObjetoSO.objetoEnum.lector) {
            if(_objetoInteraccion.estado >= 2){
                AbrirPuerta();
            }
        }
        // Puerta
        if(objetivo.objeto == ObjetoSO.objetoEnum.puerta) {
            Endgame();
        }
    }

    public void LimpiarInventario() {
        // Elimina la referencia del objeto llevado
        inventario = null;
        inventarioUi.sprite = spriteVacio;
    }

    private void SumarUso(ObjetoInteraccion _objetoInteraccion) {
        _objetoInteraccion.usos = _objetoInteraccion.usos + 1;
        _objetoInteraccion.Animacion();
    }

    public void AbrirPuerta() {
        puerta.accesible = true;
        puerta.Animacion();
        Debug.Log("Puerta abierta!");
    }

    public void Endgame() {
        SceneManager.LoadScene(creditsScene, LoadSceneMode.Single);
    }
}
