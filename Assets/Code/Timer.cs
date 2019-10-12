using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    float time = 0.00f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SetTimer()
    {
        gameObject.GetComponent<TMP_Text>().text = time.ToString("F2") + " s";
        time+=0.01f;
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(SetTimer());
    }
}
