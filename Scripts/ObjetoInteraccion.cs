using UnityEngine;

public class ObjetoInteraccion : MonoBehaviour {
    public ObjetoSO so;
    public bool accesible;
    public bool cerca;
    public int estado;

    SpriteRenderer sr;

    private void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();

        cerca = false;

        sr.sprite = so.estados[estado]; // estado default
    }

    public void PuedeInteraccionar(bool vf) {
        cerca = vf;
    }

    public void Interaccion() {
        // TODO: cada objeto tendra interacciones diferentes
    }
}