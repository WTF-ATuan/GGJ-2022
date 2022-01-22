using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    public Transform TextUI;
    public Transform Kr;
    public Transform Kr_ef;
    public Transform Pl;

    Vector2 StartTextUI_Pot;
    float StartKr_R;
    float StartPl_R;

    private void Start()
    {
        StartTextUI_Pot = TextUI.transform.localPosition;
        StartKr_R = Kr.eulerAngles.z;
        StartPl_R = Pl.eulerAngles.z;

        StartCoroutine(KrMove());
        StartCoroutine(PlMove());
    }

    private void Update()
    {
        TextUI.localPosition = StartTextUI_Pot + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    public IEnumerator KrMove()
    {
        for(; ; )
        {
            yield return Kr.DORotate(new Vector3(0, 0, StartKr_R + 90), 5).SetEase(Ease.Linear).WaitForCompletion();
            yield return Kr.DORotate(new Vector3(0, 0, StartKr_R + 180), 5).SetEase(Ease.Linear).WaitForCompletion();
            yield return Kr.DORotate(new Vector3(0, 0, StartKr_R + 270), 5).SetEase(Ease.Linear).WaitForCompletion();
            yield return Kr.DORotate(new Vector3(0, 0, StartKr_R), 5).SetEase(Ease.Linear).WaitForCompletion();
        }
    }
    public IEnumerator PlMove()
    {
        for (; ; )
        {
            yield return Pl.DORotate(new Vector3(0, 0, StartPl_R - 10), 5).SetEase(Ease.Linear).WaitForCompletion();
            yield return Pl.DORotate(new Vector3(0, 0, StartPl_R + 10), 5).SetEase(Ease.Linear).WaitForCompletion();
        }
    }
}
