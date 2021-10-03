using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Collider coll;

    public int GoToSceneNumber;


    public void OnTriggerEnter(Collider coll)
    {
        SceneManager.LoadScene(GoToSceneNumber);
    }

}
