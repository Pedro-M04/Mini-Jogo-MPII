using UnityEngine;

public class CameraFollow3D : MonoBehaviour
{
    [Header("Alvo")]
    public Transform player;                 // O transform do sapo

    [Header("Configurações de Posição")]
    public Vector3 offset = new Vector3(0f, 10f, -7f); // Distância da câmara em relação ao sapo
    public float smoothSpeed = 5f;           // Velocidade de seguimento (quanto maior, mais direto é o movimento)

    [Header("Rotação Fixa")]
    public float cameraAngleX = 45f;         // Ângulo de inclinação para baixo

    void Start()
    {
        // Define a rotação fixa da câmara logo no início para dar a perspetiva perfeita
        transform.rotation = Quaternion.Euler(cameraAngleX, 0f, 0f);
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calcula a posição desejada da câmara com base no sapo
        Vector3 desiredPosition = player.position + offset;

        // Move a câmara suavemente até à posição desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Atualiza a posição da câmara
        transform.position = smoothedPosition;
    }
}