using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLobby : MonoBehaviour
{
    public GameObject black;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BtnDown()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        black.SetActive(true);
        while (black.GetComponent<Image>().color.a < 1.0f)
        {
            black.GetComponent<Image>().color = new Color(black.GetComponent<Image>().color.r, black.GetComponent<Image>().color.g, black.GetComponent<Image>().color.b, black.GetComponent<Image>().color.a + (Time.deltaTime / 3.0f));
            yield return null;
        }
        SceneManager.LoadScene("GameScene");
    }
}
