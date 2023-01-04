using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float speed=20f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity=transform.right*speed;
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if (gameObject!=null){
            Destroy(gameObject);
        }      
    }
}
