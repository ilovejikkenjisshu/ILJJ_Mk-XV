using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private Player[] players;
    private Square[] squares;
    private Stage stage;

    public GameManager(Stage _stage)
    {
        stage = _stage;
        /*
        players = stage.GetPlayers();
        squares = stage.GetSquares();
        */
    }

    public IEnumerator GameCoroutine()
    {
        //READY!ボタンが押されるまで待つ
        yield return stage.WaitForGetReady();

        /*
        while(ゲーム終了まで){
            プレイヤーごとの処理{
                サイコロを振ってもらう;
                行先を選んでもらう(player, dice);
                行先のマスのイベント;
            }
        }
        */
    }
}
