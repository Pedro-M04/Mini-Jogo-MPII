using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo")]
    public Transform player;

    [Header("Configurações")]
    public Vector3 offset = new Vector3(0f, 10f, -7f);
    public float smoothSpeed = 5f;

    [Header("Rotação")]
    public float cameraAngleX = 45f;
    public float cameraAngleY = 90f;
    public float cameraAngleZ = 0f;
    public float rotationAmount = 30f;         
    public float rotationSpeed = 5f; 
    private float currentRotationOffset = 0f;


    void LateUpdate()
    {
        if (player == null) return;

        float targetRotationOffset = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            targetRotationOffset = -rotationAmount;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            targetRotationOffset = rotationAmount;
        }


        currentRotationOffset = Mathf.Lerp(currentRotationOffset, targetRotationOffset, rotationSpeed * Time.deltaTime);

        float finalAngleY = cameraAngleY + currentRotationOffset;
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(cameraAngleX, finalAngleY, cameraAngleZ);
        transform.position = smoothedPosition;


    }
}