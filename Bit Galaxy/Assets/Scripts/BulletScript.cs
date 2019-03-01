using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody2D bulletBody;
    private CameraScript Camera;

    public GameObject bulletExplosion;

    void Awake() {
      bulletBody = GetComponent<Rigidbody2D>();
      Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
      bulletBody.velocity = transform.right * speed;
      Destroy(gameObject, 0.8f);
    }

    void OnCollisionEnter2D(Collision2D target) {
      if(target.gameObject.CompareTag("Planet")) {
        Destroy(gameObject);
        Instantiate(bulletExplosion, transform.position, transform.rotation);
      }
    }

    public void DestroyBullet() {
      Camera.CamShake();
      Instantiate(bulletExplosion, transform.position, transform.rotation);
      Destroy(gameObject);
    }

}//class
