using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    /// <summary> ボタンを押してからシーンが変わるまでの時間 </summary>
    [SerializeField] float _interval = 1.5f;
    [SerializeField] AudioClip sound1;
    AudioSource _se;
    AudioSource _bgm;

    void Start()
    {
        _se = GetComponents<AudioSource>()[0];
        _bgm = GetComponents<AudioSource>()[1];
    }
 
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            _bgm.Stop();Debug.Log("aaa");
            _se.PlayOneShot(sound1);
            StartCoroutine(NextScene(_interval));
        }
    }

    IEnumerator NextScene(float _interval)
    {
        yield return new WaitForSeconds(_interval);
        FadeManager.FadeOut(1);
    }
}
