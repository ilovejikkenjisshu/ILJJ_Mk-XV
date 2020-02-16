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

    public IEnumerator MoveTo(Square to)
    {
        yield return new WaitForSeconds(0.25f);
        float diffx = to.transform.position.x - this.transform.position.x;
        float diffy = to.transform.position.y - this.transform.position.y;
        this.transform.Translate(diffx / 2, diffy / 2, 0);
        yield return new WaitForSeconds(0.25f);
        this.Pos = to;
        yield return null;
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
