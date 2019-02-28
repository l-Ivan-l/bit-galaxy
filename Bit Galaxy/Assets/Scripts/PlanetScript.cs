using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    private float gravity = -30f;
    private int planetLife = 3;

    public void Attract(Transform body, Rigidbody2D physxBody) {
      Vector3 gravityUp = (body.position - transform.position).normalized;
      Vector3 bodyUp = body.up;
      physxBody.AddForce(gravityUp * gravity);
      Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
      body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }

    public void PlanetHited() {
      planetLife -= 1;
      if(planetLife <= 0) {
        //GAME OVER
      }
    }

}//class
