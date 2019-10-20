using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Tooltip("Point at which the colliding object will teleport")]
    public GameObject m_Target; 
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.position = m_Target.transform.position;
    }
}
