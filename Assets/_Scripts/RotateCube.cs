using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    private void UpdateRotateCube()
    {
        transform.Rotate(1f, 10f * Time.deltaTime, 1f, Space.Self); 
    }

}
