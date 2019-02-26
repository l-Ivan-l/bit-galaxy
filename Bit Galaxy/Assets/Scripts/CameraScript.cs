using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Animator anim;

    void Awake() {
      anim = GetComponent<Animator>();
    }

    public void CamShake() {
      anim.SetTrigger("Shake");
    }

}//class
