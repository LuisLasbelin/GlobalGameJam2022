using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputManager input;
    public float speed;
    public float actionRange;
    GameManager manager;
    private List<GameObject> _objetosEnRango = new List<GameObject>();
    private GameObject _masCercano;
    private Rigidbody2D rb;
    public Transform mainCamera;
    Animator animator;
    public bool parado = false;
    bool viajando = false;
    bool recogiendo = false;
    Transform portalSalida;
    public float maxParadoTimer;
    float paradoTimer;
    public AudioSource audioSource, recogerSource;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        parado = false;
        recogiendo = false;
        viajando = false;
        paradoTimer = maxParadoTimer;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movimiento del jugador
        if(!parado) {
            Vector3 movimiento = new Vector2(Input.GetAxisRaw(input.horizontal), Input.GetAxisRaw(input.vertical));

            rb.velocity = movimiento * speed;

            animator.SetFloat("Horizontal", movimiento.x);
            animator.SetFloat("Vertical", movimiento.y);
            animator.SetFloat("Speed", Vector3.Magnitude(rb.velocity));

            if(Vector3.Magnitude(rb.velocity) == 0) {
                audioSource.mute = true;
            } else {
                audioSource.mute = false;
            }
            
        } else {
            rb.velocity = Vector2.zero;
        }
        #endregion

        #region Interaccionar con un objeto
        if(Input.GetButtonDown(input.interact)) Interact();
        #endregion

        #region Usar objeto del inventario
        if(Input.GetButtonDown(input.usarInventario)) manager.soltarObjeto();
        #endregion

        if(Input.GetButtonDown(input.exit)) Application.Quit();

        Comprobarbjetos();

        #region Timer parado
        if(parado) {
            paradoTimer -= Time.deltaTime;
        }
        if(paradoTimer <= 0) {
            parado = false;
            paradoTimer = maxParadoTimer;
            if(viajando) {
                viajando = false;
                // Transporta la camara y el jugador a la otra sala
                transform.position = portalSalida.position;
                mainCamera.transform.position = new Vector3(portalSalida.position.x, portalSalida.position.y, mainCamera.transform.position.z);
            }
            if(recogiendo) {
                _masCercano.GetComponent<ObjetoInteraccion>().Interaccion();
                recogerSource.Play();
                recogiendo = false;
            }
        }
        #endregion
    }

    private void Comprobarbjetos() {
        _objetosEnRango.Clear();
        ObjetoInteraccion[] _objetos = GameObject.FindObjectsOfType<ObjetoInteraccion>();
        for (int i = 0; i < _objetos.Length; i++)
        {
            _objetos[i].PuedeInteraccionar(false);
            float _distancia = Vector2.Distance(transform.position, _objetos[i].transform.position);
            if(_distancia < actionRange) {
                _objetosEnRango.Add(_objetos[i].gameObject);
            }
        }

        MasCercano();
    }

    /**
     * MasCercano
     * 
     * Calcula el objeto interaccionable mas cercano para marcarlo 
     * 
     * @return GameObjet
     */
    private GameObject MasCercano() {
        if(_objetosEnRango.Count == 0) {
            _masCercano = null;
            return null;
        }
        // Calcula las distancias para encontrar el objeto mas cercano
        _masCercano = _objetosEnRango[0];
        float _distanciaMenor = Vector2.Distance(_objetosEnRango[0].transform.position, transform.position);
        foreach (GameObject _objeto in _objetosEnRango)
        {
            // Devuelve todos los objetos a estado falso
            _objeto.GetComponent<ObjetoInteraccion>().PuedeInteraccionar(false);
            // Averigua la distancia
            float _distancia = Vector2.Distance(_objeto.transform.position, transform.position);
            if(_distancia < _distanciaMenor) {
                _distanciaMenor = _distancia;
                _masCercano = _objeto;
            }
        }
        // _masCercano es el objeto a interaccionar o mostrar un indicador
        // TODO: mostrar indicador en el objeto
        ObjetoInteraccion oi = _masCercano.GetComponent<ObjetoInteraccion>();
        oi.PuedeInteraccionar(true);
        return _masCercano;
    }

    /**
     * Interact
     * 
     * Interaccion con objetos del escenario
     * 
     * @return bool
     */
    private bool Interact() {
        //Debug.Log("Boton Interaccion pulsado");
        // Calcula el objeto más cercano dentro de su rango e interacciona con él
        if(_masCercano == null) {
            return false;
        }
        //Debug.Log("Interacciona con: " + _masCercano.name);
        // interaccion del objeto
        bool _animacion = _masCercano.GetComponent<ObjetoInteraccion>().ComprobarAnimacion();
        // Animacion
        if(_animacion) {
            recogiendo = true;
            animator.SetTrigger("Pickup");
            parado = true;
        } else {
            _masCercano.GetComponent<ObjetoInteraccion>().Interaccion();
        }
        return true; // interaccion correcta
    }

    public void Viaje(Transform _portalSalida) {
        parado = true;
        viajando = true;
        portalSalida = _portalSalida;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, actionRange);
        Gizmos.color = Color.green;
    }
}
