using UnityEngine;
using System.Collections;
using System.Linq;


public class InvertMesh : MonoBehaviour {

	void Start () 
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;

		// Black transparent cube around the player camera becomes visable upon death
		// Inverting the cube's mesh makes the cube visible from the inside
		mesh.triangles = mesh.triangles.Reverse().ToArray();
	}

}
