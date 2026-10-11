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
        //Definir Temporariamente cursor
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

        //Criação Quaternions
        Quaternion yaw = Quaternion.Euler(0, inputRaw.x, 0);
        Quaternion pitch = Quaternion.Euler(-inputRaw.y, 0, 0);


        //Translacao Input
        //Ordem de Aplicacao de Rotacao
        playerCamera.transform.rotation = yaw * playerCamera.transform.rotation * pitch;

    }

}