using System;
using Extra;
using Magnet;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Enemy : MonoBehaviour
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

    public Vector3 StartPosition { get; private set; }

    public float MoveTime = 4;

    public Action End_Act;

    public void Start()
    {
        StartPosition = transform.position;
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
                yield return transform.DOMoveX(EndV3.x, MoveTime * 0.5f).WaitForCompletion();
            }
            else if (Index == 1)
            {
                transform.DOMoveX(EndV3.x, MoveTime);
                transform.DOMoveZ(EndV3.z, MoveTime);
                yield return transform.DOMoveY(EndV3.y + 0.1f, MoveTime * 0.3f).WaitForCompletion();
                yield return transform.DOMoveY(EndV3.y, MoveTime * 0.7f).WaitForCompletion();
            }
            else
            {
                transform.DOMoveZ(EndV3.z, MoveTime);
                transform.DOMoveY(EndV3.y, MoveTime);
                yield return transform.DOMoveX(EndV3.x + 2f, MoveTime * 0.5f).WaitForCompletion();
                yield return transform.DOMoveX(EndV3.x, MoveTime * 0.5f).WaitForCompletion();
            }
            End_Act?.Invoke();
        }
    }
}