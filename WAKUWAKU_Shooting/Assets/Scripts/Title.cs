using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    /// <summary> ボタンを押してからシーンが変わるまでの時間 </summary>
    [SerializeField] float _interval = 1.5f;
    [SerializeField] AudioClip sound1;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
 
    void Update()
    {
        if (Input.GetKeyDown("Fire1_1")) {
            audioSource.PlayOneShot(sound1);
            StartCoroutine(NextScene(_interval));
        }
    }

    IEnumerator NextScene(float _interval)
    {
        yield return new WaitForSeconds(_interval);
        FadeManager.FadeOut(1);
    }
}
