using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlanetScript planet;
    private Rigidbody2D playerBody;
    private Transform playerTransform;

    private float moveSpeed = 10f;
    private Vector2 moveDir;

    void Awake() {
      playerTransform = transform;
      playerBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
      playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
      playerBody.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
      planet.Attract(playerTransform, playerBody);
      PlayerController();
    }

    void FixedUpdate() {
      Vector3 playerPosition = new Vector3(playerBody.position.x, playerBody.position.y, 0f);
      playerBody.MovePosition(playerPosition + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }

    void PlayerController() {
      moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0f).normalized;
    }

}//class
























//space
