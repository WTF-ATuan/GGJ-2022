using System;
using System.Collections;
using System.Collections.Generic;
using Magnet;
using UnityEngine;

public class Enemy_Backtrack : MonoBehaviour
{
    public Sprite blueSprite01, blueSprite02, redSprite01, redSprite02;
    public MagneticPole magneticPole
    {
        get => _magneticPole;
        set
        {
            // ========改變磁極的特效寫這邊=========
            var spriteRender = GetComponent<SpriteRenderer>();

            Sprite _sprite01 = value == MagneticPole.North ? blueSprite01 : redSprite01;
            Sprite _sprite02 = value == MagneticPole.North ? blueSprite02 : redSprite02;
            _magneticPole = value;

            StartCoroutine(RunChangeSprite(_sprite01, _sprite02, 0.2f));
            IEnumerator RunChangeSprite(Sprite _sprite01, Sprite _sprite02, float _waitTime)
            {
                while (true)
                {
                    spriteRender.sprite = _sprite01;
                    yield return new WaitForSeconds(_waitTime);
                    spriteRender.sprite = _sprite02;
                    yield return new WaitForSeconds(_waitTime);
                }
            }
        }
    }
    public MagneticPole _magneticPole;

    public Action End_Act;

    public float MoveTime = 1f;

    public void Move(Transform EndObj)
    {
        StartCoroutine(_());

        IEnumerator _()
        {
            //  寫錯了這是給反擊用的移動
            Vector3 StartV3 = transform.position;
            float f = 0;
            for (; ; )
            {
                f += 1f / MoveTime * Time.deltaTime;
                transform.position =  Vector3.Lerp(StartV3, EndObj.position, f);
                if (f >= 1) break;
                yield return null;
            }
            End_Act?.Invoke();
        }
    }
}
