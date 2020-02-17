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
            Vector3 pos = value.transform.position + this.PosOffset;
            pos.z = -1f;
            this.transform.position = pos;
        }
    }
    public Vector3 PosOffset { get; set; }

    public IEnumerator MoveTo(Square to)
    {
        Vector3 start = this.transform.position;
        float diffx = to.transform.position.x + this.PosOffset.x - this.transform.position.x;
        float diffy = to.transform.position.y + this.PosOffset.y - this.transform.position.y;

        const int animationFrames = 15;

        // ベジェ曲線のパラメータ
        float x2 = 0.17f;
        float x3 = 0.83f;
        for (int i = 0; i < animationFrames; i++) {
            float t = (float)i / animationFrames;

            // ベジェ曲線 0 <= diff < 1
            float diff = Mathf.Pow(t, 3) + 3 * Mathf.Pow(t, 2) * (1 - t) * x3 + 3 * t * Mathf.Pow(1 - t, 2) * x2;

            // スタート位置から計算
            Vector3 pos = start;
            pos.x += diffx * diff;
            pos.y += diffy * diff;
            this.transform.position = pos;

            yield return null;
        }
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
