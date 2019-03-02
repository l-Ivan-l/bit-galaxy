using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteScript : MonoBehaviour
{
    public Transform meteoriteAttractor;
    private Rigidbody2D meteoriteBody;
    private float meteoriteSpeed;
    private float rotationSpeed;

    public GameObject explosionEffect;

    void Awake() {
      meteoriteBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
      meteoriteSpeed = 0.3f;
      rotationSpeed = 20f;
      RandomSize();
    }

    void Update() {
      transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      meteoriteBody.MovePosition(Vector2.Lerp(transform.position, meteoriteAttractor.position, meteoriteSpeed * Time.deltaTime));
    }

    void RandomSize() {
      Vector3 scaleVector = transform.localScale;
      scaleVector.x = Random.Range(0.75f, 1.5f);
      scaleVector.y = scaleVector.x;
      transform.localScale = scaleVector;
    }

    void OnCollisionEnter2D(Collision2D target) {
      if(target.gameObject.CompareTag("Bullet")) {
        //Aumentar score aquí
        GameController.Instance.SetScore(5);
        target.gameObject.GetComponent<BulletScript>().DestroyBullet();
        DestroyMeteorite();
      } else if(target.gameObject.CompareTag("Planet")) {
        target.gameObject.GetComponent<PlanetScript>().PlanetHited();
        DestroyMeteorite();
      } else if(target.gameObject.CompareTag("Player")) {
        //GAME OVER
      }
    }

    void DestroyMeteorite() {
      Instantiate(explosionEffect, transform.position, transform.rotation);
      Destroy(gameObject);
    }

}//class
