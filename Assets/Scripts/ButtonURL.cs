using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonURL : MonoBehaviour
{
    public void ClickButtonToSelfURL(string _url)
    {
        Application.OpenURL(_url);
    }
}
