using System;
using Extra;
using Magnet;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public MagneticPole magneticPole;

    public Vector3 StartPosition { get; private set; }

    const float MoveTime = 2;

    public void Start()
    {
        StartPosition = transform.position;
    }

    public void SetMagneticPole(MagneticPole pole)
    {
        magneticPole = pole;
        var spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.color = pole == MagneticPole.North ? Color.red : Color.blue;
    }

    /// <summary>
    /// 移動到某個終點（Index=終點編號 EndV3=終點座標）
    /// </summary>
    public void Move(int Index, Vector3 EndV3)
    {
        StartCoroutine(_());

        IEnumerator _()
        {
            if (Index == 0)
            {
                transform.DOMoveZ(EndV3.z, MoveTime);
                transform.DOMoveY(EndV3.y, MoveTime);
                yield return transform.DOMoveX(EndV3.x - 2f, MoveTime * 0.5f).WaitForCompletion();
                transform.DOMoveX(EndV3.x, MoveTime * 0.5f);
            }
            else if (Index == 1)
            {
                transform.DOMoveX(EndV3.x, MoveTime);
                transform.DOMoveZ(EndV3.z, MoveTime);
                yield return transform.DOMoveY(EndV3.y + 0.1f, MoveTime * 0.3f).WaitForCompletion();
                transform.DOMoveY(EndV3.y, MoveTime * 0.7f);
            }
            else
            {
                transform.DOMoveZ(EndV3.z, MoveTime);
                transform.DOMoveY(EndV3.y, MoveTime);
                yield return transform.DOMoveX(EndV3.x + 2f, MoveTime * 0.5f).WaitForCompletion();
                transform.DOMoveX(EndV3.x, MoveTime * 0.5f);
            }
        }
    }
}