using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SaveData saveData;

    public List<GameObject> objList;


    // Splash screen transition
    // Crossfade on editor
    public Animator transition;
    private Collider coll;
    // What scene should we go to
    public int GoToSceneNumber;

    // skipidii sound
    private AudioSource skipidii;

    void Start()
    {
        // Switch between save points and set portal active state accordingly
        int A = saveData.levelsCompleted;
        if (objList.Count > 0) {
        objList[A].SetActive(true);
        }
        // console debug
        print(A);
    }

    private void Awake()
    {
        //Use the static method SaveData.LoadFile() to attempt to load savedata.json, which is stored
        //in StreamingAssets.
        SaveData tempData = SaveData.LoadFile();
        //If the load failed, then create a new instance of SaveData
        if (tempData == null)
        {
            saveData = new SaveData();
        }
        //If the load succeeded, then use the loaded file
        else
        {
            saveData = tempData;
        }
    }

    // When player hits trigger, switch scene and 
    // play skipidii sound effect
    public void OnTriggerEnter(Collider coll)
    {
        
        StartCoroutine(SceneTransition());
        skipidii = GetComponent<AudioSource>();
        skipidii.Play();
    }

    // Wait for 3 seconds for the animation to start
    // and then switch scene
    IEnumerator SceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(GoToSceneNumber);
    }

    private void OnDestroy()
    {
        //If the saveOnDestroy flag is set, data will automatically be saved.  This can be
        //triggered by scene changes, application ending, or manual deletion of the GameObject
        if (saveData.saveOnDestroy)
        {
            saveData.SaveToDisk();
        }
    }
}
