using System;
using UnityEngine;
using System.Collections;

public class MovingCar : MonoBehaviour
{
    public int CarroNumero = 0;
    public Transform inicio;
    public Transform fim;
    [Range(0f, 1f)]
    public float tempo;
    public float velocidade = 1f;
    public AnimationCurve curve;
    public static int carroAtual = 1;

    private void Start()
    { StartCoroutine(Movimento()); }

    private void Update()
    {
        if (carroAtual >= 9)
        { carroAtual = 1; }
    }
    IEnumerator Movimento()
    {
        while (true)
        {
            if (CarroNumero == carroAtual)
            {
                tempo = 0f;

                while (tempo < 1f)
                {
                    if (inicio != null && fim != null)
                    {
                        tempo += Time.deltaTime * velocidade;

                        float curveValue = curve.Evaluate(tempo);
                        transform.position = Vector3.Lerp(inicio.position, fim.position, curveValue);
                        yield return null;
                    }
                }
                yield return new WaitForSeconds(3f);

                carroAtual += 1;

                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return null;
            }
        }
    }
}