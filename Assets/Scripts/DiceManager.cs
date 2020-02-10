using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0.3f * Mathf.Sin(0.19f * t);
        float y = 0.7f * Mathf.Sin(0.29f * t);
        float z = 0.13f * Mathf.Sin(0.37f * t);

        gameObject.transform.Rotate(new Vector3(x, y, z));

        t += 0.01f;
    }
}
