using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class BloodRemover : MonoBehaviour
{
    public float rotationSpeed;
    public float moveSpeed, CleanSpeed;
    public Transform bloodParent;

    private Quaternion lookRotation;
    private Vector3 direction;
    private NavMeshAgent navMeshAgent;
    public BloodyUniform PickUpScript;
    public bool OnTopOfBlood;
    public Transform currentBlood;

    public int BloodCleaned;

    public bool Full, FullTextOn, Robot;

    private GameObject sakura;

    private PlayerController sakurascript;

    public Prompt PromptScript;

    public int MaxBloodCleaned;

    private void Start()
    {
        if (Robot)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        else
        {
            PromptScript = transform.parent.GetComponent<Prompt>();
        }
        sakura = GameObject.FindWithTag("Player");
        sakurascript = sakura.GetComponent<PlayerController>();
        CleanSpeed = 0.3f;
    }

    private void Update()
    {
        if (currentBlood == null)
        {
            this.transform.parent.GetComponent<MoppingScript>().ChangingColor = false;
        }
        if (sakurascript.Club == "Science")
        {
            MaxBloodCleaned = 100;
        }
        else
        {
            MaxBloodCleaned = 50;
        }
        if (PickUpScript.PickedUp && Robot)
        {
            navMeshAgent.enabled = false;
            return;
        }
        if (Robot)
        {
            navMeshAgent.enabled = true;
        }

        // Find the closest BloodPrefab with tag "Blood"
        GameObject closestBlood = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform child in bloodParent)
        {
            if (child.CompareTag("Blood"))
            {
                float dist = Vector3.Distance(transform.position, child.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestBlood = child.gameObject;
                }
            }
        }

        if (closestBlood != null && !Full && Robot)
        {
            direction = (closestBlood.transform.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            navMeshAgent.SetDestination(closestBlood.transform.position);
        }

        if (BloodCleaned > MaxBloodCleaned - 1 && Robot)
        {
            Full = true;
        }
        else
        {
            Full = false;
        }
        if (Full & !FullTextOn)
        {
            FullTextOn = true;
            this.sakurascript.InfoSound.Play();
            this.sakurascript.Info.Play("infoshow");
            this.sakurascript.infotext.text = "The cleaning robot is full! empty it in the sink.";
        }
        if (!Full & FullTextOn)
        {
            FullTextOn = false;
        }
        // Shrinking the actual blood object
        if (OnTopOfBlood && currentBlood != null && !Full)
        {
            if (currentBlood.gameObject.name.Contains("Prefab"))
            {
                Transform actualBlood = currentBlood.Find("Visual/Blood");
                if (actualBlood != null)
                {
                    if (actualBlood.GetComponent<BloodPool>().BloodPrint)
                    {
                        actualBlood.localScale = new Vector3(0f, 0f, 0f);
                    }
                    else
                    {
                        float Cutoff = actualBlood.GetComponent<MeshRenderer>().material.GetFloat("_Cutoff");
                        actualBlood.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", Cutoff += 1f * CleanSpeed * Time.deltaTime);
                        Vector3 newSize = actualBlood.GetComponent<BoxCollider>().size;
                        newSize.x = Mathf.Lerp(newSize.x, 0f, CleanSpeed * Time.deltaTime);
                        newSize.z = Mathf.Lerp(newSize.z, 0f, CleanSpeed * Time.deltaTime);

                        actualBlood.GetComponent<BoxCollider>().size = newSize;
                    }
                    if (actualBlood.localScale.x < 0.009f && actualBlood.GetComponent<BloodPool>().BloodPrint)
                    {
                        if (!actualBlood.GetComponent<BloodPool>().Water)
                        {
                            this.BloodCleaned += 1;
                        }
                        Destroy(currentBlood.gameObject);
                        currentBlood = null;
                        OnTopOfBlood = false;
                    }
                    else if (actualBlood.GetComponent<MeshRenderer>().material.GetFloat("_Cutoff") > 0.941f && !actualBlood.GetComponent<BloodPool>().BloodPrint)
                    {
                        if (!actualBlood.GetComponent<BloodPool>().Water)
                        {
                            this.BloodCleaned += 1;
                        }
                        if (!actualBlood.GetComponent<BloodPool>().Stain)
                        {
                            Destroy(currentBlood.parent.parent.gameObject);
                        }
                        else
                        {
                            Destroy(currentBlood.gameObject);
                        }
                        currentBlood = null;
                        OnTopOfBlood = false;
                    }
                }
            }
            else
            {
                Transform actualBlood2 = currentBlood;
                if (actualBlood2 != null)
                {
                    if (actualBlood2.GetComponent<BloodPool>().BloodPrint)
                    {
                        actualBlood2.localScale = new Vector3(0f, 0f, 0f);
                    }
                    else
                    {
                        float Cutoff = actualBlood2.GetComponent<MeshRenderer>().material.GetFloat("_Cutoff");
                        actualBlood2.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", Cutoff += 1f * CleanSpeed * Time.deltaTime);
                        Vector3 newSize = actualBlood2.GetComponent<BoxCollider>().size;
                        newSize.x = Mathf.Lerp(newSize.x, 0f, CleanSpeed * Time.deltaTime);
                        newSize.z = Mathf.Lerp(newSize.z, 0f, CleanSpeed * Time.deltaTime);

                        actualBlood2.GetComponent<BoxCollider>().size = newSize;
                    }
                    if (actualBlood2.localScale.x < 0.001f && actualBlood2.GetComponent<BloodPool>().BloodPrint)
                    {
                        if (!actualBlood2.GetComponent<BloodPool>().Water)
                        {
                            this.BloodCleaned += 1;
                        }
                        Destroy(currentBlood.gameObject);
                        currentBlood = null;
                        OnTopOfBlood = false;
                    }
                    else if (actualBlood2.GetComponent<MeshRenderer>().material.GetFloat("_Cutoff") > 0.941f && !actualBlood2.GetComponent<BloodPool>().BloodPrint)
                    {
                        if (!actualBlood2.GetComponent<BloodPool>().Water)
                        {
                            this.BloodCleaned += 1;
                        }
                        if (!actualBlood2.GetComponent<BloodPool>().Stain)
                        {
                            Destroy(currentBlood.parent.parent.gameObject);
                        }
                        else
                        {
                            Destroy(currentBlood.gameObject);
                        }
                        currentBlood = null;
                        OnTopOfBlood = false;
                    }
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Blood"))
        {
            if (!Robot && this.transform.parent.GetComponent<MoppingScript>().Sweeping)
            {
                if (!other.gameObject.GetComponent<BloodPool>().Water)
                {
                    this.transform.parent.GetComponent<MoppingScript>().ChangingColor = true;
                }
                OnTopOfBlood = true;
                currentBlood = other.transform;
            }
            if (Robot)
            {
                OnTopOfBlood = true;
                currentBlood = other.transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Blood") && other.transform == currentBlood)
        {
            if (!Robot)
            {
                this.transform.parent.GetComponent<MoppingScript>().ChangingColor = false;
                OnTopOfBlood = false;
                currentBlood = null;
            }
            else
            {
                OnTopOfBlood = false;
                currentBlood = null;
            }

        }
    }
}
