using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject cube;
    public GameObject cylinder;
    public GameObject sphere;

    public Material red;
    public Material green;
    public Material blue;

    public static int score;
    public static int missed;

    public static int round;

    private float T = 1.0f;
    private bool next;

    private float mtime;

    private const float x_min = 1;
    private const float x_max = 15 + 1;
    private const float y_init = -8;

    // Start is called before the first frame update
    void Start()
    {
        score = missed = 0;
        mtime = 0;
        round = 1;
        next = true;
        red = Resources.Load<Material>("Materials/Red");
        green = Resources.Load<Material>("Materials/Green");
        blue = Resources.Load<Material>("Materials/Blue");
        cube = Resources.Load<GameObject>("Prefabs/Cube");
        cylinder = Resources.Load<GameObject>("Prefabs/Cylinder");
        sphere = Resources.Load<GameObject>("Prefabs/Sphere");
        InvokeRepeating("createObject", 0.5f, T);
    }

    // Update is called once per frame
    void Update()
    {
        mtime += Time.deltaTime;
        if (score >= 20) {
            if (round < 3 && next) {
                Invoke("reset", 3);
                CancelInvoke("createObject");
                Invoke("goNext", 3);
                next = false;
            } else CancelInvoke("createObject");
        } else next = true;
    }

    void changeColor(ref GameObject arg) {
        int color = Random.Range(0, 3);
        if (color == 0) arg.GetComponent<Renderer>().material = red;
        else if (color == 1) arg.GetComponent<Renderer>().material = green;
        else arg.GetComponent<Renderer>().material = blue;
    }

    float getPostionX() {
        float x = Random.Range(x_min, x_max);
        if (Random.Range(0, 2) == 0) return x;
        else return -x;
    }

    void createObject() {
        GameObject sth;
        int c = Random.Range(0, 3);
        if (c == 0) sth = cube;
        else if (c == 1) sth = cylinder;
        else sth = sphere;
        sth.transform.position = new Vector3(getPostionX(), y_init, 0);
        changeColor(ref sth);
        sth = GameObject.Instantiate(sth);
        sth.AddComponent<SSActionManager>();
    }

    void reset() {
        score = missed = 0;
        round += 1;
        T /= 2;
    }

    void goNext() {
        InvokeRepeating("createObject", 0, T);
    }


}
