//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class ReverseNormals :MonoBehaviour {
    [SerializeField]bool isReversed = false;
    void Start () {
        if (isReversed) return;
        isReversed = true;
        MeshFilter filter = GetComponent(typeof(MeshFilter)) as MeshFilter;
        if (filter != null) {
            Mesh mesh = filter.mesh;

            Vector3[] normals = mesh.normals;
            for (int i = 0 ; i < normals.Length ; i++)
                normals[i] = -normals[i];
            mesh.normals = normals;

            for (int m = 0 ; m < mesh.subMeshCount ; m++) {
                int[] triangles = mesh.GetTriangles(m);
                for (int i = 0 ; i < triangles.Length ; i += 3) {
                    int temp = triangles[i + 0];
                    triangles[i + 0] = triangles[i + 1];
                    triangles[i + 1] = temp;
                }
                mesh.SetTriangles(triangles, m);
            }
        }
    }
}
