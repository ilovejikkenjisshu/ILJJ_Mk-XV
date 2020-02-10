using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightManager : MonoBehaviour
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
        float x = Mathf.Sin(t);
        float y = Mathf.Cos(t);

        Vector3 pos = new Vector3(x, y, -10);
        gameObject.transform.position = pos;
        gameObject.transform.LookAt(new Vector3(0, 0, 8f));

        t += 0.01f;
    }
}
