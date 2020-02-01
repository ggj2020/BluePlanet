using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsRotatePlanet : MonoBehaviour
{
    public Vector3 rotationSpeed;


    void Update()
    {
        transform.localEulerAngles += rotationSpeed * Time.deltaTime;
    }
}
