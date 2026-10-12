using UnityEngine;
using UnityEngine.SceneManagement; // Necessário se quiseres reiniciar ou mudar de nível

public class GoalZone : MonoBehaviour
{
    [Header("Configurações")]
    public string playerTag = "Player";          // Tag do sapo
    public float restartDelay = 2f;             // Tempo de espera antes de reiniciar/passar de nível

    private bool hasReached = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou na zona é o jogador e se ainda não foi alcançado
        if (!hasReached && other.CompareTag(playerTag))
        {
            hasReached = true;
            Debug.Log("Parabéns! O sapo chegou ao destino!");

            // Podes congelar o sapo ou desativar o movimento dele se quiseres
            FrogJump3D frog = other.GetComponent<FrogJump3D>();
            if (frog != null)
            {
                frog.enabled = false; // Desativa o script de movimento
            }

            // Exemplo: Reiniciar o jogo ou carregar a próxima fase após alguns segundos
            // Invoke("RestartGame", restartDelay);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
