using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Animator transition;
    public Collider coll;
    public int GoToSceneNumber;
    public AudioSource skipidii;

    public void OnTriggerEnter(Collider coll)
    {
        
        StartCoroutine(waitples());
        skipidii = GetComponent<AudioSource>();
        skipidii.Play();
    }

    IEnumerator waitples()
    {
        
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(GoToSceneNumber);
    }

}
