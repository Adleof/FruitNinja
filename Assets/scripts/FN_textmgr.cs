using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FN_textmgr : MonoBehaviour
{
    public TextMeshProUGUI myui;
    public FN_curser player;
    void Update()
    {
        myui.text = player.getinfo();
    }
}
