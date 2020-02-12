using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStageManager : MonoBehaviour, Stage
{
    public GameObject readyPanel;
    public Player playerPrefab;
    public List<Square> startSquares;

    private Player[] players;

    Player[] Stage.GetPlayers()
    {
        return players;
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

    public IEnumerator WaitForGettingReady()
    {
        readyPanel.SetActive(true);
        Button readyButton = readyPanel.transform.GetChild(0).transform.GetComponent<Button>();
        yield return new WaitForButtonClicked(readyButton);
        readyPanel.SetActive(false);
    }
}
