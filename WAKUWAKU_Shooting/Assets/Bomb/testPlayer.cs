using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    public float speed = 3.0f;

    [SerializeField]
    private GameObject _bullet = null;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) { transform.Translate(0,0, speed); }
        if (Input.GetKey(KeyCode.DownArrow)) { transform.Translate(0, 0, -speed); }
        if (Input.GetKey(KeyCode.RightArrow)) { transform.Translate(speed, 0, 0); }
        if (Input.GetKey(KeyCode.LeftArrow)) { transform.Translate(-speed, 0, 0); }
        if (Input.GetKey(KeyCode.Z)) { Instantiate(_bullet, gameObject.transform.position, gameObject.transform.localRotation); }
    }
}
