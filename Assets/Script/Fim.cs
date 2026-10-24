using UnityEngine;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour
{
    [Header("Configurações")]
    public string playerTag = "Player";
    public float restartDelay = 2f;

    [Header("Aúdio")]
    public AudioClip victorySound;
    private AudioSource audioSource;

    private bool hasReached = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasReached && other.CompareTag(playerTag))
        {
            hasReached = true;

            Player frog = other.GetComponent<Player>();
            if (frog != null)
            {
                if (victorySound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(victorySound);
                }

                Invoke("RestartGame", restartDelay);
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}