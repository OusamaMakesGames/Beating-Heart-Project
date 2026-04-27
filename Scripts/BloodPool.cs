using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BloodPool : MonoBehaviour
{
	public float TargetSize, ColliderTargetSizeX, ColliderTargetSizeZ;
	public bool Blood = true;
	public bool BloodPrint;
	public bool Grow, Water, Stain;

	public LayerMask groundLayer;

	private PlayerController sakurascript;
	private GameObject player;
	public float add, Cutoff;
	public float x, z;

	void Awake()
	{
		MeshRenderer r = GetComponent<MeshRenderer>();
		if (SceneManager.GetActiveScene().name == "SampleScene")
		{
			player = GameObject.FindWithTag("Player");
			player.GetComponent<EasterEggs>().StoredMaterials.Add(r.material);
		}

	}

	private void Start()
	{
		if (Stain)
		{
			x = UnityEngine.Random.Range(-4.14f, 4.26f);
			z = UnityEngine.Random.Range(0.81955f, 3.98f);
			transform.position = new Vector3(x, transform.position.y, z);
		}
		if (!BloodPrint)
		{
			transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 180f), 0f);
		}
		if (SceneManager.GetActiveScene().name != "SampleScene")
		{
			player = GameObject.FindWithTag("Player");
		}
		sakurascript = player.GetComponent<PlayerController>();
		if (!BloodPrint && !Stain)
		{
			Cutoff = 1f;
		}
		if (Grow)
		{
			gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", 0f);
		}
		if (sakurascript.Club != "Science" && !BloodPrint && !Water && !Stain)
		{
			TargetSize = 0.15f;
			Vector3 currentCenter = gameObject.GetComponent<BoxCollider>().center;
			currentCenter.x = -0.11f;
			currentCenter.z = 0.52f;
			gameObject.GetComponent<BoxCollider>().center = currentCenter;
			ColliderTargetSizeX = 4.3f;
			ColliderTargetSizeZ = 6.04f;
		}
		if (sakurascript.Club == "Science" && !BloodPrint && !Water && !Stain)
		{
			TargetSize = 0.5f;
			Vector3 currentCenter = gameObject.GetComponent<BoxCollider>().center;
			currentCenter.x = 0f;
			currentCenter.z = 0f;
			gameObject.GetComponent<BoxCollider>().center = currentCenter;
			ColliderTargetSizeX = 4.3f;
			ColliderTargetSizeZ = 4.28f;
		}
	}

	private void Update()
	{
		if (Cutoff == 1f && !Water)
		{
			Grow = true;
		}
		if (Grow)
		{
			Cutoff = Mathf.Lerp(Cutoff, TargetSize, 1f * Time.deltaTime);
			gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", Cutoff);
			if (!Water)
			{
				BoxCollider col = gameObject.GetComponent<BoxCollider>();
				Vector3 newSize = col.size;
				newSize.x = Mathf.Lerp(newSize.x, ColliderTargetSizeX, 1f * Time.deltaTime);
				newSize.z = Mathf.Lerp(newSize.z, ColliderTargetSizeZ, 1f * Time.deltaTime);

				col.size = newSize;
			}
			else
			{
				CapsuleCollider col = gameObject.GetComponent<CapsuleCollider>();
				col.radius = Mathf.Lerp(col.radius, 2.487149f, 1f * Time.deltaTime);
			}
		}
		if (Cutoff < TargetSize + 0.01f)
		{
			Grow = false;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("RightFoot") && !BloodPrint && !Water)
		{
			if (!Stain)
			{
				if (sakurascript.Club == "Science")
				{
					sakurascript.BloodTimer1 = 10f;
				}
				else
				{
					sakurascript.BloodTimer1 = 15f;
				}
			}
			else
			{
				sakurascript.BloodTimer1 = 5f;
			}
		}
		else if (other.CompareTag("LeftFoot") && !BloodPrint && !Water)
		{
			if (!Stain)
			{
				if (sakurascript.Club == "Science")
				{
					sakurascript.BloodTimer2 = 10f;
				}
				else
				{
					sakurascript.BloodTimer2 = 15f;
				}
			}
			else
			{
				sakurascript.BloodTimer2 = 5f;
			}
		}
	}
}
