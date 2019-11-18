using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Player : MonoBehaviour
{
    public string GetName()
    {
        return "ILJJ";
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager manager = new GameManager(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
