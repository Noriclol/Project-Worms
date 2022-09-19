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
    public Vector3 CellSize = new Vector3(10f, 10f, 10f);
    public Vector3Int PointDensity = new Vector3Int(10, 10, 10);

    public float SurfaceLevel = 0;
    public PointData[,,] Points;
    public PointData[,,] ScanCube = new PointData[2,2,2];

    public List<Vector3> VertexBuffer = new List<Vector3>();
    public List<int> IndexBuffer = new List<int>();

    public float heightMultiplier = 10f;
    
    private void OnDrawGizmos()
    {
        if (Points != null)
        {
            foreach (var point in Points)
            {
                if (point.Value >= SurfaceLevel)
                    Gizmos.color = Valid.color;
                
                else
                    Gizmos.color = Invalid.color; 
                
                Gizmos.DrawCube(point.Position, Vector3.one * 0.1f);
            }
        }
    }


    private void Start()
    {
        Points = new PointData[PointDensity.x, PointDensity.y, PointDensity.z];
        GeneratePoints();
        ScanCells();
        var meshGenerator = GetComponent<MeshGenerator>();
        meshGenerator.Generate(VertexBuffer.ToArray(), IndexBuffer.ToArray());

    }
    
    public void ScanCells()
    {
        for (int x = 0; x < PointDensity.x - 1; x++) {
            for (int y = 0; y < PointDensity.y - 1; y++) {
                for (int z = 0; z < PointDensity.z - 1; z++)
                {
                    SampleScanCube(new Vector3Int(x,y,z));
                    int index = CubeIndex();
                    //print($"Index = {index}");
        
                    int[] triangulation = CubeLookupTable.TriTable[index];
                    for (int i = 0; i < triangulation.Length; i++)
                    {
                        //print($"Triangulation{i} = {triangulation[i]}");
                    
                    }

                    for (int i = 0; i < triangulation.Length; i+=3)
                    {
                        if (triangulation[i] == -1)
                        {
                            continue;
                        }
                        
                        //print($"Triangle = {i / 3}");
                        for (int j = 0; j < 3; j++)
                        {
                            //print($"Vertex = {i+j}");
                            //Get PointIndexes
                            int A = CubeLookupTable.EdgeTable[triangulation[i+j], 0];
                            int B = CubeLookupTable.EdgeTable[triangulation[i+j], 1];
                    
                            //Get PointCoords for Indexes
                            Vector3Int aIndex = CubeLookupTable.pointCoords[A];
                            Vector3Int bIndex = CubeLookupTable.pointCoords[B];
                    
                            PointData aPos = ScanCube[aIndex.x, aIndex.y, aIndex.z];
                            PointData bPos = ScanCube[bIndex.x, bIndex.y, bIndex.z];
                    
                            Vector3 vertexPos = (aPos.Position + bPos.Position) / 2f;
                        
                             VertexBuffer.Add(vertexPos);
                        }
                        
                        IndexBuffer.Add(VertexBuffer.Count-3);
                        IndexBuffer.Add(VertexBuffer.Count-1);
                        IndexBuffer.Add(VertexBuffer.Count-2);
                    }
                }
            }
        }
    }
    
    public void SampleScanCube(Vector3Int n)
    {
        ScanCube[0, 0, 0] = Points[n.x, n.y, n.z            ];
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
        int PointCount = 0;
        int CubeIndex = 0;


        for (int x = 0; x < 2; x++) {
            for (int y = 0; y < 2; y++) {
                for (int z = 0; z < 2; z++)
                {
                    if (ScanCube[z,x,y].Value >= SurfaceLevel)
                    {
                        CubeIndex += 1 << PointCount;
                    }
                    PointCount++;
                }
            }
        }
        
        return CubeIndex;
    }
    
    
    //Main Functions
    public float GetPointValue(Vector3 pos)
    {
        //
        return (-pos.y) + (pos.z / 5);
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
