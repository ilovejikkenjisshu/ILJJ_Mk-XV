using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStageManager : MonoBehaviour, Stage
{
    Player[] Stage.GetPlayers()
    {
        throw new System.NotImplementedException();
    }

    Square[] Stage.GetSquares()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager manager = new GameManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
