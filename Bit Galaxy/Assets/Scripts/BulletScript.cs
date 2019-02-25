using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody2D bulletBody;

    void Awake() {
      bulletBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
      bulletBody.velocity = transform.right * speed;
    }

}//class
