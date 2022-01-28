using UnityEngine;

public class ObjetoInteraccion : MonoBehaviour {
    public ObjetoSO so;
    public GameObject spriteObj;
    public bool accesible;
    public bool cerca;
    public int estado;
    public GameObject exclamacion;
    SpriteRenderer sr;

    private void Start() {
        sr = spriteObj.GetComponent<SpriteRenderer>();

        cerca = false;

        sr.sprite = so.estados[estado]; // estado default

        exclamacion.SetActive(false);
    }

    public void PuedeInteraccionar(bool vf) {
        cerca = vf;
        exclamacion.SetActive(vf);
    }

    public void Interaccion() {
        // TODO: cada objeto tendra interacciones diferentes
    }
}