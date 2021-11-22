using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalOnTrigger : MonoBehaviour
{
    public int GoToSceneNumber;
    private Animator transition;
    private AudioSource skipidii;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        yield return new WaitForSeconds(3);


        SceneManager.LoadScene(GoToSceneNumber);
    }
}
