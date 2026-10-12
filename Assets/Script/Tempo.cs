using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Tempo : MonoBehaviour
{
    [Header("Configurações do Tempo")]
    public float timeLimit = 30.0f;             // Tempo total do nível em segundos
    public TextMeshProUGUI timerText;          // Arrastas aqui o texto da UI

    private float currentTime;
    private bool isCounting = false;

    void Start()
    {
        currentTime = timeLimit;
        UpdateTimerUI();

        // Esconde o texto do timer no início da cena
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Se o timer ainda não começou e o jogador pressionar Space
        if (!isCounting && Input.GetKeyDown(KeyCode.Space))
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        if (!isCounting)
        {
            // Mostra o texto do timer quando o jogo/contagem começa
            if (timerText != null)
            {
                timerText.gameObject.SetActive(true);
            }

            StartCoroutine(CountdownRoutine());
        }
    }

    IEnumerator CountdownRoutine()
    {
        isCounting = true;

        while (currentTime > 0 && isCounting)
        {
            yield return new WaitForSeconds(1.0f);

            currentTime -= 1.0f;
            UpdateTimerUI(); // Atualiza o texto no ecrã
        }

        if (currentTime <= 0)
        {
            TimeOut();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Converte o tempo para formato inteiro e mostra no ecrã
            timerText.text = "Tempo: " + Mathf.Ceil(currentTime).ToString();
        }
    }

    void TimeOut()
    {
        Debug.Log("O tempo esgotou-se! O sapo não sobreviveu.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StopTimer()
    {
        isCounting = false;
        StopAllCoroutines();
    }
}