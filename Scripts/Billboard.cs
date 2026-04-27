using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	private void LateUpdate()
	{
		base.transform.LookAt(base.transform.position + this.cam.forward);
	}

	public Transform cam;
}
