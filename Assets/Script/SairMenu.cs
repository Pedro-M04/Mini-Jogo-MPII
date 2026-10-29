using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Função para sair do jogo
    public void ExitGame()
    {
        Debug.Log("A sair do jogo...");

        // Fecha a aplicação (funciona no jogo final / build)
        Application.Quit();

#if UNITY_EDITOR
        // Se estiveres a testar dentro do Editor do Unity, para o modo Play
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
