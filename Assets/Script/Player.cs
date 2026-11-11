using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    private Transform player;
    public float moveDistance = 1.0f;
    public float jumpDuration = 0.15f;
    public float jumpHeight = 0.4f;
    private bool isJumping = false;

    [Header("Efeitos de Partículas")]
    public GameObject JumpParticles;    // Partículas ao saltar (partida)
    public GameObject LandingParticles; // Partículas ao aterrar (chegada)
    
    void Start()
    {
        player = GetComponent<Transform>();
    }

    void Update()
    {
        // Movimento:
        
        if (Input.anyKeyDown)
        {
            player.DORotate(new Vector3(0, 0, 360), 0.8f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutQuad);
        }

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

    // Corrontina do salto:
    IEnumerator JumpRoutine(Vector3 direction, float targetYRotation)
    {
        if (JumpParticles != null)
        {
            Instantiate(JumpParticles, transform.position, Quaternion.identity);
        }
        isJumping = true;

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

        transform.DOJump(transform.position + direction, 1f, 1, 0.2f);
        transform.position = targetPosition;
        isJumping = false;

        if (LandingParticles != null)
        {
            Instantiate(LandingParticles, transform.position, Quaternion.identity);
        }
    }
}