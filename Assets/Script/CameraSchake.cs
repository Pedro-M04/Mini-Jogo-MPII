using UnityEngine;
using DG.Tweening;

public class CameraShakeWASD : MonoBehaviour
{
    [Header("Configurações do Shake (Suave)")]
    [Tooltip("Duração de cada abanão.")]
    [SerializeField] private float duration = 0.15f;

    [Tooltip("Força/Intensidade do abanão (podes baixar para 0.02 ou 0.03 se ainda estiver forte).")]
    [SerializeField] private float strength = 0.05f;

    [Tooltip("Número de vibrações durante o shake.")]
    [SerializeField] private int vibrato = 8;

    [Tooltip("Aleatoricidade do movimento.")]
    [SerializeField] private float randomness = 45f;

    private Vector3 originalLocalPosition;

    void Start()
    {
        // Guarda a posição local original da câmara
        originalLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Sempre que premes W, A, S ou D, a câmara dá um pequeno abanão
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            TriggerShake();
        }
    }

    void TriggerShake()
    {
        // Mata o tween anterior e força a câmara a voltar IMEDIATAMENTE à posição original,
        // evitando que ela se vá desviando aos poucos ("drift").
        transform.DOKill(true);

        // Executa o shake na posição local
        transform.DOShakePosition(duration, strength, vibrato, randomness, false, true)
            .SetEase(Ease.OutQuad);
    }
}