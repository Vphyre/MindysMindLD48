using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDeley = 0.25f;
    public float jumpTimer; 

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;
    // public GameObject characterHolder;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.69f;
    public Vector3 colliderOffset;
    
    [Header("Health Status")]
    public static float invencibleTime = 0;
    public static bool dameged;
    public static float enemyPosition = 0;
    public static bool fallDamage = false;
    public static bool canMove = true;
    public static bool enemyJumpFlag = false;
    private float deathTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) ||  Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        if(Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDeley;
        }
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void FixedUpdate()
    {   
        if(canMove)
        {
            MoveCharacter(direction.x);

            if(jumpTimer > Time.time && onGround)
            {
                Jump();
            }
            animator.SetBool("JumpFall", rb.velocity.y!=0f && !onGround);
            JumpEnemy();
        }
        ModifyPhysics();
        GetDamegeAndDeath();
        
    }
    void MoveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if(onGround)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            if(Input.GetAxisRaw("Horizontal")==0)
            {
                animator.SetFloat("Speed", 0f);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if((horizontal>0 && !facingRight) || (horizontal<0 && facingRight ))
        {
            Flip();
        }

        if(Mathf.Abs(rb.velocity.x) > (maxSpeed))
        {
            rb.velocity = new Vector2 (Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

    }
    void Jump()
    {
        rb.velocity = new Vector2 (rb.velocity.x,0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
    }
    void ModifyPhysics()
    {
        bool changingDirections = (direction.x> 0 && rb.velocity.x <0) || (direction.x < 0 && rb.velocity.x >0);
        if(onGround)
        {    
            if(Mathf.Abs (direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if(rb.velocity.y<0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if(rb.velocity.y>0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }
    void Flip()
    {
        facingRight =!facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180,0);
    }
    
    private void GetDamegeAndDeath()
    {
        if(dameged && invencibleTime<=0)
        {
            if(fallDamage)
            {

            }
            else
            {
                KnockBack();
                invencibleTime = 2f;
                PlayerStatus.hpCurrent--;
                dameged = false;
                print("Tomou dano "+PlayerStatus.hpCurrent);
            }
            
        }
        else if(invencibleTime>0)
        {
            InvokeRepeating("BlinkSprite", 0.5f, 0.2f);
            if(invencibleTime<0.4f)
            {
                canMove = true;
            }     
        }
        else
        {
            CancelInvoke();
            transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        invencibleTime -= Time.deltaTime;

        if(PlayerStatus.hpCurrent<=0)
        {
            canMove = false;
            animator.SetBool("Death", true);
            deathTime -= Time.deltaTime;
            if(deathTime<=0)
            {
                PlayerStatus.previousScene = SceneManager.GetActiveScene().name;
                if(PlayerStatus.previousScene=="Phase1")
                {
                    PlayerStatus.mindPoints = 0;
                }
                else if(PlayerStatus.previousScene=="Phase2")
                {
                    PlayerStatus.mindPoints = PlayerStatus.mpPhase2;
                }
                else if(PlayerStatus.previousScene=="Phase3")
                {
                    PlayerStatus.mindPoints = PlayerStatus.mpPhase3;
                }
                print(PlayerStatus.previousScene);
                SceneManager.LoadScene("GameOver2");
            }
            //fazer possíveis alterações no reconhecimento de inimigos
        }
    }
    private void KnockBack()
    {
        if(transform.position.x>enemyPosition)  
        {
            transform.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 (900f, 500f));
            canMove = false;
        }
        else
        {
            transform.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 (-900f, 500f));
            canMove = false;
        }
        enemyPosition = 0f;
    }
    private void BlinkSprite()
    {
        transform.gameObject.GetComponent<SpriteRenderer>().enabled = !transform.gameObject.GetComponent<SpriteRenderer>().enabled;
    }
    private void JumpEnemy()
    {
        if(enemyJumpFlag)
        {
            // transform.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 (0f, 800f));
            Jump();
            enemyJumpFlag = false;
            print("Pulou na cabeça");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color  = Color.white;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }
}
