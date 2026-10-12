using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Configurações de Salto")]
    public float moveDistance = 1.0f;
    public float jumpDuration = 0.15f;
    public float jumpHeight = 0.4f;

    [Header("Áudio de Salto")]
    public AudioClip jumpSound;
    private AudioSource audioSource;

    private bool isJumping = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isJumping) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(JumpRoutine(Vector3.right, 90f));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(JumpRoutine(Vector3.left, -90f));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(JumpRoutine(Vector3.forward, 180f));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(JumpRoutine(Vector3.back, 0f));
        }
    }

    IEnumerator JumpRoutine(Vector3 direction, float targetYRotation)
    {
        isJumping = true;

        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + (direction * moveDistance);

        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / jumpDuration;

            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);

            currentPos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = currentPos;

            yield return null;
        }

        transform.position = targetPosition;
        isJumping = false;
    }
}