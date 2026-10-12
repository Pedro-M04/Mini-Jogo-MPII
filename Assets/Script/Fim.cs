using UnityEngine;
using UnityEngine.SceneManagement; // Necessário se quiseres reiniciar ou mudar de nível

public class Fim : MonoBehaviour
{
    [Header("Configurações")]
    public string playerTag = "Player";          // Tag do sapo
    public float restartDelay = 2f;             // Tempo de espera antes de reiniciar/passar de nível

    private bool hasReached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasReached && other.CompareTag(playerTag))
        {
            hasReached = true;

            Player frog = other.GetComponent<Player>();
            if (frog != null)
            {
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
