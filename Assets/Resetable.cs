using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetable : MonoBehaviour
{
    [Tooltip("Prefab to instantiate on reset")]
    public GameObject m_Prefab;

    [Tooltip("Prefab to delete on reset, aka original instance in the scene")]
    public GameObject m_ObjectToReset;

    public void Reset()
    {
        m_ObjectToReset.transform.position = gameObject.transform.position;

        //Destroy(m_ObjectToReset);   //Destroy the old boi
        //m_ObjectToReset = Instantiate(m_Prefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);    //Spawn a new boi, whomst becomes the new old boi
    }
}
