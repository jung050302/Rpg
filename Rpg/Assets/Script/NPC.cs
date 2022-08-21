using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPC : MonoBehaviour
{
    //x:-45~45,y:-16~16;
    // Start is called before the first frame update
    NavMeshAgent nav;
    int rdm_x;
    int rdm_y;
    Vector2 pos ;
    float timer;
    int rdmTimer;
    float scale;
    public Animator ani;
    void Start()
    {
        timer = 0;
        rdm_x = Random.Range(-44, -15);
        rdm_y = Random.Range(14, 1);
        rdmTimer = Random.Range(1, 4);
        scale = this.gameObject.transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector2(rdm_x, rdm_y);
         
        timer += Time.deltaTime;
        if (timer >= rdmTimer)
        {
            if (this.gameObject.transform.position.x != rdm_x && this.gameObject.transform.position.y != rdm_y)
            {
                 
                this.gameObject.transform.position = Vector2.MoveTowards
                    (
                        gameObject.transform.position, pos, 3 * Time.deltaTime
                    );
                ani.SetFloat("RunState", 0.5f);
            }
            else if (this.gameObject.transform.position.x == rdm_x && this.gameObject.transform.position.y == rdm_y)
            {
                 
                rdm_x = Random.Range(-44, -15);
                rdm_y = Random.Range(14, 1);
                rdmTimer = Random.Range(1, 4);
                timer = 0;
                ani.SetFloat("RunState", 0f);
            }
            else
            {
                rdm_x = Random.Range(-44, -15);
                rdm_y = Random.Range(14, 1);
                rdmTimer = Random.Range(1, 4);
                timer = 0;
                ani.SetFloat("RunState", 0f);
            }
        }
        if (this.gameObject.transform.position.x < rdm_x)
        {
            this.gameObject.transform.localScale = new Vector2(-scale, this.gameObject.transform.localScale.y);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector2(scale, this.gameObject.transform.localScale.y);
        }
        //if (this.gameObject.transform.position.x > 0)
        //{
        //    this.gameObject.transform.localScale = new Vector2(-scale, this.gameObject.transform.localScale.y);
        //}
        //else
        //{
        //    this.gameObject.transform.localScale = new Vector2(scale, this.gameObject.transform.localScale.y);
        //}
    }
     
}
