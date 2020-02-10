using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStageManager : MonoBehaviour, Stage
{
    public GameObject readyPanel;

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
        StartCoroutine(manager.GameCoroutine());
        Debug.Log("Game Initialization finished");
    }

    public IEnumerator WaitForGetReady()
    {
        readyPanel.SetActive(true);
        Button readyButton = readyPanel.transform.GetChild(0).transform.GetComponent<Button>();
        yield return new WaitForButtonClick(readyButton);
        readyPanel.SetActive(false);
    }
}
