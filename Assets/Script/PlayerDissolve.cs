using UnityEngine;
using DG.Tweening;

public class PlayerDissolve : MonoBehaviour
{
    private Material playerMaterial;

    [Header("Configurações de Dissolução")]
    public float dissolveDuration = 1.5f;
    [SerializeField] private string propertyName = "Dissolve Shader"; // ATENÇÃO: Confirma se o Reference Name no Shader Graph é exatamente este!

    void Start()
    {
        // Procura o Renderer no objeto ou nos filhos
        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            // Nota: Usar .material cria uma instância única para este objeto
            playerMaterial = rend.material;
            playerMaterial.SetFloat(propertyName, 0f);
            Debug.Log("Sucesso: Material encontrado no objeto " + rend.gameObject.name);
        }
        else
        {
            Debug.LogError("Erro: Nenhum Renderer encontrado no player ou nos filhos!");
        }
    }

    // Chama esta função quando o sapo morre
    public void StartDissolveDeath()
    {
        if (playerMaterial != null)
        {
            // Anima o shader de 0 para 1 durante o tempo definido
            DOTween.To(() => playerMaterial.GetFloat(propertyName),
                       x => playerMaterial.SetFloat(propertyName, x),
                       1f, dissolveDuration);
        }
    }
}