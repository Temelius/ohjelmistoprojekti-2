using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SaveData saveData;

    // Portals
    public GameObject portal1;
    public GameObject portal2;
    public GameObject portal3;
    public GameObject portal4;

    public Animator transition;
    private Collider coll;
    public int GoToSceneNumber;
    private AudioSource skipidii;

    void Start()
    {
        int A = saveData.levelsCompleted;
        switch (A)
        {
            case 0:
                portal1.SetActive(true);
                break;
            case 1:
                portal2.SetActive(true);
                break;
            case 2:
                portal3.SetActive(true);
                break;
            case 3:
                portal4.SetActive(true);
                break;
        }
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
