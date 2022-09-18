using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;


//meant to be appended to a gameobject with a 
public class MarchingCubes : MonoBehaviour
{
    public List<GameObject> generatedObjs;
    public GameObject pointRep;

    public Material Valid;
    public Material Invalid;
    //CellGeneration
    [SerializeField]
    public Vector3 CellSize = new Vector3(10f, 10f, 10f);
    public Vector3Int PointDensity = new Vector3Int(10, 10, 10);

    public float SurfaceLevel = 0;
    public PointData[,,] Points;
    public PointData[,,] ScanCube = new PointData[2,2,2];

    
    
    
    
    
    
    
    private void Start()
    {
        Points = new PointData[PointDensity.x, PointDensity.y, PointDensity.z];
        GeneratePoints();
        
    }
    
    
    //Debug Functions
    [ContextMenu("RefreshPoints")]
    private void UpdatePoints()
    {
        //Clear Points;
        ClearPoints();
        
        //populate points;
        foreach (var point in Points)
        {
            var pointmesh = Instantiate(pointRep, point.Position, Quaternion.identity);
            
            if (point.Value >= SurfaceLevel)
                pointmesh.GetComponent<MeshRenderer>().material = Valid;
            else
                pointmesh.GetComponent<MeshRenderer>().material = Invalid;
            
            generatedObjs.Add(pointmesh);
        }
    }
    
    [ContextMenu("ClearPoints")]
    private void ClearPoints()
    {
        foreach (var point in generatedObjs)
        {
            Destroy(point);
        }

        generatedObjs.Clear();
    }

    public void ScanCells()
    {
        for (int x = 0; x < PointDensity.x - 1; x++) {
            for (int y = 0; y < PointDensity.y - 1; y++) {
                for (int z = 0; z < PointDensity.z - 1; z++)
                {
                    SampleScanCube(new Vector3Int(x,y,z));
                    int index = CubeIndex();
                    
                    for (int i = 0; i < 16; i++)
                    {
                        // Vector3Int vertex;
                        // vertex[0] = CubeLookupTable.TriTable[index, i];
                        // vertex[1] = CubeLookupTable.TriTable[index, i];
                        // vertex[2] = CubeLookupTable.TriTable[index, i];
                        
                    }
                    
                }
            }
        }
    }
    
    public void SampleScanCube(Vector3Int n)
    {
        ScanCube[0, 0, 0] = Points[n.x, n.y, n.z];
        ScanCube[1, 0, 0] = Points[n.x + 1, n.y,     n.z    ];
        ScanCube[0, 1, 0] = Points[n.x,     n.y + 1, n.z    ];
        ScanCube[1, 1, 0] = Points[n.x + 1, n.y + 1, n.z    ];
        ScanCube[0, 0, 1] = Points[n.x,     n.y,     n.z + 1];
        ScanCube[1, 0, 1] = Points[n.x + 1, n.y,     n.z + 1];
        ScanCube[0, 1, 1] = Points[n.x,     n.y + 1, n.z + 1];
        ScanCube[1, 1, 1] = Points[n.x + 1, n.y + 1, n.z + 1];
    }
    
    
    
    private int CubeIndex()
    {
        // for (int i = 0; i < 8; i++)
        // {
        //     if (ScanCube[i].Value < SurfaceLevel)
        //     {
        //         
        //     }
        // }

        return 0;
    }
    
    
    //Main Functions
    public float GetPointValue(Vector3 pos)
    {
        return - pos.y;
    }

    public void GeneratePoints()
    {
        Vector3 PointSize;
        PointSize.x = CellSize.x / PointDensity.x;
        PointSize.y = CellSize.y / PointDensity.y;
        PointSize.z = CellSize.z / PointDensity.z;
        //loop Through Space
        for (int x = 0; x < PointDensity.x; x++) {
            for (int y = 0; y < PointDensity.y; y++) {
                for (int z = 0; z < PointDensity.z; z++)
                {
                    Vector3 pos = transform.position + Vector3.Scale((new Vector3(x, y, z)), PointSize);
                    Points[x,y,z].Value = GetPointValue(pos);
                    Points[x,y,z].Position = pos;
                }
            }
        }
    }
    
    
    
    public struct PointData
    {
        public float Value;
        public Vector3 Position;

        public PointData(float value, Vector3 pos)
        {
            Value = value;
            Position = pos;
        }
    }
    
}
