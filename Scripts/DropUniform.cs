using System;
using UnityEngine;

public class DropUniform : MonoBehaviour
{

	public PickUpUniform wpscript;

	public Animator sakuraAnimator, jayAnimator;

	public void Dropped()
	{
		this.sakuraAnimator.SetLayerWeight(1, wpscript.currentWeight);
	}
}
