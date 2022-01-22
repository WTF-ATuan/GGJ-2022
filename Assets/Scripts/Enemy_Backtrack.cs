using System;
using System.Collections;
using System.Collections.Generic;
using Magnet;
using UnityEngine;

public class Enemy_Backtrack : MonoBehaviour
{
    public MagneticPole magneticPole
    {
        get => _magneticPole;
        set
        {
            // ========改變磁極的特效寫這邊=========
            var spriteRender = GetComponent<SpriteRenderer>();
            spriteRender.color = value == MagneticPole.North ? Color.red : Color.blue;
            _magneticPole = value;
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
