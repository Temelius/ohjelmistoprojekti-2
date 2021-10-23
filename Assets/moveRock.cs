using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRock : MonoBehaviour
{
    private float movementSpeed = 1f;

    void Update()
    {
     
        float horizontalInput = Input.GetAxis("Horizontal");
       
        float verticalInput = Input.GetAxis("Vertical");

    
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 1);
    }
}