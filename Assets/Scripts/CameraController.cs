using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;
    public float speed;
    public float limiteDistancia;
    private void Update() {
        if(Vector2.Distance(transform.position, player.position) > limiteDistancia) {
                    Vector2 _transicion = Vector2.Lerp(transform.position, player.position, speed * Time.deltaTime);
            transform.position = new Vector3(_transicion.x, _transicion.y, transform.position.z);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(player.position, limiteDistancia);
        Gizmos.color = Color.blue;
    }
}