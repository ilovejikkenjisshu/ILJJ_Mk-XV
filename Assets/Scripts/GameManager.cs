using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private Player[] players;
    private Square[] squares;

    public GameManager(Stage stage)
    {
        /*
        players = stage.GetPlayers();
        squares = stage.GetSquares();
        */
    }

    public IEnumerator GameCoroutine()
    {
        //READY!ボタンが押されるまで待つ
        yield return null;

        /*
        while(ゲーム終了まで){
            
        }
        */
    }
}
