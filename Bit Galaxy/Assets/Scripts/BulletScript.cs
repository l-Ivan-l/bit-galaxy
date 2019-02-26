using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody2D bulletBody;
    private CameraScript Camera;

    void Awake() {
      bulletBody = GetComponent<Rigidbody2D>();
      Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
      Camera.CamShake();
      bulletBody.velocity = transform.right * speed;
      Destroy(gameObject, 0.8f);
    }

}//class
