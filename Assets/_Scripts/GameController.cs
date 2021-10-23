using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Collider coll;
    public int GoToSceneNumber;
    public AudioSource skipidii;
    

    public void OnTriggerEnter(Collider coll)
    {
        skipidii = GetComponent<AudioSource>();
        skipidii.Play();
        StartCoroutine(waitples());
        SceneManager.LoadScene(GoToSceneNumber);
    }

    IEnumerator waitples()
    {
        yield return new WaitForSeconds(4);
    }

}
