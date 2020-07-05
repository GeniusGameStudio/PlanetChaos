using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移动速度
    public float moveSpeed = 50;

    //跳跃力度
    public float jumpForce = 200;

    //地面检测点
    public Transform groundCheckPoint;

    //地面检测层
    public LayerMask groundCheckLayer;

    //动画机
    private Animator anim;

    //2D刚体
    private Rigidbody2D rb;

    //重力
    private float gravityScale;

    //精灵渲染器
    private SpriteRenderer sprite;

    //水平移动输入
    private float horizontal_move;

    //跳跃输入
    private bool isJumpButtonDown;

    //地面检查
    private bool isGround;

    //是否已跳跃
    private bool isJump;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        gravityScale = rb.gravityScale;
    }

    /// <summary>
    /// 处理输入
    /// </summary>
    private void ProcessInput()
    {
        horizontal_move = Input.GetAxisRaw("Horizontal");
        isJumpButtonDown = Input.GetButtonDown("Jump");
    }

    /// <summary>
    /// 更新移动
    /// </summary>
    private void UpdateMove()
    {
        rb.velocity = new Vector2(moveSpeed * horizontal_move, rb.velocity.y);

        if (horizontal_move > 0)
            sprite.flipX = true;
        else if (horizontal_move < 0)
            sprite.flipX = false;

        anim.SetBool("isWalk", horizontal_move != 0);
    }

    private void UpdateJump()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.05f, groundCheckLayer);
        if(isGround)
        {
            isJump = false;
            anim.SetBool("isJump", false);
            if (isJumpButtonDown)
            {
                isJump = true;
                rb.AddForce(new Vector2(0, jumpForce));
                isJumpButtonDown = false;
            }
        }
        else
        {
            if(rb.velocity.y > 2f)
                anim.SetBool("isJump", true);
        }
    }

    void Update()
    {
        ProcessInput();
    }

    private void FixedUpdate()
    {
        UpdateMove();
        UpdateJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.gravityScale = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.gravityScale = gravityScale;
        }
    }
}
