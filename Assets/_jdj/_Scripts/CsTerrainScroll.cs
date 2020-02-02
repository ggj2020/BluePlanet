using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsTerrainScroll : MonoBehaviour
{
    public List<Transform> terrains;
    public List<TerrainCollider> terrainColliders;


    public Transform cameraTrans;

    private void Awake()
    {
        terrainColliders = new List<TerrainCollider>();
        for (int i = 0; i < terrains.Count; i++)
        {
            terrainColliders.Add(terrains[i].GetComponent<TerrainCollider>());

            Transform tmp = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            tmp.position = terrainColliders[i].bounds.center;
        }
    }

    void Update()
    {
        //41.5f
        for (int i = 0; i < terrains.Count; i++)
        {
            //if (terrainColliders[i].bounds.center.x < cameraTrans.position.x - 41.5f * 0.5f)
            if (terrainColliders[i].bounds.center.x < Statics.playerUnit.position.x - 41.5f * 0.5f)
            {
                terrains[i].position += 1.0f * 41.5f * Vector3.right;
            }
        }
    }
}
