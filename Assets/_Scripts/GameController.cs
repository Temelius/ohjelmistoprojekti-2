using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Animator transition;
    public Collider coll;
    public int GoToSceneNumber;
  

    public void OnTriggerEnter(Collider coll)
    {
        
        StartCoroutine(waitples());
     
    }

    IEnumerator waitples()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(GoToSceneNumber);
    }

}
