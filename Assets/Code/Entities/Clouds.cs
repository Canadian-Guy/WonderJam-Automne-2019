using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public float m_TravelSpeed = 0.2f;
    public bool m_TravelRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.right * (m_TravelRight == true ? 1 : -1) * m_TravelSpeed * Time.deltaTime);
    }
}
