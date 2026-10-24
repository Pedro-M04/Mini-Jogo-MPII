using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public static CameraPlayer current;
    public float turnSpeed = 0.2f;

    public Camera playerCamera;
    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        Turn();
    }

    void Turn()
    {
        Vector2 inputRaw = InputHandler.current.look.ReadValue<Vector2>();
        Vector3 input = new Vector3(-inputRaw.y, inputRaw.x, 0) * turnSpeed * Time.deltaTime;

        Quaternion yaw = Quaternion.Euler(0, inputRaw.x, 0);
        Quaternion pitch = Quaternion.Euler(-inputRaw.y, 0, 0);
        playerCamera.transform.rotation = yaw * playerCamera.transform.rotation * pitch;

    }

}