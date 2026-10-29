using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tempo : MonoBehaviour
{
    [Header("Tempo")]
    public float timeLimit = 30.0f;
    public TextMeshProUGUI timerText;

    private float currentTime;
    private bool isCounting = false;

    void Start()
    {
        currentTime = timeLimit;
        UpdateTimerUI();

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
        // Coloca no Start() do teu LevelCountdownUI
        timerText.transform.localScale = Vector3.zero;
        timerText.transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBack);


    }

    void Update()
    {
        if (!isCounting && Input.GetKeyDown(KeyCode.Space))
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        if (!isCounting)
        {
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
<<<<<<< Updated upstream
            UpdateTimerUI();
=======
            UpdateTimerUI(); // Atualiza o texto no ecrã

            if (currentTime <= 5f && timerText != null)
            {
                timerText.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
            }
>>>>>>> Stashed changes
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
            timerText.text = "Tempo: " + Mathf.Ceil(currentTime).ToString();
        }
    }

    void TimeOut()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StopTimer()
    {
        isCounting = false;
        StopAllCoroutines();
    }
}