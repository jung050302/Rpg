using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Villageinteraction : MonoBehaviour
{
    public Text text;
     
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
             
             
            StartCoroutine(TextFadeOut());
             
            
        }
    }
    IEnumerator TextFadeOut()  // 알파값 1에서 0으로 전환
    {
        //text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
        while (text.color.a < 1f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime));
            yield return null;
        }
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 4.0f));
            yield return null;
        }

    }
}
