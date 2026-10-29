using DG.Tweening;
using Unity.Collections;
using UnityEngine;

public class Carro : MonoBehaviour
{
    [Header("Animações")]
    private Transform carro;
    public Transform fim;
    public Ease caminho;
    public float velocidade = 5f;
    public float delay = 0f;
    public Ease tamanho;
    public float duracao = 1f;

    void Start()
    {
        carro = GetComponent<Transform>();

        transform.DOMove(new Vector3(carro.position.x, carro.position.y, fim.position.z), velocidade + delay).SetEase(caminho).SetLoops(-1, LoopType.Restart);
        carro.DOScale(new Vector3(0.8f, 1.2f, 1f), duracao).SetEase(tamanho).SetLoops(-1, LoopType.Yoyo);
    }
}