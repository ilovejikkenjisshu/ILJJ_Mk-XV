using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour
{
    public string Name { get; set; }
    private Square pos;
    public Square Pos
    {
        get { return this.pos; }
        set
        {
            this.pos = value;
            this.transform.position = value.transform.position;
            this.transform.Translate(0, 0, -1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    //    GameManager manager = new GameManager(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
