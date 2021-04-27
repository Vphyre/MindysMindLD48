using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Enemy
{    
    public GameObject eyeFindGround;
    public GameObject eyeFindWall;
    public GameObject eyeFindPlayer;
    public GameObject WeakPointObj;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        turnedRight = false;
        chasingPlayer = false;
        movementSpeed = 2.5f;

        EyeFindGround = eyeFindGround;
        EyeFindWall = eyeFindWall;
        EyeFindPlayer = eyeFindPlayer;
        weakPointObj = WeakPointObj;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color  = Color.yellow;
        Gizmos.DrawWireSphere(eyeFindGround.transform.position, 0.2f);
        Gizmos.DrawWireSphere(eyeFindPlayer.transform.position, 0.4f);
        Gizmos.DrawWireSphere(eyeFindWall.transform.position, 0.2f);
        Gizmos.DrawWireSphere(WeakPointObj.transform.position, 0.2f);
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
