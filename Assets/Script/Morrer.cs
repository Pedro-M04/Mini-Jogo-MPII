using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Morrer : MonoBehaviour
{
    [Header("Morte")]
    public string obstacleTag = "Obstacle";
    public float deathDelay = 2.0f;

    [Header("Animação")]
    public float knockbackForce = 5f;
    public float upForce = 3f;
    public float rotationSpeed = 720f;

    [Header("Áudio")]
    public AudioClip deathSound;
    private AudioSource audioSource;

    private bool isDead = false;
    private Rigidbody rb;
    private Player moveScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveScript = GetComponent<Player>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.CompareTag(obstacleTag))
        {
            StartCoroutine(DieSequence());
        }
    }

    IEnumerator DieSequence()
    {
        isDead = true;
        Debug.Log("O Sapo Morreu!");

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        if (moveScript != null) moveScript.enabled = false;
        GetComponent<Collider>().enabled = false;

<<<<<<< Updated upstream
=======
        Camera.main.transform.DOShakePosition(0.4f, strength: 0.5f, vibrato: 14);

        // Roda loucamente e diminui a escala até desaparecer
        transform.DORotate(new Vector3(360, 720, 0), 0.5f, RotateMode.FastBeyond360);
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);

        // 3. Aplicar Física (Empurrão e Pulo)
>>>>>>> Stashed changes
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            Vector3 knockbackDir = (transform.position - rb.worldCenterOfMass).normalized;
            knockbackDir.y = 0;

            rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        }

        float timer = 0;
        float duration = deathDelay * 0.75f;

        while (timer < duration)
        {
            transform.Rotate(new Vector3(rotationSpeed, 0, rotationSpeed / 2f) * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(deathDelay * 0.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}