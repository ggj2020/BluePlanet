using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CsRotateAsteroids : MonoBehaviour
{
    public Transform[] asteroids;
    public float[] speed;

    public bool isRotateReverse;
    public float totalSpeed;

    void Update()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            asteroids[i].localEulerAngles += Vector3.forward * (isRotateReverse ? speed[i] : -speed[i]) * Time.deltaTime * totalSpeed;
        }
    }


}
