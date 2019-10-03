using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private GUIStyle style;

    // Start is called before the first frame update
    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 40;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width / 8, Screen.height / 6, 200, 100), "Round: " + Player.round + "\nScore: " + Player.score + "\nMissed: " + Player.missed, style);
        if(Player.round >= 3 && Player.score >= 20) {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "You win!", style);
        }else if(Player.score >= 20) {
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 200, 200), "Next round will start after 3s", style);
        }
    }
}
