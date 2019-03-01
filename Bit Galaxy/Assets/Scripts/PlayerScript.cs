using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private PlanetScript planet;
    private Rigidbody2D playerBody;
    private Transform playerTransform;
    private Animator playerAnim;

    private float moveSpeed = 5f;
    private bool canMove;
    private float h_Move;
    private Vector2 moveDir;
    private bool isOnPlanet;
    private Color invisible = Color.white;
  	private Color visible = Color.white;
    private SpriteRenderer playerRender;
    private SpriteRenderer armRender;
    private GunScript gun;
    public LayerMask planetLayer;

    void Awake() {
      playerTransform = transform;
      playerBody = GetComponent<Rigidbody2D>();
      playerAnim = GetComponent<Animator>();
      playerRender = GetComponent<SpriteRenderer>();
      armRender = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
      gun = transform.GetChild(0).gameObject.GetComponent<GunScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
      isOnPlanet = false;
      playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
      playerBody.gravityScale = 0f;
      invisible.a = 0;
  		visible.a = 1;
      canMove = false;
      playerRender.color = invisible;
      armRender.color = invisible;
    }

    // Update is called once per frame
    void Update()
    {
      if(isOnPlanet && canMove && planet != null) {
        planet.Attract(playerTransform, playerBody);
      }
      PlayerController();
    }

    void FixedUpdate() {
      Vector3 playerPosition = new Vector3(playerBody.position.x, playerBody.position.y, 0f);
      playerBody.MovePosition(playerPosition + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
      RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 5f, planetLayer);
      if(hit) {
        if (hit.collider.gameObject.tag == "Planet") {
          planet = hit.collider.gameObject.GetComponent<PlanetScript>();
          isOnPlanet = true;
        }
      }
    }

    void PlayerController() {
      if(canMove && isOnPlanet) {
        h_Move = Input.GetAxisRaw("Horizontal");
        moveDir = new Vector2(h_Move, 0f).normalized;
        playerAnim.SetInteger("Speed", (int)moveDir.x);
        ChangeDirection(h_Move);
      }
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

    public void Teletransport_Out() {
      canMove = false;
      gun.canShoot = false;
      isOnPlanet = false;
      planet = null;
      playerRender.color = invisible;
      armRender.color = invisible;
      var rotationVector = transform.rotation.eulerAngles;
      rotationVector.z = 0f;
      transform.rotation = Quaternion.Euler(rotationVector);
    }

    public void Teletransport_In(Vector3 newPosition) {
      playerRender.color = visible;
      armRender.color = visible;
      canMove = true;
      gun.canShoot = true;
      transform.position = newPosition;
      var rotationVector = transform.rotation.eulerAngles;
      rotationVector.z = 0f;
      transform.rotation = Quaternion.Euler(rotationVector);
    }

}//class
























//space
