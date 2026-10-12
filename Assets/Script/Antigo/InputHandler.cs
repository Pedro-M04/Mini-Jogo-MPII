using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler current;

    [Header("Input Actions")]
    public InputAction move;
    public InputAction look;
    public InputAction interact;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        move.Enable();
        look.Enable();
        interact.Enable();
    }
}