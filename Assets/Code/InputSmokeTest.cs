using UnityEngine;
using UnityEngine.InputSystem;

public class InputSmokeTest : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("NEW INPUT SYSTEM SPACE DETECTED");
        }
    }
}
