using UnityEngine;
using DG.Tweening;

public class PlayerScaleBounce : MonoBehaviour
{
    [Header("Configurações de Escala")]
    [Tooltip("Fator de escala máxima (ex: 1.15 significa 15% maior).")]
    [SerializeField] private float targetScaleMultiplier = 1.15f;

    [Tooltip("Duração da transição da escala.")]
    [SerializeField] private float duration = 0.12f;

    [Header("Correção de Flutuação (Pés no Chão)")]
    [Tooltip("Ajusta a compensação para manter os pés colados ao chão enquanto cresce.")]
    [SerializeField] private float heightOffsetCorrection = 0.5f;

    private Vector3 originalScale;
    private Vector3 originalLocalPos;
    private bool isExpanded = false;

    void Start()
    {
        // Guarda a escala e a posição local inicial com segurança
        originalScale = transform.localScale;
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        // Verifica se qualquer uma das teclas WASD foi pressionada
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            ToggleScale();
        }
    }

    void ToggleScale()
    {
        // Mata qualquer animação anterior para evitar conflitos
        transform.DOKill(true);

        // Alterna o estado
        isExpanded = !isExpanded;

        // Calcula a escala de destino
        Vector3 destinationScale = isExpanded ? originalScale * targetScaleMultiplier : originalScale;

        // Calcula a altura extra necessária para compensar o crescimento a partir do centro
        float scaleDifference = destinationScale.y - originalScale.y;
        float targetPosY = originalLocalPos.y + (scaleDifference * heightOffsetCorrection);

        // Usa uma sequência do DOTween para animar a ESCALA e a PISOR Y em simultâneo
        Sequence mySequence = DOTween.Sequence();
        mySequence.Join(transform.DOScale(destinationScale, duration).SetEase(Ease.OutQuad));
        mySequence.Join(transform.DOLocalMoveY(targetPosY, duration).SetEase(Ease.OutQuad));
    }
}