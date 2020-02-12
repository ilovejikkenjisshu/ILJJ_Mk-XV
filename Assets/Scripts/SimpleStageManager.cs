using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStageManager : MonoBehaviour, Stage
{
    public GameObject readyPanel;
    public Player playerPrefab;

    private Player[] players;
    private Square[] squares;

    Player[] Stage.GetPlayers()
    {
        throw new System.NotImplementedException();
    }

    Square[] Stage.GetSquares()
    {
        return squares;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitPlayers(4);
        InitSquares();
        GameManager manager = new GameManager(this);
        StartCoroutine(manager.Run());
        Debug.Log("Game Initialization finished");
    }

    // 仮に生成しているだけでPlayerに必要な情報は用意されていない
    private void InitPlayers(int playerNum)
    {
        players = new Player[playerNum];
        for(int i = 0; i < playerNum; i++)
        {
            players[i] = (Player) Instantiate(playerPrefab);
        }
    }

    private void InitSquares()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Square");

        squares = new Square[objs.GetLength(0)];
        for (int i = 0; i < objs.GetLength(0); i++)
        {
            squares[i] = objs[i].GetComponent<Square>();
        }
    }

    public IEnumerator WaitForGettingReady()
    {
        readyPanel.SetActive(true);
        Button readyButton = readyPanel.transform.GetChild(0).transform.GetComponent<Button>();
        yield return new WaitForButtonClicked(readyButton);
        readyPanel.SetActive(false);
    }
}
