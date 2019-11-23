using UnityEngine;
using System.Collections.Generic;

public class NormalSquare : Square
{
    public List<Square> next;

    public override Square GetNext(int index = 0)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
