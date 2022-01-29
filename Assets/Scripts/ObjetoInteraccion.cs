using UnityEngine;

public class ObjetoInteraccion : MonoBehaviour {
    public ObjetoSO so;
    public GameObject spriteObj;
    public bool accesible;
    public bool cerca;
    public int estado;
    public GameObject exclamacion;
    SpriteRenderer sr;
    GameManager manager;

    private void Start() {
        sr = spriteObj.GetComponent<SpriteRenderer>();

        cerca = false;

        sr.sprite = so.estados[estado]; // estado default

        exclamacion.SetActive(false);

        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void PuedeInteraccionar(bool vf) {
        cerca = vf;
        exclamacion.SetActive(vf);
    }

    public void Interaccion() {
        switch (so.tipoObjeto)
        {
            case ObjetoSO.tipoObjetoEnum.recoger:
                RecogerObjeto();
                break;
            case ObjetoSO.tipoObjetoEnum.uso:
                ActivarObjeto();
                break;
            case ObjetoSO.tipoObjetoEnum.dialogo:
                DialogoObjeto();
                break;
            default:
                break;
        }
    }

    public void Mostrar(bool _estado) {
        spriteObj.SetActive(_estado);
    }

    public void RecogerObjeto() {
        manager.recogerObjeto(this);
        Debug.Log("Objeto recogido: " + gameObject.name);
    }

    public void ActivarObjeto() {
        manager.usarObjeto(this, so);
        Debug.Log("Objeto activado: " + gameObject.name);
    }

    public void DialogoObjeto() {

    }
}