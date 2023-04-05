using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodMininig : MonoBehaviour
{
    public int foodMined;
    public int mine;
    public float miningSpeed = 10;
    public float durable = 1;
    public GameObject icon;

    public bool isRepair;
    public void Start()
    {
        foodMined = FindObjectOfType<ResManager>().food;
        StartCoroutine(mining());
    }

    public void Update()
    {
        if (durable < 1) icon.SetActive(true);
        
        else icon.SetActive(false);

        if (isRepair)
        {
            if (Input.GetMouseButton(0) && FindObjectOfType<ResManager>().food > 0)
            {
                FindObjectOfType<ResManager>().food -= 1;
                durable += 0.3f;
            }
        }

        if (durable > 1) durable = 1;
        if (durable >= 1) isRepair = false;
    }
    public void hollow()
    {
        StartCoroutine(mining());
    }

    public IEnumerator mining()
    {
        FindObjectOfType<ResManager>().food++;
        mine++;

        yield return new WaitForSeconds(miningSpeed / durable);
        hollow();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "meteor" && durable > 0.3)
        {
            durable -= 0.3f;            
        }

        if (collision.transform.tag == "repair" && durable < 1) isRepair = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "repair") isRepair = false;
    }
}
