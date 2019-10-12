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

    [Tooltip("The audio clip played when jumping")]
    public SimpleAudioEvent m_jumpSound;

    [HideInInspector] public Player m_player;
    [HideInInspector] public bool m_directionX; // Whether or not the character is facing right

    protected Rigidbody2D m_rigidbody2D;
    protected Vector3 m_velocity;

    private float m_lastJumpTime = 0f;
    private bool m_isGrounded = true;
    private AudioSource m_source = null;

    void Awake() {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_rigidbody2D.gravityScale = 0f;
        m_directionX = true;
    }

    void Update() {
        if(m_source == null) m_source = GetComponent<AudioSource>();

        m_isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.25f), 
                                            new Vector2(0.5f, 0.5f), 0, m_groundLayer);

        if(!m_isGrounded) ApplyGravity();

        if(!m_player.m_hasEnteredGame && !m_player.m_puppet) {
            m_rigidbody2D.velocity = new Vector2(0, m_rigidbody2D.velocity.y);
            m_player.m_animator.SetBool("isWalking", false);
            return;
        }

        if(m_player.m_puppet &&
            Game.m_players.GetPlayerFromId(m_player.m_playerId == 0 ? 1 : 0).m_lastUsed !=
            m_player.m_rewiredPlayer.controllers.GetLastActiveController()) {
            m_player.m_animator.SetBool("isWalking", false);
            return;
        }

        float xMove = m_player.m_rewiredPlayer.GetAxisRaw("MoveX");
        float yMove = 0f;
        bool jump = m_player.m_rewiredPlayer.GetButton("Jump");
        bool withinHoldLimit = Time.time - m_lastJumpTime <= m_maxJumpHoldTime;

        if(jump) {
            if(Time.time - m_lastJumpTime >= m_jumpCooldown && !withinHoldLimit && m_isGrounded) {
                if(m_jumpSound != null) m_jumpSound.Play(m_source);

                m_lastJumpTime = Time.time;
                yMove = m_jumpHeight * 2f;
            } else if(withinHoldLimit)
                yMove = m_jumpHeight * 2f * (1f - (Time.time - m_lastJumpTime) / m_maxJumpHoldTime);
        }

        if(float.IsNaN(xMove)) xMove = 0;
        if(float.IsNaN(yMove)) yMove = 0;

        xMove *= Time.deltaTime;
        yMove *= Time.deltaTime;

        if(Mathf.Abs(xMove) > 0.05) m_player.m_animator.SetBool("isWalking", true);
        else m_player.m_animator.SetBool("isWalking", false);

        Vector3 targetVelocity = new Vector2(xMove * (m_speed / Time.unscaledDeltaTime) + m_rigidbody2D.velocity.x / 2, 
                                             yMove * (m_jumpSpeed / Time.unscaledDeltaTime));

        Move(targetVelocity);

        if((targetVelocity.x > 0.1 && !m_directionX) || (targetVelocity.x < -0.1 && m_directionX)) Flip();
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
        m_player.m_switchTooltip.flipX = !m_player.m_switchTooltip.flipX;
        transform.localScale = localScale;
        m_directionX = !m_directionX;
    }
}
