﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private List<Player> players;
    private List<Square> squares;
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
            for (int i = 0; i < players.Count; i++) {
                Debug.Log("turn: player" + i.ToString());
                stage.MoveCamera(players[i].transform.position);

                //dice roll
                IEnumerator rollDice = stage.RollDice(1, 6);
                yield return rollDice;
                int dicenum = (int)rollDice.Current;
                Debug.Log("dicenum: " + dicenum);

                //player move
                IEnumerator movePlayer = stage.MovePlayer(players[i], dicenum);
                yield return movePlayer;

                //行先のマスのイベント;
                IEnumerator squareEvent = players[i].Pos.execEvent(stage,i);
                yield return squareEvent;
            }
        }
    }
}
