using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensBehaviour : MonoBehaviour
{
    [Header("Item Information")]
    public string itemName;
    public LayerMask playerLayer;
    protected bool getItem;
    public float itemArea;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ItemController();
    }
    protected void ItemController()
    {
        getItem = Physics2D.CircleCast(transform.position, itemArea, transform.position, itemArea, playerLayer);

        if(getItem)
        {
            if(itemName=="MindPoints")
            {
                PlayerStatus.mindPoints++;
                print("Mind Points: "+PlayerStatus.mindPoints);
                Destroy(transform.gameObject);    
            }
            else if(itemName=="LifeRecovery")
            {
                PlayerStatus.hpCurrent++;
                print("HP: "+PlayerStatus.hpCurrent);
                Destroy(transform.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color  = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, itemArea);
        
    }
}
