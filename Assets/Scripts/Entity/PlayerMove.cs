using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float runSpeed = 20.0f;
    public Animator animator;
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("XInput", horizontal);
        animator.SetFloat("YInput", vertical);
        if (horizontal != 0)
        {
            animator.SetFloat("AnimMag", Mathf.Abs(horizontal));
        }
        else if (vertical != 0)
        {
            animator.SetFloat("AnimMag", Mathf.Abs(vertical));
        }
        else
        {
            animator.SetFloat("AnimMag", 0);
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}