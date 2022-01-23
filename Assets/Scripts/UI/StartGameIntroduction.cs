using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartGameIntroduction : MonoBehaviour
{
    public Transform[] TextUIs;
    Vector2[] StartTextUI_Pots;

    //Boss image change
    public Image bossImage;
    public Image RedStom;
    public Image BuleStom;
    public Sprite boss01Sprite, boss02Sprite, boss03Sprite;
    public float minChangeTime = 0.1f, maxChangeTime = 3f;
    public float shakePower = 5f;

    private void OnEnable()
    {
        StartTextUI_Pots = new Vector2[TextUIs.Length];
        for (int i = 0; i < TextUIs.Length; i++)
            StartTextUI_Pots[i] = TextUIs[i].transform.localPosition;

        StartCoroutine(IE_WaitToChangeBossImage());
        IEnumerator IE_WaitToChangeBossImage()
        {
            while (true) {
                float _changeTime = Random.Range(minChangeTime, maxChangeTime);
                yield return new WaitForSeconds(_changeTime);
                bossImage.sprite = boss02Sprite;
                yield return new WaitForSeconds(0.15f);
                bossImage.sprite = boss03Sprite;
                yield return new WaitForSeconds(0.15f);
                bossImage.sprite = boss02Sprite;
                yield return new WaitForSeconds(0.15f);
                bossImage.sprite = boss01Sprite;
            }
        }
        StartCoroutine(IE_BossHandFly());
        IEnumerator IE_BossHandFly()
        {
            Vector2 startv2 = bossImage.transform.localPosition;
            for(; ; )
            {
                yield return bossImage.transform.DOLocalMoveY(startv2.y + 50, 5f).WaitForCompletion();
                yield return bossImage.transform.DOLocalMoveY(startv2.y, 5f).WaitForCompletion();
            }
        }
        StartCoroutine(IE_RedStomFly());
        IEnumerator IE_RedStomFly()
        {
            Vector2 startv2 = bossImage.transform.localPosition;
            for (; ; )
            {
                yield return RedStom.transform.DOLocalMoveY(startv2.y + 40, 4f).WaitForCompletion();
                yield return RedStom.transform.DOLocalMoveY(startv2.y - 40, 4f).WaitForCompletion();
            }
        }
        StartCoroutine(IE_BlueStomFly());
        IEnumerator IE_BlueStomFly()
        {
            Vector2 startv2 = bossImage.transform.localPosition;
            for (; ; )
            {
                yield return BuleStom.transform.DOLocalMoveY(startv2.y - 40, 4f).WaitForCompletion();
                yield return BuleStom.transform.DOLocalMoveY(startv2.y + 40, 4f).WaitForCompletion();
            }
        }
    }
    void Update()
    {
        for (int i = 0; i < TextUIs.Length; i++)
            TextUIs[i].localPosition = StartTextUI_Pots[i] + new Vector2(Random.Range(-shakePower, shakePower), Random.Range(-shakePower, shakePower));
    }
}
