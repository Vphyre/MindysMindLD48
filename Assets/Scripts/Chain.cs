using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public float chainTime;
    public GameObject chain;
    public LayerMask playerLayer;
    protected bool stopTime = false;
    protected bool stopFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        chain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        stopTime = Physics2D.CircleCast(transform.position, 1.5f, Vector2.up, 1.5f, playerLayer);
        if(stopTime)
        {
            stopFlag = true;
        }

        if(!stopFlag)
        {
            chainTime -= Time.deltaTime;
            if(chainTime<0)
            {
                chain.SetActive(true); 
            }
        }        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color  = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
        
    }
}
