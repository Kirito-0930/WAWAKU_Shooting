using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //KeyDownCheck();
    }

    /// <summary>
    /// どのボタンが押されたかをチェックする
    /// </summary>
    void KeyDownCheck()
    {
        if(Input.anyKeyDown)
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(code)) {
                Debug.Log(code);
                break;
            }
        }
    }
}
