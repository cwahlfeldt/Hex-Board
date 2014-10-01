using UnityEngine;
using System.Collections;

public class HexGen : MonoBehaviour {

	//fields
	public Vector3[] verts;
	public Vector2[] uv;
	public int[] tris;
	public Texture texture;
	public Mesh[] meshs;
	// sets up vars that can increase 
	public float negOne = -1f,
				 zero = 0f,
				 one = 1f,
				 negPntFive = -.5f,
				 pntFive = .5f;
	public float floorLevel = 0f;

	
	void Awake () {
		// generates single hexagon
		HexGenerator ();
	}

	void HexGenerator () {
		
		// Gets verts for game board
		tagDistributer ();

		//sets triangles
		tris = new int[]
		{
			1,5,0,
			1,4,5,
			1,2,4,
			2,3,4
		};

		//sets uv corrdinates
		uv = new Vector2[]
		{
			new Vector2(0,0.25f),
			new Vector2(0,0.75f),
			new Vector2(0.5f,1),
			new Vector2(1,0.75f),
			new Vector2(1,0.25f),
			new Vector2(0.5f,0),
		};

		//creates the mesh
		

		//add a mesh filter to the GO the script is attached to; cache it for later
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter> ();

		//add a mesh renderer to the GO the script is attached to
		gameObject.AddComponent<MeshRenderer> ();

		//add collider to object
		MeshCollider meshC = gameObject.AddComponent<MeshCollider> ();
			
		//create a mesh object to pass our data into
		Mesh mesh = new Mesh();
			
		//add our vertices to the mesh
		mesh.vertices = verts;
		//add our triangles to the mesh
		mesh.triangles = tris;
		//add out UV coordinates to the mesh
		mesh.uv = uv;
			
		//make it play nicely with lighting
		mesh.RecalculateNormals();
			
		//set the GO's meshFilter's mesh to be the one we just made
		meshFilter.mesh = mesh;

		// sets shared mesh to this mesh
		meshC.sharedMesh = meshFilter.mesh;

		//UV TESTING
		Material transparent = new Material ( Shader.Find("Transparent/Diffuse") );
			
		renderer.material = transparent;
		renderer.material.mainTexture = texture;
	
	
	}

	// distributes the hexagon to correct 
	void tagDistributer () {

		verts = new Vector3[]
		{
			new Vector3 (negOne, floorLevel, negPntFive),
			new Vector3 (negOne, floorLevel, pntFive),
			new Vector3 (zero, floorLevel, one),
			new Vector3 (one, floorLevel, pntFive),
			new Vector3 (one, floorLevel, negPntFive),
			new Vector3 (zero, floorLevel, negOne)
		};
	}

	public bool HitByRay () {
			return true;
		}
}





















