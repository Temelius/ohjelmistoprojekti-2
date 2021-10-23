using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerCollision : MonoBehaviour
{
    public ParticleSystem part;
    public int GotoSceneNumber;


    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player") 
        SceneManager.LoadScene(GotoSceneNumber);
    }
}
