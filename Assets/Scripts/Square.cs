using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Square : MonoBehaviour
{
    public abstract Square GetNext(int index = 0);
}
