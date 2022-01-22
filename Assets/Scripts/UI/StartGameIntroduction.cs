using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartGameIntroduction : MonoBehaviour
{
    public Transform[] TextUIs;
    Vector2[] StartTextUI_Pots;

    //Boss image change
    public Image bossImage;
    public Sprite boss01Sprite, boss02Sprite, boss03Sprite;
    public float minChangeTime = 0.1f, maxChangeTime = 3f;
    
    void Start()
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
    }
    void Update()
    {
        for (int i = 0; i < TextUIs.Length; i++)
            TextUIs[i].localPosition = StartTextUI_Pots[i] + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
