using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DelayedSceneLoader : MonoBehaviour
{
    [Header("Tempo de espera antes de mudar de cena")]
    public float delaySeconds = 1.5f; // Podes alterar este valor diretamente no Inspector do Unity!

    // Agora recebe apenas 1 parâmetro, o que permite aparecer no OnClick() do botão
    public void LoadSceneAfterDelay(string sceneName)
    {
        StartCoroutine(WaitAndLoad(sceneName));
    }

    private IEnumerator WaitAndLoad(string Jogo)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(Jogo);
    }
}