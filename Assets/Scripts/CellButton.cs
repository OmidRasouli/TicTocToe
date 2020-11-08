using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CellButton : MonoBehaviour
{
    internal bool isActive = true;
    public void Clicked(string character)
    {
        transform.GetChild(0).GetComponent<Text>().text = character;
        isActive = false;
    }

    internal void Reset()
    {
        transform.GetChild(0).GetComponent<Text>().text = "";
        isActive = true;
    }
}
