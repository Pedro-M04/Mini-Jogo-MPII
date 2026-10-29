using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Agua : MonoBehaviour
{
    [Header("Configuração de Morte")]
    [Tooltip("Tag que identifica o jogador.")]
    public string tagDoJogador = "Player";

    [Tooltip("Prefab do efeito VFX (Water_Splash_B).")]
    public GameObject efeitoSplash; // Arrasta o teu prefab para aqui no Inspector

    [Tooltip("Tempo em segundos de espera (animação/áudio/efeito) antes de reiniciar a cena.")]
    public float tempoDeEspera = 1.5f;

    // Variável para evitar que a morte seja acionada várias vezes
    private bool jogadorMorreu = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!jogadorMorreu && collision.gameObject.CompareTag(tagDoJogador))
        {
            IniciarMorte(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!jogadorMorreu && collision.CompareTag(tagDoJogador))
        {
            IniciarMorte(collision.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!jogadorMorreu && collision.gameObject.CompareTag(tagDoJogador))
        {
            IniciarMorte(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!jogadorMorreu && other.CompareTag(tagDoJogador))
        {
            IniciarMorte(other.gameObject);
        }
    }

    void IniciarMorte(GameObject jogador)
    {
        jogadorMorreu = true;
        Debug.Log("O jogador morreu! A instanciar o splash e a reiniciar...");

        // 1. CRIAR O EFEITO DE SPLASH NA POSIÇÃO DO JOGADOR
        if (efeitoSplash != null)
        {
            // Instancia o efeito na posição e rotação atual do jogador
            Instantiate(efeitoSplash, jogador.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("O prefab 'efeitoSplash' não foi atribuído no Inspector!");
        }

        // 2. FAZER O JOGADOR DESAPARECER COMPLETAMENTE:
        Renderer renderizador = jogador.GetComponentInChildren<Renderer>();
        if (renderizador != null)
        {
            renderizador.enabled = false;
        }

        // Desativa os colliders e a física para ele não continuar a cair
        Collider colisorJogador = jogador.GetComponent<Collider>();
        if (colisorJogador != null) colisorJogador.enabled = false;

        Collider2D colisorJogador2D = jogador.GetComponent<Collider2D>();
        if (colisorJogador2D != null) colisorJogador2D.enabled = false;

        Rigidbody rb = jogador.GetComponent<Rigidbody>();
        if (rb != null) { rb.linearVelocity = Vector3.zero; rb.isKinematic = true; }

        Rigidbody2D rb2d = jogador.GetComponent<Rigidbody2D>();
        if (rb2d != null) { rb2d.linearVelocity = Vector2.zero; rb2d.simulated = false; }

        // 3. Inicia a contagem de tempo para reiniciar a cena
        StartCoroutine(EsperarEReiniciar());
    }

    IEnumerator EsperarEReiniciar()
    {
        // Espera o tempo definido para o efeito de splash e o áudio terminarem
        yield return new WaitForSeconds(tempoDeEspera);

        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}