using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BloodSpawner : MonoBehaviour
{
	public GameObject BloodPool;
	public Transform BloodParent;
	public Vector3 position;
	public GameObject LastPool;
	public bool CanSpawn;
	public int PoolsSpawned;
	public float add = 0.01f;
	public LayerMask groundLayer, blockingLayers;

	public void Start()
	{
		this.BloodParent = GameObject.Find("BloodParent").transform;
	}

	private void Update()
	{
		position = base.transform.position;
		this.CanSpawn = true;
			RaycastHit hit;
			
			if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, groundLayer) && this.CanSpawn & this.PoolsSpawned < 1)
			{
				Vector3 direction = (hit.point - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, hit.point);
				if (!Physics.Raycast(transform.position, direction, distance, blockingLayers))
            	{
					GameObject gameObject = Instantiate<GameObject>(this.BloodPool, new Vector3(position.x, hit.point.y + add, position.z), Quaternion.identity);
					gameObject.transform.parent = this.BloodParent;
					PoolsSpawned += 1;
					return;
				}
			}
		}
}
