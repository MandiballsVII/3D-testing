using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public InputActionAsset actions;

    void OnEnable()
    {
        Debug.Log("OnEnable called");

        var map = actions.FindActionMap("Gameplay", true);
        map.Enable();

        // ESTO ES LO QUE FALTABA
        map.devices = new[] { Keyboard.current };

        var move = map.FindAction("Move", true);
        var jump = map.FindAction("Jump", true);

        move.performed += ctx => Debug.Log("MOVE: " + ctx.ReadValue<Vector2>());
        jump.performed += _ => Debug.Log("JUMP");
    }
}
