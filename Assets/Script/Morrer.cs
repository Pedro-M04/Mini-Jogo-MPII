using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Morrer : MonoBehaviour // Substitua pelo nome real da sua classe se for diferente
{
    [Header("Configurações de Morte")]
    public string obstacleTag = "Obstacle";    // Tag usada nos carros
    public float deathDelay = 2.0f;           // Tempo antes de reiniciar

    [Header("Animação de Morte")]
    public float knockbackForce = 5f;         // Força do empurrão para trás
    public float upForce = 3f;                // Força do pulo para cima na morte
    public float rotationSpeed = 720f;        // Velocidade da rotação (graus por segundo)

    [Header("Áudio de Morte")]
    public AudioClip deathSound;              // Arquivo de som da morte
    private AudioSource audioSource;          // Componente de áudio

    private bool isDead = false;
    private Rigidbody rb;
    private Player moveScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveScript = GetComponent<Player>();

        // Configura ou pega o AudioSource automaticamente no mesmo objeto
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

        // 1. Tocar o som de morte
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // 2. Desativar o movimento e colisões futuras
        if (moveScript != null) moveScript.enabled = false;
        GetComponent<Collider>().enabled = false; // Impede que seja atropelado novamente enquanto cai

        // 3. Aplicar Física (Empurrão e Pulo)
        if (rb != null)
        {
            rb.isKinematic = false; // Desativa o modo Cinemático para a física agir
            rb.useGravity = true;

            // Calcula a direção oposta à colisão (aproximadamente para trás)
            Vector3 knockbackDir = (transform.position - rb.worldCenterOfMass).normalized;
            knockbackDir.y = 0; // Mantém o empurrão horizontal

            // Aplica as forças
            rb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse);
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        }

        // 4. Animação de Rotação (Capote)
        float timer = 0;
        float duration = deathDelay * 0.75f; // Gira durante boa parte do tempo de morte

        while (timer < duration)
        {
            // Gira o sapo loucamente nos eixos X e Z
            transform.Rotate(new Vector3(rotationSpeed, 0, rotationSpeed / 2f) * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // 5. Reiniciar o Nível
        yield return new WaitForSeconds(deathDelay * 0.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}