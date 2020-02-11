using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitForButtonClicked : CustomYieldInstruction
{
    private bool clicked;

    public override bool keepWaiting
    {
        get { return !clicked; }
    }

    public WaitForButtonClicked(Button waitButton)
    {
        clicked = false;
        waitButton.onClick.AddListener( () => clicked = true );
    }
}
