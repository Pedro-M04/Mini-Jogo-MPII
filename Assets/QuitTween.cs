using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class QuitTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Configuração do Hover (Passar o Rato)")]
    [SerializeField] private float hoverScale = 1.15f;
    [SerializeField] private float hoverDuration = 0.2f;

    [Header("Configuração do Clique (Escala)")]
    [SerializeField] private float clickScale = 1.25f;
    [SerializeField] private float clickDuration = 0.2f;

    private Vector3 originalScale;
    private bool isAnimatingClick = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimatingClick) return;
        transform.DOScale(originalScale * hoverScale, hoverDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimatingClick) return;
        transform.DOScale(originalScale, hoverDuration).SetEase(Ease.OutQuad);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isAnimatingClick) return;

        isAnimatingClick = true;
        transform.DOKill();

        Sequence clickSeq = DOTween.Sequence();
        clickSeq.Append(transform.DOScale(originalScale * clickScale, clickDuration / 2f).SetEase(Ease.OutQuad));
        clickSeq.Append(transform.DOScale(originalScale, clickDuration / 2f).SetEase(Ease.InQuad));
        clickSeq.OnComplete(() =>
        {
            isAnimatingClick = false;
        });
    }
}