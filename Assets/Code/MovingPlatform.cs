using UnityEngine;

public class MovingPlatform : Lockable
{
    [Tooltip("Empty GameObject which represents the stopping point of the platform")]
    public GameObject m_endPoint;

    [Tooltip("Platform will move vertically if set to false")]
    public bool m_horizontalSliding = true;

    [Tooltip("Default is right for horizontal sliding and down for vertical sliding")]
    public bool m_reverseDirection = false;

    [Tooltip("Speed at which the platform moves")]
    public float m_slidingSpeed = 0.2f;

    private Vector3 m_startingPosition;
    private bool m_destinationReached = false;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(locked);
        if (!locked)
        {
            if (!m_destinationReached)
            {
                gameObject.transform.Translate((m_horizontalSliding ? Vector3.right : Vector3.up) * m_slidingSpeed * Time.deltaTime * (!m_reverseDirection ? 1 : -1));
            }
            else
            {
                gameObject.transform.Translate((m_horizontalSliding ? Vector3.right : Vector3.up) * m_slidingSpeed * Time.deltaTime * (m_reverseDirection ? 1 : -1));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlatformEnd")
            m_destinationReached = true;
        else if (collision.tag == "PlatformStart")
            m_destinationReached = false;
    }
}
