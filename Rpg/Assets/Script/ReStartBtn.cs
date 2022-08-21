using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReStartBtn : MonoBehaviour
{
    public Animator ani;
    Image black;
    void Start()
    {
        black = GameObject.Find("black").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        
        while (black.color.a < 1.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a + (Time.deltaTime / 3.0f));
            yield return null;
        }
        ani.SetBool("EditChk", true);
         
        GameObject.Find("Player").transform.position = new Vector2(-29.48f, 7.32f);
        GameObject.Find("Player").GetComponent<Player>()._Restart();
        
        while (black.color.a > 0.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a - (Time.deltaTime / 3.0f));
            yield return null;
        }
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = GameObject.Find("BGM").GetComponent<BGM>().bgm[1];
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        ani.SetBool("EditChk", false);
        gameObject.SetActive(false);
         
    }
}
