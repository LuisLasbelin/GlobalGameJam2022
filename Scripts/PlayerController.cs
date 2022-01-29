using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputManager input;
    public float speed;
    public float actionRange;
    GameManager manager;
    [SerializeField]
    private List<GameObject> _objetosEnRango = new List<GameObject>();
    private GameObject _masCercano;
    private Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movimiento del jugador
        Vector3 movimiento = new Vector2(Input.GetAxis(input.horizontal), Input.GetAxis(input.vertical));

        rb.velocity = movimiento * speed;

        animator.SetInteger("Horizontal", Mathf.RoundToInt(movimiento.x));
        animator.SetInteger("Vertical", Mathf.RoundToInt(movimiento.y));
        #endregion

        #region Interaccionar con un objeto
        if(Input.GetButtonDown(input.interact)) Interact();
        #endregion

        #region Usar objeto del inventario
        if(Input.GetButtonDown(input.usarInventario)) manager.soltarObjeto();
        #endregion

        Comprobarbjetos();
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
        Debug.Log("Boton Interaccion pulsado");
        // Calcula el objeto más cercano dentro de su rango e interacciona con él
        if(_masCercano == null) {
            return false;
        }
        Debug.Log("Interacciona con: " + _masCercano.name);
        // interaccion del objeto
        _masCercano.GetComponent<ObjetoInteraccion>().Interaccion();
        // Animacion
        animator.SetTrigger("Pickup");
        return true; // interaccion correcta
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, actionRange);
        Gizmos.color = Color.green;
    }
}
