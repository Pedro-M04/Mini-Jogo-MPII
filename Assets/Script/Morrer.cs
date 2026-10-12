using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FrogCollisionHandler : MonoBehaviour
{
    [Header("Configurações de Morte")]
    public string obstacleTag = "Obstacle";    // Tag usada nos carros
    public float deathDelay = 2.0f;           // Tempo antes de reiniciar

    [Header("Animação de Morte")]
    public float knockbackForce = 5f;         // Força do empurrão para trás
    public float upForce = 3f;                // Força do pulo para cima na morte
    public float rotationSpeed = 720f;        // Velocidade da rotação (graus por segundo)
    public GameObject deathParticlePrefab;    // Prefab do efeito de partículas

    private bool isDead = false;
    private Rigidbody rb;
    private FrogJump3D moveScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveScript = GetComponent<FrogJump3D>();
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

        // 1. Desativar o movimento e colisões futuras
        if (moveScript != null) moveScript.enabled = false;
        GetComponent<Collider>().enabled = false; // Impede que seja atropelado novamente enquanto cai

        // 2. Efeito de Partículas (se configurado)
        if (deathParticlePrefab != null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }

        // 3. Aplicar Física (Empurrão e Pulo)
        // Ativamos a física temporariamente para o efeito de queda
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