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
        yield return new WaitForButtonClicked(rollDiceButton);

        rollDiceButton.gameObject.SetActive(false);
        int number = UnityEngine.Random.Range(min, max + 1);
        numbertext.text = number.ToString();
        yield return new WaitForSeconds(1);

        rollDicePanel.SetActive(false);
        yield return number;
    }

    private void GenerateDestTree(List<TreeNode<Square>> destOptions, TreeNode<Square> node, int dicenum, int now = 0)
    {
        if (now == dicenum) {
            foreach (TreeNode<Square> value in destOptions) {
                if (node.value == value.value) return;
            }
            destOptions.Add(node);
            return;
        }
        for (int i = 0; i < node.value.GetNext().Count; i++) {
            // 進んできた方へ戻るマスは除いて木を作る
            if (node.Parent != null && node.value.GetNext()[i] == node.Parent.value) continue;
            TreeNode<Square> newnode = new TreeNode<Square>(node.value.GetNext()[i]);
            node.Add(newnode);
            GenerateDestTree(destOptions, newnode, dicenum, now + 1);
        }
    }

    private IEnumerator SelectDest(Player player, int dicenum)
    {
        TreeNode<Square> root = new TreeNode<Square>(player.Pos);
        // destOptionsは「到着先のマスを指す木の葉ノード」のリスト
        List<TreeNode<Square>> destOptions = new List<TreeNode<Square>>();

        // 多分木をつくる
        GenerateDestTree(destOptions, root, dicenum);

        Debug.Log("destOptions: " + destOptions.Count.ToString());
        // ここでdestOptionsからプレイヤーに行き先を選ばせる
        // nodeを返してもらう
        // 注意！到着先は同じだけど辿る道順が異なるものもある
        //yield return new WaitForDestSelected(destOptions);

        // 選択させるのにいいのがまだ思いついていないのでランダムに選ばせる
        TreeNode<Square> nodeptr = destOptions[UnityEngine.Random.Range(0, destOptions.Count - 1)];

        // 木を逆に辿りながらstackにpushしていくことで進む道順を作成する
        Stack<Square> directions = new Stack<Square>();
        while (nodeptr.Parent != null) {
            directions.Push(nodeptr.value);
            nodeptr = nodeptr.Parent;
        }
        yield return directions;
    }

    public IEnumerator MovePlayer(Player player, int dicenum)
    {
        // 到着先を選ばせて、道順を返してもらう
        IEnumerator selectDest = SelectDest(player, dicenum);
        yield return selectDest;

        // 道順をもとにプレイヤーを歩かせる
        Stack<Square> directions = (Stack<Square>)selectDest.Current;
        while (directions.Count > 0) {
            IEnumerator moveTo = player.MoveTo(directions.Pop());
            yield return moveTo;
        }
        yield return null;
    }
}
