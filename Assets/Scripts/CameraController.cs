using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 startMousePos;
    private Vector3 presentCamPos;
    private Transform camTransform;
    //カメラの移動量
    [SerializeField, Range(0.1f, 50.0f)]
    private float positionStep = 25.0f;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
            presentCamPos = camTransform.position;
        }

        if (Input.GetMouseButton(0))
        {
            //(移動開始座標 - マウスの現在座標) / 解像度 で正規化
            float x = (startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (startMousePos.y - Input.mousePosition.y) / Screen.height;

            x = x * positionStep;
            y = y * positionStep;

            Vector3 velocity = camTransform.rotation * new Vector3(x, y, 0);
            velocity = velocity + presentCamPos;
            camTransform.position = velocity;
        }
    }
}
