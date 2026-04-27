using System;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
	FootstepsScript steps;
	public LayerMask groundMask;
	public int submesh;

	public void Start()
	{
		steps = this.gameObject.GetComponent<FootstepsScript>();
	}

	public void Update()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 2f, groundMask) && !hit.collider.gameObject.name.Contains("J_Bip"))
		{
			int count = hit.collider.GetComponent<Renderer>().sharedMaterials.Length;

			if (count == 1)
			{
				if (hit.collider.GetComponent<Renderer>().sharedMaterial.name == "Grass" || hit.collider.GetComponent<Renderer>().sharedMaterial.name == "plant")
				{
					steps.clips = steps.grassclips;
				}
				else if (hit.collider.GetComponent<Renderer>().sharedMaterial.name.Contains("Exterior"))
				{
					steps.clips = steps.floorclips;
				}
				else if (hit.collider.GetComponent<Renderer>().sharedMaterial.name.Contains("SHW"))
				{
					steps.clips = steps.tileclips;
				}
				else if (hit.collider.GetComponent<Renderer>().sharedMaterial.name.Contains("Wood"))
				{
					steps.clips = steps.woodclips;
				}
				else if (hit.collider.GetComponent<Renderer>().sharedMaterial.name.Contains("Metal"))
				{
					steps.clips = steps.metalclips;
				}
				else if (hit.collider.GetComponent<Renderer>().sharedMaterial.name.Contains("Water"))
				{
					steps.clips = steps.liquidclips;
				}
				else if (hit.collider.GetComponent<Renderer>().sharedMaterial.name.Contains("Blood") || hit.collider.GetComponent<Renderer>().sharedMaterial.name.Contains("Shoe") || hit.collider.GetComponent<Renderer>().sharedMaterial.name.Contains("Stain"))
				{
					steps.clips = steps.bloodclips;
				}
				else if (hit.collider.GetComponent<Renderer>().sharedMaterials[submesh].name.Contains("bed"))
				{
					steps.clips = steps.rockclips;
				}
			}
			else
			{
				MeshCollider col = hit.collider as MeshCollider;

				Mesh mesh = col.sharedMesh;
				int tri = hit.triangleIndex * 3;

				submesh = 0;

				for (int i = 0; i < mesh.subMeshCount; i++)
				{
					int len = mesh.GetTriangles(i).Length;
					if (tri < len)
					{
						submesh = i;
						break;
					}
					tri -= len;
				}

				if (hit.collider.GetComponent<Renderer>().sharedMaterials[submesh].name.Contains("bed"))
				{
					steps.clips = steps.rockclips;
				}
				else if (hit.collider.GetComponent<Renderer>().sharedMaterials[submesh].name.Contains("gravel"))
				{
					steps.clips = steps.gravelclips;
				}
			}
		}
	}
}
