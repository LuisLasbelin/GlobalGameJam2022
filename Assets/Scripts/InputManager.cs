using UnityEngine;

[CreateAssetMenu(fileName = "InputManager", menuName = "TimeBox/InputManager", order = 0)]
public class InputManager : ScriptableObject {
    public string horizontal;
    public string vertical;
    public string interact;
    public string pause;
    public string timeTravel;
    public string usarInventario;
    public string exit;
}