using UnityEngine;
using System.Collections.Generic;

public class NormalSquare : Square
{
    public List<Square> nextSquares;

    public override List<Square> GetNext()
    {
        return nextSquares;
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
