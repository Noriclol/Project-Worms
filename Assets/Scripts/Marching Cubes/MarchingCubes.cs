using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//meant to be appended to a gameobject with a 
public class MarchingCubes : MonoBehaviour
{
    //CellGeneration
    public float CellSize = 10f;
    public int PointDensity = 10;

    public float SurfaceLevel = 0;
    private float[,,] CellPoints;

    private void Start()
    {
        CellPoints = new float[PointDensity, PointDensity, PointDensity];
        GeneratePoints();
    }


    public float GetPointValue(Vector3 pos)
    {
        return - pos.z;
    }

    public void GeneratePoints()
    {
        float PointSize = CellSize / PointDensity;
        
        //loop Through Space
        for (int x = 0; x < PointDensity; x++) {
            for (int y = 0; y < PointDensity; y++) {
                for (int z = 0; z < PointDensity; z++)
                {
                    Vector3 pos = new Vector3(x, y, z) * PointSize;
                    CellPoints[x,y,z] = GetPointValue(pos);
                }
            }
        }
    }
    
}
