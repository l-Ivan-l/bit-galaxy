using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    private float gravity = -30f;
    private int planetLife = 3;

    public Sprite[] planetDetails = new Sprite[10];
    private int backDetailsIndex;
    private int frontDetailsIndex;
    private Color baseColor = Color.white;
    private Color backColor = Color.white;
    private Color frontColor = Color.white;
    private SpriteRenderer baseRender;
    private SpriteRenderer backRender;
    private SpriteRenderer frontRender;

    private float planetTranslationSpeed;
    public int planetState;

    public Transform centerPos;
    public Transform leftPos;
    public Transform rightPos;

    void Awake() {
      baseRender = GetComponent<SpriteRenderer>();
      backRender = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
      frontRender = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
    }

    void Start() {
      GeneratePlanet();
      planetState = 1;
      planetTranslationSpeed = 20f;
    }

    void Update() {
      switch(planetState) {
        case 1:
          PlanetToCenter();
        break;

        case 2:
          PlanetToLeft();
        break;

        default:
        break;
      }
    }

    void GeneratePlanet() {
      baseColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
      backColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
      frontColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
      backDetailsIndex = Random.Range(0, planetDetails.Length);
      frontDetailsIndex = FrontDetIndex(0, planetDetails.Length, backDetailsIndex);
      baseRender.color = baseColor;
      backRender.sprite = planetDetails[backDetailsIndex];
      backRender.color = backColor;
      frontRender.sprite = planetDetails[frontDetailsIndex];
      frontRender.color = frontColor;
      RandomRotation();
    }

    private int FrontDetIndex(int min, int max, int except) {
      int result = Random.Range(min, max - 1);
      if (result >= except) result += 1;
      return result;
    }

    void RandomRotation() {
      var rotationVector = transform.rotation.eulerAngles;
      rotationVector.z = Random.Range(0f, 360f);
      transform.rotation = Quaternion.Euler(rotationVector);
    }

    public void Attract(Transform body, Rigidbody2D physxBody) {
      Vector3 gravityUp = (body.position - transform.position).normalized;
      Vector3 bodyUp = body.up;
      physxBody.AddForce(gravityUp * gravity);
      Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
      body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }

    void PlanetToCenter() {
      transform.position = Vector3.MoveTowards(transform.position, centerPos.position, planetTranslationSpeed * Time.deltaTime);
    }

    void PlanetToLeft() {
      transform.position = Vector3.MoveTowards(transform.position, leftPos.position, planetTranslationSpeed * Time.deltaTime);
      if(transform.position == leftPos.position) {
        StartCoroutine(DestroyPlanet(0.5f));
      }
    }

    public void PlanetHited() {
      planetLife -= 1;
      if(planetLife <= 0) {
        //GAME OVER
      }
    }

    IEnumerator DestroyPlanet(float timer) {
      yield return new WaitForSeconds(timer);
      Destroy(gameObject);
    }

}//class
