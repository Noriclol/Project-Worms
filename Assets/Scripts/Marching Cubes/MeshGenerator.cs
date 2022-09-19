using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    public Mesh mesh;
    
    private Vector3[] vertices;
    private int[] triangles;
    
    public void Generate(Vector3[] verticesInput, int[] indicesInput)
    {
        mesh = new Mesh();
        mesh.Clear();
        
        mesh.vertices = verticesInput;
        mesh.triangles = indicesInput;
        
        mesh.RecalculateNormals();
        
        GetComponent<MeshFilter>().mesh = mesh;
    }
    
}
