using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void OnGUI()
    {
        Util.SetFontSize(32);

        Util.SetFontAlignment(TextAnchor.MiddleCenter);

        float w = 128;
        float h = 32;
        float px = Screen.width / 2 - w / 2;
        float py = Screen.height / 2 - h / 2;

        Util.GUILabel(px, py, w, h, "心中すごろく");

        py += 60;
        if (GUI.Button(new Rect(px, py, w, h), "START"))
        {
            Application.LoadLevel("SimpleStageScene");
        }
    }
}
