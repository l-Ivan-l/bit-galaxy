using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlanetScript planet;
    private Rigidbody2D playerBody;
    private Transform playerTransform;
    private Animator playerAnim;

    private float moveSpeed = 5f;
    private float h_Move;
    private Vector2 moveDir;

    void Awake() {
      playerTransform = transform;
      playerBody = GetComponent<Rigidbody2D>();
      playerAnim = GetComponent<Animator>();
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
      h_Move = Input.GetAxisRaw("Horizontal");
      moveDir = new Vector2(h_Move, 0f).normalized;
      playerAnim.SetInteger("Speed", (int)moveDir.x);
      ChangeDirection(h_Move);
    }

    void ChangeDirection(float direction) {
      Vector3 tempScale = transform.localScale;
      if(direction > 0f) {
        tempScale.x = 1f;
      } else if(direction < 0f) {
        tempScale.x = -1f;
      }
      transform.localScale = tempScale;
      transform.GetChild(0).gameObject.transform.localScale = tempScale;
    }

}//class
























//space
