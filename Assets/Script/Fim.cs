using System.Collections; // Necessário para usar Corrotinas
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para reiniciar ou mudar de nível

public class Fim : MonoBehaviour
{
    [Header("Configurações")]
    public string playerTag = "Player";          // Tag do jogador
    public float restartDelay = 2f;             // Tempo de espera antes de reiniciar/passar de nível

    private bool hasReached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasReached && other.CompareTag(playerTag))
        {
            hasReached = true;

            // Podes acionar a animação se o jogador tiver um script "Player" ou um "Animator"
            Player frog = other.GetComponent<Player>();
            if (frog != null)
            {
                // Se o teu script Player tiver uma função de vitória/fim, podes chamá-la aqui
            }

            // Exemplo para disparar a animação diretamente se o jogador tiver um Animator:
            Animator anim = other.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Vitoria"); // Altera "Vitoria" para o nome do trigger que tens na Animation Window
            }

            // Inicia a contagem de tempo antes de reiniciar o jogo
            StartCoroutine(EsperarEReiniciar());
        }
    }

    IEnumerator EsperarEReiniciar()
    {
        // Espera os segundos definidos na variável restartDelay (ex: 2 segundos)
        yield return new WaitForSeconds(restartDelay);

        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}