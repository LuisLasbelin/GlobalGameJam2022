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
    public Transform portalSalida;

    private void Awake() {
        sr = spriteObj.GetComponent<SpriteRenderer>();

        cerca = false;

        sr.sprite = so.estados[estado]; // estado default
        
        // En caso de que la animacion ya se haya activado antes
        // Animacion Activated
        Animator _anim = spriteObj.GetComponent<Animator>();
        if(_anim != null) {
            _anim.SetInteger("Estado", estado);
        }

        exclamacion.SetActive(false);

        manager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void PuedeInteraccionar(bool vf) {
        cerca = vf;
        exclamacion.SetActive(vf);
    }

    public bool Interaccion() {
        switch (so.tipoObjeto)
        {
            case ObjetoSO.tipoObjetoEnum.recoger:
                RecogerObjeto();
                return true;
            case ObjetoSO.tipoObjetoEnum.uso:
                ActivarObjeto();
                return true;
            case ObjetoSO.tipoObjetoEnum.dialogo:
                DialogoObjeto();
                return true;
            case ObjetoSO.tipoObjetoEnum.portal:
                manager.SaltoTemporal(portalSalida);
                return false;
            default:
                return false;
        }
    }

    public void Mostrar(bool _estado) {
        spriteObj.SetActive(_estado);
    }

    public void RecogerObjeto() {
        manager.recogerObjeto(this);
        //Debug.Log("Objeto recogido: " + gameObject.name);
    }

    public void ActivarObjeto() {
        manager.usarObjeto(this, so);
        Debug.Log("Objeto activado: " + gameObject.name);
        // Animacion Activate
        Animator _anim = spriteObj.GetComponent<Animator>();
        if(_anim != null) {
            _anim.SetTrigger("Activate");
            
            estado = 2;
            _anim.SetInteger("Estado", estado);
        }
    }

    public void DialogoObjeto() {

    }
}