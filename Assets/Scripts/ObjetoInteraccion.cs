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
        // TODO: cada objeto tendra interacciones diferentes
        
        // Objeto recoger
        if(so.tipoObjeto == ObjetoSO.tipoObjetoEnum.recoger)
        {
            manager.recogerObjeto(this);
            Debug.Log("Objeto recogido: " + gameObject.name);

        }else if(so.tipoObjeto == ObjetoSO.tipoObjetoEnum.activar)
        {
            GameObject manager = GameObject.FindWithTag("GameManager");
            manager.GetComponent<GameManager>().usarObjeto(this, so);
            Debug.Log("Objeto activado: " + gameObject.name);
        }
    }

    public void Mostrar(bool _estado) {
        spriteObj.SetActive(_estado);
    }
}