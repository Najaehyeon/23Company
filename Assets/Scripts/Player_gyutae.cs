using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player_gyutae : MonoBehaviour
{
    Animator animator; // ����Ƽ���� ��������
    Rigidbody2D _rigidbody;

    public CapsuleCollider2D capsuleCollider2D; // ĸ�� �ݶ��̴�2d ����
    private Vector2 normalcolliderSize; // �⺻ũ�� ����
    private Vector2 slideColliderSize = new Vector2(3f, 2.8f); // �����̵�� ������
    private Vector2 normalcolliderOffset; // �⺻ ������ ����
    private Vector2 slideColliderOffset = new Vector2(0f, -2.2f); // �����̵�� �ݶ��̴� ��ġ �̵�

    public float playerJumpPower = 10f; // �����ϴ� ��
    public float forwardSpeed = 5f; // ���� �ӵ�
    public bool isDead = false; // ���翩�� Ȯ��
    float deathCooldown = 0f; // �״� ��� ������
    public bool slide; // �����̵� �۵�
    private bool ground;
    private int jumpCount = 0;
    bool isRun = false; // �������� üũ ��� // �⺻�ൿ



    // Start is called before the first frame update
    void Start()
    { // InChildren �� �ٿ� ���� ������Ʈ�鿡�Ե� Ž�� ����
        animator = GetComponentInChildren<Animator>(); // �ۼ����� ��ũ��Ʈ�� ������ ������Ʈ���� ���� ã���ִ� 
        _rigidbody = GetComponent<Rigidbody2D>(); // ������Ʈ�� �ִ��� Ž���� ��ȯ
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        normalcolliderSize = capsuleCollider2D.size;
        normalcolliderOffset = capsuleCollider2D.offset;

        if (animator == null)
            Debug.Log("ani error");

        if (_rigidbody == null)
            Debug.Log("rigid error");
    }

    // Update is called once per frame
    void Update()
    {
        ground = CheckGround();

        if (ground) // �ٴڿ� ������
        {
            jumpCount = 0; // ����Ƚ�� �ʱ�ȭ
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 1 && !slide)
        { // 2�� ���� ����
            Jump();
            jumpCount++;
            ground = false;
        }

        if (ground && Input.GetKey(KeyCode.LeftShift)) // �ٴڿ� �������� �����̵� ����
        {
            Slide(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Slide(false);
        }


        if (isDead)
        {
            if (deathCooldown <= 0f) // ������� �����
            {

            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else // ���� ���� ���� 
        {
            if ((Input.GetKeyDown(KeyCode.Space)) && jumpCount < 1)
            {
                isRun = true;
            }
        }
    }
    private void FixedUpdate() // ���� ������ �ʿ��� �� ������ �������� ȣ��
    {
        if (isDead) return; // isDead �� true ��� �۾����� �ʰ� ��ȯ

        Vector3 velocity = _rigidbody.velocity; // _rigidbody �� ���ӵ� ��������
        velocity.x = forwardSpeed;

        if (isRun) // isRun �� true �϶�
        {
            velocity.y += playerJumpPower; // velocity.y �� �����Ҷ� ���� ����
            //velocity.y = playerJump; // velocity.y �� �����ϴ� �� �ֱ� ���̰� ����
            isRun = false;
        }

        _rigidbody.velocity = velocity; // �ٽ� �־������
    }

    private void OnCollisionEnter2D(Collision2D collision) // �浹�� ���� �̺�Ʈ �߻��� ����
    {
        if (isDead) return; // �̹� �׾����� �浹 ����

        if (collision.gameObject.CompareTag("Ground"))  // ���� �浹�ϸ� ���� �ʵ��� ���� ó��
        {
            ground = true;   // �ٴ� ����
            jumpCount = 0;   // ���� Ƚ�� �ʱ�ȭ
            return;          // ���� �ʵ��� ���� ó��
        }

        isDead = true;
        deathCooldown = 1f;
        animator.SetInteger("IsDie", 1); // �ִϸ����Ϳ� "IsDie"��� �Ķ������ ���� 1�� ����(������)
        
    }

    void Jump()
    {
        if(slide)
        {
            Slide(false);
            animator.SetBool("IsJump", true);
            slide = false;
        }

        if (jumpCount == 0)
        {
           // _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, playerJumpPower); // 1�� ����
            _rigidbody.AddForce(Vector3.up*playerJumpPower, ForceMode2D.Impulse);
        }
    }
    void Slide(bool isSlide)
    {
        if (slide == isSlide) return; // ���� ���¿� �����ϸ� ����

        slide = isSlide;// �����̵� ����
        animator.SetBool("IsSlide", isSlide);

        if (isSlide)
        {
            capsuleCollider2D.size = slideColliderSize; // �����̵� ũ��� ����
            capsuleCollider2D.offset = slideColliderOffset;
        }
        else
        {
            animator.SetBool("IsSlide", false);
            capsuleCollider2D.size = normalcolliderSize; // �⺻ ũ��� ����
            Debug.Log("�����̵� ����, �ݶ��̴� ���� ũ��� ������: " + normalcolliderSize);
            capsuleCollider2D.offset = normalcolliderOffset;

        }
    }
    bool CheckGround()
    {
        float rayLength = 2.5f; // ������ ���� ����
        LayerMask groundLayer = LayerMask.GetMask("Ground"); // �ٴ� �׶��� ���̾�
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);
        // Physics2D.Raycast �����浹�� �����ϴ� ������ �߻� , �÷��̾� ��ġ���� ������ �߻�
        // Vector2.down �Ʒ� �������� 2.5f ��ŭ�� ���̸� �߻�. �ٴ��� �ִ��� �˻�
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);
        
        if (hit.collider != null) // �ٴڿ� �浹�� ������Ʈ�� �ִٸ�
        {
            animator.SetBool("IsJump", false);
            return true; // �ٴ��� �����Ǹ� ���� �ʱ�ȭ
        }
        else
        {
            animator.SetBool("IsJump", true); // ���߿� �ִٸ� 
            return false;
        }
    }
}
