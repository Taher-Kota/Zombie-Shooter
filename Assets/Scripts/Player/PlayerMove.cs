using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private float MoveForce_X = 2f, MoveForce_Y = 2f;
    private PlayerAnimation playeranim;

    private void Awake()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        playeranim = GetComponent<PlayerAnimation>();
    }

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        float X_axix = Input.GetAxisRaw("Horizontal");
        float Y_axix = Input.GetAxisRaw("Vertical");

        MoveDirection(X_axix);
        if (X_axix > 0)
        {
            rb.velocity = new Vector2(MoveForce_X, rb.velocity.y);
        }
        else if (X_axix < 0)
        {
            rb.velocity = new Vector2(-MoveForce_X, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Y_axix > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, MoveForce_Y);
        }
        else if (Y_axix < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -MoveForce_Y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        if (X_axix != 0 || Y_axix != 0)
        {
            playeranim.RunAnimation(true);
        }
        else if (X_axix == 0 && Y_axix == 0)
        {
            playeranim.RunAnimation(false);
        }
    }

    void MoveDirection(float X)
    {
        Vector2 tempscale = transform.localScale;
        if (X > 0)
        {
            tempscale.x = -1f;
        }
        else if (X < 0)
        {
            tempscale.x = 1f;
        }
        transform.localScale = tempscale;
    }

}
