using UnityEngine;

public class EfeitoPorContato : MonoBehaviour
{
    [Header("Configuração do Efeito")]
    [Tooltip("Tag que identifica o jogador.")]
    public string tagDoJogador = "Player";

    [Tooltip("Prefab do efeito VFX a ser instanciado.")]
    public GameObject efeitoVFX; // Arrasta o teu prefab para aqui no Inspector

    [Tooltip("Se ativado, o efeito é destruído após alguns segundos para não acumular na cena.")]
    public bool destruirEfeito = true;
    public float tempoParaDestruir = 2.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagDoJogador))
        {
            AtivarEfeito(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagDoJogador))
        {
            AtivarEfeito(collision.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagDoJogador))
        {
            AtivarEfeito(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagDoJogador))
        {
            AtivarEfeito(other.gameObject);
        }
    }

    void AtivarEfeito(GameObject jogador)
    {
        // Instancia o efeito VFX na posição exata do jogador
        if (efeitoVFX != null)
        {
            GameObject efeitoCriado = Instantiate(efeitoVFX, jogador.transform.position, Quaternion.identity);

            // Se quiseres que o efeito se apague sozinho após uns segundos
            if (destruirEfeito)
            {
                Destroy(efeitoCriado, tempoParaDestruir);
            }
        }
        else
        {
            Debug.LogWarning("O prefab 'efeitoVFX' não foi atribuído no Inspector!");
        }
    }
}