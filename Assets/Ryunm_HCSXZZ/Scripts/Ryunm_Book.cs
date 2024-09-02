using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BookType
{
    One=0,
    Two=1,   
    Three=2,
    Four=3,
    Five=4,
    Six=5,
    Seven=6
}

public enum Bookstate
{
    Defind=0,
    Standby=1,
    Falling=2,
    Collisioned=3
}
//继承基类
public class Ryunm_Book : MonoBehaviour
{
    // Start is called before the first frame update
    public BookType bookType= BookType.One;
    public float x_Limit = 2.5f;
    public Bookstate bookState = Bookstate.Defind;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.x<-x_Limit)
        {
            this.gameObject.transform.position = new Vector3(-x_Limit,transform.position.y,transform.position.z);
        }
        if(this.gameObject.transform.position.x>x_Limit)
        {
            this.gameObject.transform.position = new Vector3(x_Limit, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Book"))
        {
            Ryunm_Book collisionBook = collision.gameObject.GetComponent<Ryunm_Book>();
            if(collisionBook)
            {
                if(collisionBook.bookType == bookType)
                {
                    float pos_xy = this.transform.position.x + this.transform.position.y;
                    float collision_xy = collision.transform.position.x + collision.transform.position.y;
                    if(pos_xy>collision_xy)
                    { 
                        Vector2 pos = (this.transform.position + collision.transform.position) / 2;
                        Ryunm_GameManager._instance.CombineNewBook(bookType, pos);
                    }

                    Destroy(this.gameObject);//销毁自身
                }
            }
        }
        
        bookState = Bookstate.Collisioned;

        if(bookState==Bookstate.Falling)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                Ryunm_GameManager._instance.PlayFloorAudio();
            }
        }
    }
}
