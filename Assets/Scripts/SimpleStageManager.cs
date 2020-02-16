using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SimpleStageManager : MonoBehaviour, Stage
{
    public GameObject readyPanel;
    public GameObject rollDicePanel;
    public Player playerPrefab;
    public List<Square> startSquares;
    public DestSelector destSelectorPrefab;

    private List<Player> players;
    private List<Square> squares;

    private Camera cam;

    List<Player> Stage.GetPlayers()
    {
        return players;
    }

    List<Square> Stage.GetSquares()
    {
        return squares;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitPlayers(4);
        InitSquares();
        cam = Camera.main;
        GameManager manager = new GameManager(this);
        StartCoroutine(manager.Run());
        Debug.Log("Game Initialization finished");
    }

    private void InitPlayers(int playerNum)
    {
        players = new List<Player>(playerNum);
        for(int i = 0; i < playerNum; i++)
        {
            players.Add((Player) Instantiate(playerPrefab));
            players[i].Name = "player" + i.ToString();
            players[i].Pos = startSquares[i % startSquares.Count];
        }
    }

    private void InitSquares()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Square");

        squares = new List<Square>(objs.GetLength(0));
        for (int i = 0; i < objs.GetLength(0); i++)
        {
            squares.Add(objs[i].GetComponent<Square>());
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

    [System.Serializable]
    public class DestSelectEvent : UnityEvent<TreeNode<Square>>
    {
    }

    private IEnumerator SelectDest(Player player, int dicenum)
    {
        TreeNode<Square> root = new TreeNode<Square>(player.Pos);
        // destOptionsは「到着先のマスを指す木の葉ノード」のリスト
        List<TreeNode<Square>> destOptions = new List<TreeNode<Square>>();

        // 多分木をつくる
        GenerateDestTree(destOptions, root, dicenum);

        // ここでdestOptionsからプレイヤーに行き先を選ばせる
        DestSelectEvent OnDestSelected = new DestSelectEvent();
        TreeNode<Square> nodeptr = null;
        bool selected = false;
        OnDestSelected.AddListener((TreeNode<Square> dest) => {
            nodeptr = dest;
            selected = true;
        });

        // DestSelectorを生成
        List<DestSelector> destSelectors = new List<DestSelector>();
        foreach (TreeNode<Square> dest in destOptions)
        {
            DestSelector temp = (DestSelector) Instantiate(destSelectorPrefab);
            temp.transform.position = dest.value.transform.position;
            temp.transform.Translate(0, 0, -1f);
            temp.dest = dest;
            temp.OnDestSelected = OnDestSelected;
            destSelectors.Add(temp);
        }
        while (!selected) yield return null;
        foreach (DestSelector ds in destSelectors) Destroy(ds.gameObject);

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

        Coroutine camMove = StartCoroutine(RunCameraTrackPlayer(player));
        // 道順をもとにプレイヤーを歩かせる
        Stack<Square> directions = (Stack<Square>)selectDest.Current;
        while (directions.Count > 0) {
            IEnumerator moveTo = player.MoveTo(directions.Pop());
            yield return moveTo;
        }
        StopCoroutine(camMove);

        yield return null;
    }

    private IEnumerator PlayDiceAnimation(int min, int max, Text numbertext)
    {
        int number;
        while(true){
            number = UnityEngine.Random.Range(min, max + 1);
            numbertext.text = number.ToString();
            yield return number;
        }
    }

    public void MoveCamera(Vector3 pos)
    {
        cam.transform.position = new Vector3(pos.x,pos.y,-10);
    }

    private IEnumerator RunCameraTrackPlayer(Player player)
    {
        while(true){
            MoveCamera(player.transform.position);
            yield return null;
        }
    }
}
