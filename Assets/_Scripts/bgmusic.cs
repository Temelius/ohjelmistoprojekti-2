using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmusic : MonoBehaviour
{
    // Check if there is multiple music tracks playing
    // dont destroy it when switching scenes
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1) Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
