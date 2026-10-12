using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Namespace necessário para usar TextMeshPro

public class LevelCountdownUI : MonoBehaviour
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
        StartTimer();
    }

    public void StartTimer()
    {
        if (!isCounting)
        {
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