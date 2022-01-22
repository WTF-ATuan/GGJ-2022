using System;
using Extra;
using Magnet;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public MagneticPole magneticPole;
    public int EndIndex;

    public Vector3 StartPosition { get; private set; }

    public void Start()
    {
        StartPosition = transform.position;
    }

    /// <summary>
    /// 移動到某個終點（Index=終點編號 EndV3=終點座標）
    /// </summary>
    public void Move (int Index, Vector3 EndV3)
    {
        StartCoroutine(_());
        IEnumerator _()
        {
            if (Index == 0)
            {
                transform.DOMoveZ(EndV3.z, 1);
                transform.DOMoveY(EndV3.y, 1);
                yield return transform.DOMoveX(EndV3.x - 2f, 0.5f).WaitForCompletion();
                transform.DOMoveX(EndV3.x, 0.5f);
            }
            else if (Index == 1)
            {
                transform.DOMoveX(EndV3.x, 1);
                transform.DOMoveZ(EndV3.z, 1);
                yield return transform.DOMoveY(EndV3.y + 0.1f, 0.3f).WaitForCompletion();
                transform.DOMoveY(EndV3.y, 0.7f);
            }
            else
            {
                transform.DOMoveZ(EndV3.z, 1);
                transform.DOMoveY(EndV3.y, 1);
                yield return transform.DOMoveX(EndV3.x + 2f, 0.5f).WaitForCompletion();
                transform.DOMoveX(EndV3.x, 0.5f);
            }
        }
    }
}