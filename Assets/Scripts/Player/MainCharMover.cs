using System;
using UnityEngine;

public class MainCharMover : MonoBehaviour
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
    public string animationCrouchKey;
    public string animationJumpKey;

 
    private bool faceDirRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        var grounded =  Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, hereIsGround);
       
        float direction = Input.GetAxisRaw("Horizontal"); //���������� direction: ��� ������� �, <- = -1, a D, -> = 1. �������� ��������� �� ������������� ��� . Axis - ��� //������ ����� - �� -1 �� 1; ������a� ����� ���������� �����������
        Vector2 velocity = rigidbody.velocity;
        animator.SetBool(animationRunKey, direction != 0);


        if (grounded)
        {
            rigidbody.velocity = new Vector2(speed * direction, velocity.y);

            if (direction < 0 && faceDirRight)
            {
                Flip();
            }   else if (direction > 0 && !faceDirRight)
            {
                Flip();
            }
        }


        if (Input.GetButton("Jump"))
        {
            if (Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, hereIsGround))
            { 
                animator.SetBool(animationJumpKey, true);
                rigidbody.velocity = new Vector2(velocity.x, jumpPower);
            }
        }
        else
        {
            if (Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, hereIsGround))
            {
                animator.SetBool(animationJumpKey, false);
            }
        }

     
        bool ceilAboveHead = Physics2D.OverlapCircle(ceilChecker.position, ceilCheckerRadius, hereIsGround);

        if (Input.GetKey(KeyCode.C))
        {
            headCollider.enabled = false;
            animator.SetBool(animationCrouchKey, true);
        } 
        else if (!ceilAboveHead)
        {
            headCollider.enabled = true;
            animator.SetBool(animationCrouchKey, false);    
        }

        animator.SetBool(animationCrouchKey, !headCollider.enabled);
    }


    private void Flip() //���� ����� �������� �� ��, � ����� ������� �������� �������� ��� ��������
    {
        faceDirRight = !faceDirRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos() //��� �������� ����� ��� ������ � ������� ��� �������
    {
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius); 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ceilChecker.position, ceilCheckerRadius);
    }
}

