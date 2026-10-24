using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

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
            UpdateTimerUI();
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