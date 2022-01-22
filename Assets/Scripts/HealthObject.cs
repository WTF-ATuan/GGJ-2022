using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour
{
    public int Index;
    public bool Open
    {
        get => _Open;
        set
        {
            // ===========毀壞與修復的特效寫這邊============
            gameObject.SetActive(value);
            _Open = value;
        }
    }
    public bool _Open;
}
