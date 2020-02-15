using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private Player[] players;
    private Square[] squares;
    private Stage stage;

    public GameManager(Stage stage)
    {
        this.stage = stage;
        players = stage.GetPlayers();
        squares = stage.GetSquares();
    }

    public IEnumerator Run()
    {
        //READY!ボタンが押されるまで待つ
        yield return stage.WaitForGettingReady();

        while (true) {
            for (int i = 0; i < players.Length; i++) {
                Debug.Log("turn: player" + i.ToString());
                IEnumerator rollDice = stage.RollDice(1, 6);
                yield return rollDice;
                int dicenum = (int)rollDice.Current;
                Debug.Log("dicenum: " + dicenum);
                IEnumerator movePlayer = stage.MovePlayer(players[i], dicenum);
                yield return movePlayer;
                //行先のマスのイベント;
            }
        }
    }
}
