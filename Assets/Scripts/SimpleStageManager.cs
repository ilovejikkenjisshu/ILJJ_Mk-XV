using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStageManager : MonoBehaviour, Stage
{
    public GameObject readyPanel;
    public GameObject rollDicePanel;
    public Player playerPrefab;
    public List<Square> startSquares;

    private Player[] players;
    private Square[] squares;

    Player[] Stage.GetPlayers()
    {
        return players;
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

    private void InitPlayers(int playerNum)
    {
        players = new Player[playerNum];
        for(int i = 0; i < playerNum; i++)
        {
            players[i] = (Player) Instantiate(playerPrefab);
            players[i].Name = "player" + i.ToString();
            players[i].Pos = startSquares[i % startSquares.Count];
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

    public IEnumerator RollDice(int min, int max)
    {
        // 初期化処理
        Text numbertext = rollDicePanel.transform.Find("Number").transform.GetComponent<Text>();
        numbertext.text = "";
        Button rollDiceButton = rollDicePanel.transform.Find("Roll Dice Button").transform.GetComponent<Button>();
        rollDiceButton.gameObject.SetActive(true);

        rollDicePanel.SetActive(true);

        IEnumerator diceAnimation = PlayDiceAnimation(min,max,numbertext);
        StartCoroutine(diceAnimation);
        yield return new WaitForButtonClicked(rollDiceButton);
        StopCoroutine(diceAnimation);
        int number = (int)diceAnimation.Current;

        rollDiceButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        rollDicePanel.SetActive(false);
        yield return number;
    }

    private IEnumerator PlayDiceAnimation(int min, int max, Text numbertext)
    {
        int number;
        while(true){
            number = Random.Range(min, max + 1);
            numbertext.text = number.ToString();
            yield return number;
        }
    }
}
