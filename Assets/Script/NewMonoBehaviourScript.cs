using System.Collections;
using UnityEngine;

public class FrogJump3D : MonoBehaviour
{
    [Header("Configurações de Salto")]
    public float moveDistance = 1.0f;     // Distância do salto na grelha
    public float jumpDuration = 0.15f;    // Duração do salto em segundos
    public float jumpHeight = 0.4f;       // Altura máxima do arco do salto

    private bool isJumping = false;

    void Update()
    {
        if (isJumping) return;

        // Movimento em 3D (Plano XZ)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(JumpRoutine(Vector3.forward, 0f));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(JumpRoutine(Vector3.back, 180f));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(JumpRoutine(Vector3.left, -90f));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(JumpRoutine(Vector3.right, 90f));
        }
    }

    IEnumerator JumpRoutine(Vector3 direction, float targetYRotation)
    {
        isJumping = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + (direction * moveDistance);

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / jumpDuration;

            // Interpola a posição no plano horizontal (X e Z)
            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);

            // Adiciona um arco parabólico no eixo Y para simular o salto no ar
            currentPos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = currentPos;

            // Roda suavemente o modelo 3D para a nova direção
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }

        // Garante que o sapo termina exatamente na posição e rotação finais
        transform.position = targetPosition;
        transform.rotation = targetRotation;
        isJumping = false;
    }
}