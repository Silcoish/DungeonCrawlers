using UnityEngine;
using System.Collections;

public class WeaponCollider : MonoBehaviour {

	//public PolygonCollider2D right;

	void Start()
	{
		//CreateMesh(right);
	}

	public void CreateMesh(PolygonCollider2D col)
	{
		GameObject temp = col.gameObject;
		MeshFilter meshFilter;
		MeshRenderer meshRenderer;
		if (temp.GetComponent<MeshFilter>() != null)
			meshFilter = temp.GetComponent<MeshFilter>();
		else
			meshFilter = temp.AddComponent<MeshFilter>();

		if (col.GetComponent<MeshRenderer>() != null)
			meshRenderer = temp.GetComponent<MeshRenderer>();
		else
			meshRenderer = temp.AddComponent<MeshRenderer>();

		Vector3[] points = new Vector3[col.points.Length];
		for(int i = 0; i < col.points.Length; i++)
		{
			points[i] = new Vector3(col.points[i].x, col.points[i].y, -1);
		}
		int[] triangles = new int[(col.points.Length - 1) * 3];
		int lastNum = 0;
		for(int i = 0; i < ((triangles.Length - 1) / 3); i++)
		{
			if(i == 0)
			{
				triangles[0] = 0;
				triangles[1] = 1;
				triangles[2] = 2;
				lastNum = 2;
			}
			else
			{
				triangles[i * 3] = lastNum;
				triangles[i * 3 + 1] = lastNum + 1;
				triangles[i * 3 + 2] = 0;
				lastNum++;
			}
		}

		Mesh mesh = new Mesh();
		mesh.Clear();
		mesh.vertices = points;
		mesh.triangles = triangles;// new int[] { 0, 1, 2, 2, 3, 0, 3, 4, 0, 0, 0, 0 };//triangles;
		meshFilter.mesh = mesh;

		Material mat = new Material(Shader.Find("Sprites/Default"));
		mat.color = new Color(0, 0.66f, 0, 0.9f);
		meshRenderer.material = mat;
	}
}
