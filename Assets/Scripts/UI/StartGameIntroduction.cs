using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StartGameIntroduction : MonoBehaviour
{
    public Transform[] TextUIs;
    Vector2[] StartTextUI_Pots;
    void Start()
    {
        StartTextUI_Pots = new Vector2[TextUIs.Length];
        for (int i = 0; i < TextUIs.Length; i++)
            StartTextUI_Pots[i] = TextUIs[i].transform.localPosition;
    }
    void Update()
    {
        for (int i = 0; i < TextUIs.Length; i++)
            TextUIs[i].localPosition = StartTextUI_Pots[i] + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
