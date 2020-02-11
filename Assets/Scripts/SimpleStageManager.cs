using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStageManager : MonoBehaviour, Stage
{
    public GameObject readyPanel;
    public GameObject rollDicePanel;
    public Player playerPrefab;

    private Player[] players;

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
        InitPlayers(4);
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

    public IEnumerator WaitForGettingReady()
    {
        readyPanel.SetActive(true);
        Button readyButton = readyPanel.transform.GetChild(0).transform.GetComponent<Button>();
        yield return new WaitForButtonClicked(readyButton);
        readyPanel.SetActive(false);
    }

    public IEnumerator RollDice()
    {
        // 初期化処理
        Text numbertext = rollDicePanel.transform.Find("Number").transform.GetComponent<Text>();
        numbertext.text = "";
        Button rollDiceButton = rollDicePanel.transform.Find("Roll Dice Button").transform.GetComponent<Button>();
        rollDiceButton.gameObject.SetActive(true);

        rollDicePanel.SetActive(true);
        yield return new WaitForButtonClicked(rollDiceButton);

        rollDiceButton.gameObject.SetActive(false);
        int number = Random.Range(0, 6) + 1;
        numbertext.text = number.ToString();
        yield return new WaitForSeconds(1);

        rollDicePanel.SetActive(false);
        yield return number;
    }
}
