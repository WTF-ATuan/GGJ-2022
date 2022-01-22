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
            gameObject.SetActive(value);
            _Open = value;
        }
    }
    public bool _Open;
}
