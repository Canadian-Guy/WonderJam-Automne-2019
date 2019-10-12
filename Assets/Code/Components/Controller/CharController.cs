using UnityEngine;

public class CharController : MonoBehaviour
{
    [Tooltip("The ground layer used to detect grounding")]
    public LayerMask m_groundLayer;

    [Tooltip("The smoothing delay added to movement")]
    [Range(0, 1f)] public float m_smoothTime = 0.05f;

    [Tooltip("The speed the character moves at")]
    [Range(0, 10f)] public float m_speed = 1f;

    [Tooltip("The height the character jumps at")]
    [Range(0, 10f)] public float m_jumpHeight = 2f;

    [Tooltip("The speed at which the character reaches the apex of its jump")]
    [Range(0, 10f)] public float m_jumpSpeed = 2f;

    [Tooltip("The time needed to jump at the character's maximum height")]
    [Range(0, 1f)] public float m_maxJumpHoldTime = 0.3f;

    [Tooltip("The speed at which the character falls")]
    [Range(-250f, 0f)] public float m_fallVelocity = -50f;

    [Tooltip("The cooldown until the character can jump again")]
    [Range(0, 2f)] public float m_jumpCooldown = 0.5f;

    [HideInInspector] public Player m_player;
    [HideInInspector] public bool m_directionX; // Whether or not the character is facing right

    protected Rigidbody2D m_rigidbody2D;
    protected Vector3 m_velocity;

    private float m_lastJumpTime = 0f;
    private bool m_isGrounded = true;

    void Awake() {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_rigidbody2D.gravityScale = 0f;
        m_directionX = true;
    }

    void Update() {
        m_isGrounded = Physics2D.OverlapCircle(transform.position, 0.15f, m_groundLayer);

        if(!m_isGrounded) ApplyGravity();

        float xMove = m_player.m_rewiredPlayer.GetAxisRaw("MoveX");
        float yMove = 0f;
        bool jump = m_player.m_rewiredPlayer.GetButton("Jump");
        bool withinHoldLimit = Time.time - m_lastJumpTime <= m_maxJumpHoldTime;

        if(jump) {
            if(Time.time - m_lastJumpTime >= m_jumpCooldown && !withinHoldLimit && m_isGrounded) {
                m_lastJumpTime = Time.time;
                yMove = m_jumpHeight * 2f;
            } else if(withinHoldLimit)
                yMove = m_jumpHeight * 2f * (1f - (Time.time - m_lastJumpTime) / m_maxJumpHoldTime);
        }

        if(float.IsNaN(xMove)) xMove = 0;
        if(float.IsNaN(yMove)) yMove = 0;

        xMove *= Time.deltaTime;
        yMove *= Time.deltaTime;

        Vector3 targetVelocity = new Vector2(xMove * (m_speed / Time.unscaledDeltaTime) + m_rigidbody2D.velocity.x / 2, 
                                             yMove * (m_jumpSpeed / Time.unscaledDeltaTime));

        Move(targetVelocity);

        if((targetVelocity.x > 0 && !m_directionX) || (targetVelocity.x < 0 && m_directionX)) Flip();
    }

    private void ApplyGravity() {
        Vector3 velocity = m_rigidbody2D.velocity;

        velocity.y += m_fallVelocity * Time.deltaTime;
        m_rigidbody2D.velocity = velocity;
    }

    private void Move(Vector3 p_targetVelocity) {
        m_rigidbody2D.velocity = Vector3.SmoothDamp(m_rigidbody2D.velocity, p_targetVelocity, ref m_velocity, m_smoothTime);
    }

    private void Flip() {
        Vector3 localScale = transform.localScale;

        localScale.x *= -1;
        transform.localScale = localScale;
        m_directionX = !m_directionX;
    }
}
