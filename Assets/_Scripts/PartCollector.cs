using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartCollector : MonoBehaviour
{
    private GameController gameController;
 

    public Animator transition;
    private Collider coll;
    public int GoToSceneNumber;
    private AudioSource skipidii;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void OnTriggerEnter(Collider coll)
    {

        StartCoroutine(waitples());
        skipidii = GetComponent<AudioSource>();
        skipidii.Play();

    }

    IEnumerator waitples()
    {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        gameController.saveData.levelsCompleted += 1;
      
        print(gameController.saveData.levelsCompleted);
        SceneManager.LoadScene(GoToSceneNumber);
    }

}
