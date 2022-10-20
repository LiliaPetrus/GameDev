using System;
using UnityEngine;

public class NinjaMover : MonoBehaviour
{
    public float groundCheckerRadius;
    public Transform groundChecker;
    public new Rigidbody2D rigidbody;
    public float speed;
    public new Transform transform;
    public float jumpPower;
    public LayerMask hereIsGround;


    public Collider2D headCollider; 
    public Transform ceilChecker;
    public float ceilCheckerRadius;

    public Animator animator;
    public string animationRunKey;

 
    private bool faceDirRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        var grounded =  Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, hereIsGround);
       
        float direction = Input.GetAxisRaw("Horizontal"); //переменная direction: при нажатии А, <- = -1, a D, -> = 1. персонаж двигается по горизонтльной оси . Axis - ось //просто аксис - от -1 до 1; аксисРaу четко определяет направление
        Vector2 velocity = rigidbody.velocity;
        animator.SetBool(animationRunKey, direction != 0);


        if (grounded)
        {
            rigidbody.velocity = new Vector2(speed * direction, velocity.y);

            if (direction < 0 && faceDirRight)
            {
                Turning();
            }   else if (direction > 0 && !faceDirRight)
            {
                Turning();
            }
        }


        if (Input.GetButtonUp("Jump"))
        {
            if (Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, hereIsGround))
            {
                rigidbody.velocity = new Vector2(velocity.x, jumpPower);
            }
        }
       
        bool ceilAboveHead = Physics2D.OverlapCircle(ceilChecker.position, ceilCheckerRadius, hereIsGround);


        if (Input.GetKey(KeyCode.C))
        {
            headCollider.enabled = false;
        } 
        else if (!ceilAboveHead)
        {
            headCollider.enabled = true;
        }
    }


    private void Turning() //этот метод отвечает за то, в какую сторону повернут персонаж при движении
    {
        faceDirRight = !faceDirRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos() //для проверки земли под ногами и потолка над головой
    {
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius); 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ceilChecker.position, ceilCheckerRadius);
    }
}

