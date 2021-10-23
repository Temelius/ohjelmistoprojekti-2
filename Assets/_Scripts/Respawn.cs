using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public AudioSource skipidii;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        skipidii = GetComponent<AudioSource>();
        skipidii.Play();
        player.transform.position = respawnPoint.transform.position;
    }
}
