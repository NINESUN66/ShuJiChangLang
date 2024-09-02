
using UnityEngine;

public class PlayerConrtroler : MonoBehaviour
{

    private Rigidbody2D rb;//刚体
    public float movespeed;//移动速度
    private float moveX, moveY;//获取输入
    private float moveX_2, moveY_2;//获取输入

    private Animator anim;//动作


    private Vector2 moveDirection;//移动方向
    private Vector2 moveDirection_2;//摇杆移动方向
    public FixedJoystick joystick;//摇杆控制
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        inputSection();
        FixedUpdate();
        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift))//按住“Shift”进行加速
        {
            movespeed = 8.0f;
        }
        else
        {
            movespeed = 6.0f;
        }
        
    }

    private void FixedUpdate()//物理模块
    {
        playerMove();
        PlayerAnim();
    }
    private void inputSection()//输入部分
    {


#if UNITY_ANDROID
        if (joystick.isActiveAndEnabled)
        {
            moveX_2 = joystick.Horizontal;
            moveY_2 = joystick.Vertical;
        }
        else
        {
            moveX_2 = 0;
            moveY_2 = 0;
        }
        moveDirection_2 = new Vector2(moveX_2, moveY_2).normalized;
#else
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
#endif

    }
    private void playerMove()//移动
    {
        
#if UNITY_ANDROID
            rb.velocity = new Vector2(moveDirection_2.x * movespeed, moveDirection_2.y * movespeed);
#else 
            rb.velocity = new Vector2(moveDirection.x * movespeed, moveDirection.y * movespeed);
#endif
        
    }

    private void PlayerAnim()//添加给物体速度
    {


#if UNITY_ANDROID
        if(moveDirection_2 !=Vector2.zero)
        {
            anim.SetBool("IsWalking", true);
            anim.SetFloat("moveX", moveX_2);
            anim.SetFloat("moveY", moveY_2);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

#else
        if (moveDirection != Vector2.zero)
        {
            anim.SetBool("IsWalking", true);
            anim.SetFloat("moveX", moveX);
            anim.SetFloat("moveY", moveY);
        }

        else
        {
            anim.SetBool("IsWalking", false);
        }
#endif


    }
}

