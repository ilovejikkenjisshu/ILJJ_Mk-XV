using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStageManager : MonoBehaviour, Stage
{
    public GameObject readyPanel;
    public GameObject rollDicePanel;

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
        GameManager manager = new GameManager(this);
        StartCoroutine(manager.Run());
        Debug.Log("Game Initialization finished");
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
        rollDicePanel.SetActive(true);
        Button rollDiceButton = rollDicePanel.transform.GetChild(0).transform.GetComponent<Button>();
        yield return new WaitForButtonClicked(rollDiceButton);

        rollDiceButton.gameObject.SetActive(false);
        int number = Random.Range(0, 6) + 1;
        Text numbertext = rollDicePanel.transform.Find("Number").transform.GetComponent<Text>();
        numbertext.text = number.ToString();
        yield return new WaitForSeconds(1);

        rollDicePanel.SetActive(false);
        numbertext.text = "";
        rollDiceButton.gameObject.SetActive(true);
        yield return number;
    }
}