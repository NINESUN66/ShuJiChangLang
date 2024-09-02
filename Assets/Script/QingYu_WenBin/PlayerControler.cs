
using UnityEngine;

public class PlayerConrtroler : MonoBehaviour
{

    private Rigidbody2D rb;//����
    public float movespeed;//�ƶ��ٶ�
    private float moveX, moveY;//��ȡ����
    private float moveX_2, moveY_2;//��ȡ����

    private Animator anim;//����


    private Vector2 moveDirection;//�ƶ�����
    private Vector2 moveDirection_2;//ҡ���ƶ�����
    public FixedJoystick joystick;//ҡ�˿���
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
        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift))//��ס��Shift�����м���
        {
            movespeed = 8.0f;
        }
        else
        {
            movespeed = 6.0f;
        }
        
    }

    private void FixedUpdate()//����ģ��
    {
        playerMove();
        PlayerAnim();
    }
    private void inputSection()//���벿��
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
    private void playerMove()//�ƶ�
    {
        
#if UNITY_ANDROID
            rb.velocity = new Vector2(moveDirection_2.x * movespeed, moveDirection_2.y * movespeed);
#else 
            rb.velocity = new Vector2(moveDirection.x * movespeed, moveDirection.y * movespeed);
#endif
        
    }

    private void PlayerAnim()//��Ӹ������ٶ�
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

