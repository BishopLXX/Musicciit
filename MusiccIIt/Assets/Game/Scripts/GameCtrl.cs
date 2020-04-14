using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    public AudioSource audio;
    public float cubeMoveTime;

    // Start is called before the first frame update
    void Start()
    {
        audio.Stop();
        Invoke("openKor", cubeMoveTime);
    }

    void openKor()
    {
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
