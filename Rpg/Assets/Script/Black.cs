using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Black : MonoBehaviour
{
    Image black;
    void Start()
    {
        black = GameObject.Find("black").GetComponent<Image>();
        StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Fade()
    {
        //text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
        while (black.color.a > 0.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        
    }
}
