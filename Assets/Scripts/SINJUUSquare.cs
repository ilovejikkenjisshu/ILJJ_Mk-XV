using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SINJUUSquare : Square
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator execEvent(Stage stage,int playerNum)
    {
        List<Player> players = stage.GetPlayers();

        if(players[playerNum].Pair==null)yield break;

        stage.EndGame();
        while(true)yield return null;
    }
}
