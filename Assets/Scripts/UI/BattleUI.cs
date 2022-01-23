using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject[] HP_N;
    public GameObject[] HP_S;

    void Update()
    {
        for(int i = 0; i < HP_N.Length; i++)
        {
            if (i < AttackMagnet._.HP_N)
            {
                HP_N[i].SetActive(true);
            }
            else
            {
                HP_N[i].SetActive(false);
            }
        }
        for (int i = 0; i < HP_S.Length; i++)
        {
            if (i < AttackMagnet._.HP_S)
            {
                HP_S[i].SetActive(true);
            }
            else
            {
                HP_S[i].SetActive(false);
            }
        }
    }
}
