using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownOption : MonoBehaviour
{
    private static TMP_Dropdown m_dropDown;
    void Awake()
    {
        m_dropDown = GetComponent<TMP_Dropdown>();
    }
    public static int GetOption() => m_dropDown.value;
}
