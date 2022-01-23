using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magnet;
using DG.Tweening;

public class Actor_C : MonoBehaviour
{
    public Sprite blueSprite, redSprite;

    public SpriteRenderer SpriteR;

    public MagneticPole magneticPole
    {
        get => _magneticPole;
        set
        {
            // ========改變磁極的特效寫這邊=========
            SpriteR.sprite = value == MagneticPole.North ? redSprite : blueSprite;
        }
    }
    public MagneticPole _magneticPole;

    public void Open(MagneticPole m)
    {
        magneticPole = m;
        gameObject.SetActive(true);
        StartCoroutine(_());

        IEnumerator _()
        {
            transform.localScale = Vector3.zero;
            yield return transform.DOScale(Vector3.one, 0.2f).WaitForCompletion();
            Destroy(gameObject);
        }
    }
}
