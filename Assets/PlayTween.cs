using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonDOTweenEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Configuração do Hover (Passar o Rato)")]
    [SerializeField] private float hoverScale = 1.15f;
    [SerializeField] private float hoverDuration = 0.2f;

    [Header("Configuração do Clique (Rotação)")]
    [SerializeField] private float clickDuration = 0.5f;

    private Vector3 originalScale;
    private bool isRotating = false;

    private void Start()
    {
        // Guarda a escala original do botão
        originalScale = transform.localScale;
    }

    // Executado quando o rato entra na área do botão
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * hoverScale, hoverDuration).SetEase(Ease.OutBack);
    }

    // Executado quando o rato sai da área do botão
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, hoverDuration).SetEase(Ease.OutQuad);
    }

    // Executado quando se clica no botão
    public void OnPointerClick(PointerEventData eventData)
    {
        // Evita que o botão volte a rodar se já estiver a meio de uma rotação
        if (isRotating) return;

        isRotating = true;

        // Rotação corrigida para FastBeyond360 e Ease para OutQuad
        transform.DORotate(new Vector3(0, 0, 360f), clickDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Reseta a rotação para 0 para evitar valores infinitamente altos no Transform
                transform.localEulerAngles = Vector3.zero;
                isRotating = false;
            });
    }
}