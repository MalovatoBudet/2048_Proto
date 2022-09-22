using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projection : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Text _text;
    [SerializeField] Transform _visualTransform;

    public void Setup(Material material, string numberText, float radius)
    {
        _renderer.material = material;
        _text.text = numberText;
        _visualTransform.localScale = Vector3.one * radius * 2;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
