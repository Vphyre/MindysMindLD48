using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float eyesDistance = 0.2f;
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    protected GameObject EyeFindGround; // Define no outro script
    protected bool groundFound; 
    protected GameObject EyeFindWall; // Define no outro script
    protected bool wallFound;
    protected GameObject EyeFindPlayer; // Define no outro script
    protected bool chasingPlayer = false;
    protected GameObject weakPointObj; // Define no outro script
    protected bool weakFlag;
    protected bool attack;
    protected GameObject player; // Define no outro script
    protected Rigidbody2D rigidBody;  // Define no outro script
    protected Transform tr;  // Define no outro script
    protected bool turnedRight = true; 
    protected bool stop = false;
    protected float movementSpeed;  // Define no outro script
    protected float sideMoviment = 1f;
    protected float stopedTime;
        
    // Update is called once per frame
    protected virtual void Update()
    {
        if(!stop)
        {
            FindHolesPlayerWall();
            FindPlayer();
            MovimentEnemy();
        }
        AttackDefault();
        WeakPointControler();    
    }

    protected void FindPlayer()
    {            
        if(!stop)
        { 
            if(chasingPlayer)
            {
                if(tr.position.x>EyeFindPlayer.transform.position.x && turnedRight)
                {
                    sideMoviment = 2;
                }
                else if(tr.position.x<EyeFindPlayer.transform.position.x && !turnedRight)
                {
                    sideMoviment = 1;
                }       
        
            }
        }      
    }
    protected void MovimentEnemy()
    {   
        
            if(sideMoviment == 1f)
            {
                WalkSide("Left");
            }
            else
            {
               WalkSide("Right");
            }

        if(!groundFound || wallFound)
        {
            if(turnedRight)
            {
               
               sideMoviment = 1f;
            }
            else
            {
                sideMoviment = 2f;
            }
        }
        print(groundFound);
        print(wallFound);

    }
    protected void FindHolesPlayerWall()
    {
        groundFound = Physics2D.CircleCast(EyeFindGround.transform.position, 0.2f, Vector2.right, 0.1f, groundLayer);
        wallFound = Physics2D.CircleCast(EyeFindWall.transform.position, 0.2f, Vector2.right, 0.1f, groundLayer);

        chasingPlayer = Physics2D.CircleCast(EyeFindPlayer.transform.position, 0.4f, EyeFindPlayer.transform.position, 0.4f, playerLayer);
        weakFlag = Physics2D.CircleCast(weakPointObj.transform.position, 0.2f, Vector2.up, 0.2f, playerLayer);
        attack = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.5f, playerLayer);
    }
    protected void Flip()
    {
        if(!stop)
        {
           if(tr.position.x>EyeFindPlayer.transform.position.x && turnedRight)
           {
                turnedRight=!turnedRight;
                tr.localScale = new Vector2(-tr.localScale.x, tr.localScale.y);  
           }
           else if(tr.position.x<EyeFindPlayer.transform.position.x && !turnedRight)
           {
                turnedRight=!turnedRight;             
                tr.localScale = new Vector2(-tr.localScale.x, tr.localScale.y);
           }       
        }
    }
    protected void AttackDefault()
    {
        if(attack && CharacterBehaviour.invencibleTime<=0)
        {
            CharacterBehaviour.dameged = true;
            CharacterBehaviour.enemyPosition = transform.position.x;
        }
    }
    protected void WalkSide(string side)
    {
        if(side =="Right")
        {
            turnedRight=true;
            Flip();
            rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
        }
        else if(side == "Left")
        {
            turnedRight=false;
            Flip();
            rigidBody.velocity = new Vector2(-movementSpeed, rigidBody.velocity.y);
        }
    }
    protected void WeakPointControler()
    {
        if(weakFlag)
        {
            CharacterBehaviour.enemyJumpFlag = true;
            stop = true;
            stopedTime = 4f;
            weakFlag = false;
        }
        if(stopedTime>0)
        {
            animator.SetBool("Stuned", true);
            animator.SetBool("Walking", false);
            stop = true;
        }
        else
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Stuned", false);
            stop = false;
        }        
        stopedTime -=Time.deltaTime;
    }
}
