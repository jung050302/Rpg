using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    Text _text;
    void Start()
    {
        _text = this.gameObject.GetComponent<Text>();
        this.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(0, 2 * Time.deltaTime, 0);
        StartCoroutine(Dameges());
        Invoke("del", 2f);
    }
    IEnumerator Dameges()
    {
         
        
        while (_text.color.a > 0.0f)
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - (Time.deltaTime / 4.0f));
            yield return null;
        }
         
         
         
    }
    void del()
    {
        Destroy(gameObject);
    }
}
