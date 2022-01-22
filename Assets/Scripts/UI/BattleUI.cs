using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public Text BossHP;

    void Update()
    {
        BossHP.text = $"Boss HP:{AttackMagnet._.HP}";
    }
}
