using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurnBaseUtil;
using UnityEngine;
using static Global;
public class PlayerController : MonoBehaviour
{
    //移动速度
    public float moveSpeed = 1.2f;

    //跳跃力度
    public float jumpForce = 200;

    //头顶检测点
    public Transform headCheckPoint;

    //地面检测点
    public Transform groundCheckPoint;

    //地面检测层
    public LayerMask groundCheckLayer;

    //动画机
    private Animator anim;

    //2D刚体
    private Rigidbody2D rb;

    //重力尺寸
    private float gravityScale;

    //精灵渲染器
    private SpriteRenderer sprite;

    //水平移动输入
    private float horizontal_move;

    //跳跃输入
    private bool isJumpButtonDown;

    //地面检查
    private bool isGround;

    //头顶检查
    private bool isHead;

    //是否已跳跃
    private bool isJump;

    //子弹最大速度
    public float bulletMaxInitialVelocity; 

    //最大射击蓄力时间
    public float maxTimeShooting; 

    //地图的碰撞
    public PolygonCollider2D groundBC; 

    //子弹的预制体
    public GameObject bulletPrefab;

    //是否处于射击状态
    private bool shooting; 

    //射击蓄力时长
    private float timeShooting; 

    //射击朝向
    private Vector2 shootDirection; 

    //射击特效
    public GameObject shootingEffect; 

    //武器的位置
    private Transform weaponTransform; 

    //人物身体的位置
    private Transform bodyTransform; 

    //子弹初始位置
    public Transform bulletInitialTransform; 

    //是否处于瞄准状态
    private bool targetting;

    private bool isFirstInit = true;

    private bool canControl = false;

    private float mouseScrollWheel;

    public float size = 0.95f;

    private PlayerUI ui;

    private TeamPlayer player;

    public float boomForceValue = 150;

    private AudioSource SFX;
    public AudioClip jumpSFX;
    public AudioClip dieSFX;
    public AudioClip chargeSFX;
    public AudioClip shootSFX;

    public bool[] useWeapon = new bool[16];

    public bool IsDead { get; set; }


    private void OnEnable()
    {
        canControl = true;
        
        Debug.Log(gameObject.name + "OnEnable");
        if (isFirstInit)
        {
            canControl = false;
            isFirstInit = false;
            return;
        }
        //rb.gravityScale = 0;
        GameManager.Instance.vCam.m_Lens.OrthographicSize = 5;
        GameManager.Instance.vCam.Follow = transform;
        ui.SetArrowActive(true);
        UIManager.Instance.SetEndTurnButtonActive(true);
    }

    private void OnDisable()
    {
        canControl = false;
        rb.gravityScale = 0;
        Debug.Log(gameObject.name + "OnDisable");
        targetting = false;

        ui.SetArrowActive(false);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ui = GetComponent<PlayerUI>();
        player = GetComponent<TeamPlayer>();
        SFX = GetComponent<AudioSource>();
        weaponTransform = transform.Find("Bazooka").gameObject.transform;
        bodyTransform = transform;
        gravityScale = rb.gravityScale;
    }

    void UpdateCamera()
    {
        mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScrollWheel < 0)
        {
            GameManager.Instance.vCam.m_Lens.OrthographicSize /= size;
        }
        else if (mouseScrollWheel > 0)
        {
            GameManager.Instance.vCam.m_Lens.OrthographicSize *= size;
        }

        GameManager.Instance.vCam.m_Lens.OrthographicSize = Mathf.Clamp(GameManager.Instance.vCam.m_Lens.OrthographicSize, 5f, 7.933367f);
    }

    /// <summary>
    /// 射击的按键检测，按鼠标左键进行射击
    /// </summary>
    void UpdateShootDetection()
    {

        if (Input.GetMouseButtonDown(0))
        {
            shooting = true;
            shootingEffect.SetActive(true);
            timeShooting = 0f;
            SFX.PlayOneShot(chargeSFX);
            UIManager.Instance.SetEndTurnButtonActive(false);
        }
    }

    /// <summary>
    /// 松开鼠标左键，完成射击
    /// </summary>
    void UpdateShooting()
    {
        timeShooting += Time.deltaTime;
        if (Input.GetMouseButtonUp(0))
        {
            shooting = false;
            shootingEffect.SetActive(false);
            Shoot();
            SFX.Stop();
            SFX.PlayOneShot(shootSFX);
        }
        if (timeShooting > maxTimeShooting)
        {
            shooting = false;
            shootingEffect.SetActive(false);
            Shoot();
            SFX.Stop();
            SFX.PlayOneShot(shootSFX);
        }
        canControl = false;
    }

    /// <summary>
    /// 朝鼠标方向射击
    /// </summary>
    void Shoot()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector2 playerToMouse = new Vector2(mousePosWorld.x - transform.position.x,
                                            mousePosWorld.y - transform.position.y);

        playerToMouse.Normalize();

        shootDirection = playerToMouse;
        Debug.Log("Shoot!");
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletInitialTransform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletMaxInitialVelocity * (timeShooting / maxTimeShooting);
        bullet.GetComponent<BulletController>().PlayerTransform = gameObject.transform;

        StartCoroutine(GameManager.Instance.DelayFuc(() => { weaponTransform.gameObject.SetActive(false); }, 1f));
    }

    /// <summary>
    /// 更新瞄准
    /// </summary>
    void UpdateTargetting()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosScreen);
        Vector2 playerToMouse = new Vector2(mousePosWorld.x - transform.position.x,
                                            mousePosWorld.y - transform.position.y);

        playerToMouse.Normalize();

        float angle = Mathf.Asin(playerToMouse.y) * Mathf.Rad2Deg;
        if (playerToMouse.x < 0f)
            angle = 180 - angle;

        if (playerToMouse.x > 0f && sprite.flipX)
        {
            sprite.flipX = false;
        }
        else if (playerToMouse.x < 0f && !sprite.flipX)
        {
            sprite.flipX = true;
        }

        weaponTransform.localEulerAngles = new Vector3(0f, 0f, angle);
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
    /// 物理检测
    /// </summary>
    private void UpdateCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, groundCheckLayer);
        isHead = Physics2D.OverlapCircle(headCheckPoint.position, 0.1f, groundCheckLayer);
    }

    /// <summary>
    /// 更新移动
    /// </summary>
    private void UpdateMove()
    {
        rb.velocity = new Vector2(moveSpeed * horizontal_move, rb.velocity.y);

        if (horizontal_move > 0)
            sprite.flipX = false;
        else if (horizontal_move < 0)
            sprite.flipX = true;

        anim.SetBool("isWalk", horizontal_move != 0);
    }

    private void UpdateJump()
    {
        
        if (isGround)
        {
            isJump = false;
            anim.SetBool("isJump", false);
            if (isJumpButtonDown)
            {
                isJump = true;
                rb.AddForce(new Vector2(0, jumpForce));
                isJumpButtonDown = false;
                SFX.PlayOneShot(jumpSFX);
            }
        }
        else
        {
            if (rb.velocity.y > 2f)
                anim.SetBool("isJump", true);
        }
    }

    public void UseBazooka()
    {
        targetting = true;
        weaponTransform.gameObject.SetActive(true);
        UITool.FindUIGameObject("BagPanel").transform.DOLocalMoveX(600, 1f);
        UIManager.Instance.isOpenedBag = false;
        
    }

    void Update()
    {
        UpdateCamera();
        if (IsDead)
            return;
        if (canControl)
        {
            ProcessInput();
            UpdateCheck();

            if (useWeapon[(int)Weapon.BAZOOKA])
            {
                UseBazooka();
            }
            ClearWeaponBoolean();

        }

        if (targetting)
        {
            UpdateTargetting();
            if (canControl)
            {
                UpdateShootDetection();
            }
            if (shooting)
                UpdateShooting();
        }

        
    }

    void UpdateHurt(int damage)
    {
        anim.SetBool("isHurt", true);
        player.DoHurt(damage);
        StartCoroutine(GameManager.Instance.DelayFuc(() => { anim.SetBool("isHurt", false); }, 1f));
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
        if (IsDead)
            return;
        if (collision.gameObject.CompareTag("Ground") && enabled)
        {
            if (!isHead)
                rb.gravityScale = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsDead)
            return;
        if (collision.gameObject.CompareTag("Ground") && enabled)
        {
            if (!isHead)
                rb.gravityScale = gravityScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDead)
            return;
        if (collision.gameObject.CompareTag("Explosion"))
        {
            Debug.Log("ExplosionHurt");
            Vector2 vec =  transform.position - collision.gameObject.transform.position;
            int damage = (int)(15f / vec.magnitude); //距离爆炸中心越近，受到的伤害越高
            UpdateHurt(damage);
            player.belongsTo.UpdateHP();
            rb.gravityScale = gravityScale;
            //受到爆炸的冲击力
            rb.AddForce(vec.normalized * boomForceValue / vec.normalized.magnitude);
        }
        else if (collision.gameObject.CompareTag("BulletCollider"))
        {
            rb.gravityScale = gravityScale;
        }
    }

    public void ClearWeaponBoolean()
    {
        useWeapon = new bool[16];
    }
}
