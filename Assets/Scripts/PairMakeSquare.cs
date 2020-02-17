using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairMakeSquare : Square
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
        IEnumerator rollDice = stage.RollDice(1, 6);
        yield return rollDice;
        int dicenum = (int)rollDice.Current;
        if(dicenum>3){
            int pairPlayerNum = UnityEngine.Random.Range(0,players.Count);
            players[playerNum].Pair = players[pairPlayerNum];
            Debug.Log("player" + playerNum.ToString() + " paired with player" + pairPlayerNum.ToString());
        }else{
            Debug.Log("player" + playerNum.ToString() + " was not able to form a pair");
        }

        yield return new WaitForSeconds(2);
    }
}
