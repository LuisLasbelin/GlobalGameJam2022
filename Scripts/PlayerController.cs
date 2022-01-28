using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputManager input;
    public float speed;
    private List<GameObject> _objetosEnRango = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Movimiento del jugador
        Vector3 movimiento = new Vector2(Input.GetAxis(input.horizontal), Input.GetAxis(input.vertical));

        transform.position += movimiento * Time.deltaTime * speed;
        #endregion
        #region Interaccionar con un objeto
        if(Input.GetAxis(input.interact) > 0) Interact();
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Cuando entra un objeto dentro del area de interaccion debe poder interactuar
        if(other.gameObject.GetComponent<ObjetoInteraccion>()) {
            _objetosEnRango.Add(other.gameObject);
        }
        MasCercano();
        Debug.Log("Nueva colision: " + other.gameObject.name);
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        // Una vez sale del rango de un objeto, lo elimina de su rango de interaccion
        if(_objetosEnRango.Contains(other.gameObject)){
            _objetosEnRango.Remove(other.gameObject);
            other.gameObject.GetComponent<ObjetoInteraccion>().PuedeInteraccionar(false);
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
        if(_objetosEnRango.Count < 1) {
            return null;
        }
        // Calcula las distancias para encontrar el objeto mas cercano
        GameObject _masCercano = _objetosEnRango[0];
        float _distanciaMenor = Vector2.Distance(_objetosEnRango[0].transform.position, transform.position);
        foreach (GameObject _objeto in _objetosEnRango)
        {
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
        // Calcula el objeto más cercano dentro de su rango e interacciona con él
        GameObject _objeto = MasCercano();
        if(_objeto == null) {
            return false;
        }
        // interaccion del objeto
        _objeto.GetComponent<ObjetoInteraccion>().Interaccion();
        return true; // interaccion correcta
    }
}
