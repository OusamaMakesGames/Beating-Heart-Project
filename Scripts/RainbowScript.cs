using System;
using UnityEngine;

public class RainbowScript : MonoBehaviour
{
	private void Start()
	{
		this.material.color = this.Colors[0];
	}

	private void Update()
	{
		this.material.color = Vector4.MoveTowards(this.material.color, this.Colors[this.ID], Time.deltaTime);
		if (this.material.color == this.Colors[this.ID])
		{
			this.ID++;
			if (this.ID > this.Colors.Length - 1)
			{
				this.ID = 0;
			}
		}
	}

	public Material material;

	public Color[] Colors;

	public int ID;
}
