using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitForButtonClick : CustomYieldInstruction
{
    private bool clicked;

    public override bool keepWaiting
    {
        get { return !clicked; }
    }

    public WaitForButtonClick(Button waitButton)
    {
        clicked = false;
        waitButton.onClick.AddListener(OnClickButton);
    }

    public void OnClickButton()
    {
        clicked = true;
    }
}
