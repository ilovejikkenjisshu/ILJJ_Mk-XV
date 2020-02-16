using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Square : MonoBehaviour
{
    public GameObject guideArrow;
    public List<Square> nextSquares;
    public List<Square> GetNext()
    {
        return nextSquares;
    }
    private void GenerateGuideArrow()
    {
        foreach(Square sqr in GetNext())
        {
            GameObject arrow = Instantiate(guideArrow,transform.position, Quaternion.identity);
            Vector3 diff = (sqr.transform.position - arrow.transform.position);
            arrow.transform.rotation = Quaternion.FromToRotation (Vector3.up, diff);
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, sqr.transform.position, 1f);
        }
    }
    protected virtual void Start()
    {
        GenerateGuideArrow();
    }

    public virtual void execEvent()
    {

    }
}
