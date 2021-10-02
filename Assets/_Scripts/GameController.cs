using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Collider coll;

    public void OnTriggerEnter(Collider coll)
    {
        SceneManager.LoadScene("Menu");
    }
}
