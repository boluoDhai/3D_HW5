using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{
    public float ori_x;
    public float ori_y;
    public float highest_x;
    public float highest_y;

    private float a;
    private float b;
    private float c;

    private float mtime;

    private float x_min;
    private float x_max;
    private float y_min;
    private float y_max;

    // Start is called before the first frame update
    void Start()
    {
        mtime = 0;
        x_min = 0;
        x_max = 10 + 1;
        y_min = 5;
        y_max = 20 + 1;
        ori_x = this.gameObject.transform.position.x;
        ori_y = this.gameObject.transform.position.y;
        highest_x = Random.Range(x_min, x_max);
        if (Random.Range(0, 2) == 0) highest_x = -highest_x;
        highest_y = Random.Range(y_min, y_max);
        while(Mathf.Abs(highest_x - ori_x) < 1) {
            highest_x = Random.Range(x_min, x_max);
        }

        float x2 = 2 * highest_x - ori_x, y2 = ori_y;
        float temp = (highest_y - y2) / (highest_x - x2);
        a = (ori_y - temp * ori_x - y2 + temp * x2) / (ori_x * ori_x - ori_x * (highest_x + x2) - x2 * x2 + x2 * (highest_x + x2));
        b = -2 * a * highest_x;
        c = y2 - a * (x2 * x2) - b * x2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.gameObject.transform.position;
        if (ori_x < highest_x) pos.x += Time.deltaTime;
        else pos.x -= Time.deltaTime;
        float x = pos.x;
        pos.y = a * Mathf.Pow(x, 2) + b * x + c;
        //Debug.Log("x: " + x + ", y: " + pos.y + ", ori_x: " + ori_x + ", ori_y: " + ori_y + ", h_x: " + highest_x + ", h_y: " + highest_y + ", a: " + a + ", b: " + b + ", c: " + c);
        this.gameObject.transform.position = pos;
        mtime += Time.deltaTime;
        if (pos.y < -20) {
            Destroy(this.gameObject);
            Player.missed += 1;
        }
        if (Player.score >= 20) Destroy(this.gameObject);
    }

    void OnMouseDown() {
        Destroy(this.gameObject);
        Player.score += 1;
    }
}
