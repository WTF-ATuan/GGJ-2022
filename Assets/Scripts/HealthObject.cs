using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    public Sprite brokeSprite, normalSprite;
    public int Index;

    private SpriteRenderer m_SpriteRenderer;
    public bool Open
    {
        get => _Open;
        set
        {
            // ===========毀壞與修復的特效寫這邊============
            gameObject.SetActive(value);
            _Open = value;
        }
    }
    public bool _Open;

    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        m_SpriteRenderer.sprite = _Open ? normalSprite : brokeSprite;
    }
}
