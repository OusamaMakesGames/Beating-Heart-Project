using System;
using UnityEngine;

public class ClothingState : MonoBehaviour
{
	public bool BloodyClothing;

	public void Start()
	{
		this.BloodyClothing = false;
	}

	public void Bloody()
	{
		this.BloodyClothing = true;
	}

	public void Clean()
	{
		this.BloodyClothing = false;
	}

}
