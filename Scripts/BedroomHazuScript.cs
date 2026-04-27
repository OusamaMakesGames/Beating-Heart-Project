using UnityEngine;

public class BedroomHazuScript : MonoBehaviour
{
    public HeadController Head;
    public ParticleSystem Hearts;
    public Animator SakuraAnimator;
    public GameObject Poster;
    public bool Looking;
    public float currentWeight;

    void Update()
    {
        if (Head.lookObj == Poster.transform && !Looking)
        {
            Hearts.Play();
            Looking = true;
        }
        else if (!Head.lookObj == Poster.transform && Looking)
        {
            Hearts.Stop();
            Looking = false;
        }
        if (Looking && currentWeight != 1f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 1f, 3f * Time.deltaTime);
			SakuraAnimator.SetLayerWeight(4, currentWeight);
		}
		if (!Looking && currentWeight != 0f)
		{
			currentWeight = Mathf.MoveTowards(currentWeight, 0f, 3f * Time.deltaTime);
			SakuraAnimator.SetLayerWeight(4, currentWeight);
		}
    }
}
