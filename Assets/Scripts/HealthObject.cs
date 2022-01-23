using System.Collections;
using System.Collections.Generic;
using SoundEffect;
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
            _Open = value;
            m_SpriteRenderer.sprite = _Open ? normalSprite : brokeSprite;
            SoundManager.instance.PlaySoundEffect("背包被破壞");
        }
    }
    public bool _Open;

    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
