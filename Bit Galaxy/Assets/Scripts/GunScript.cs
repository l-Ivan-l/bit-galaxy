﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private float speed = 15f;

    // Update is called once per frame
    void Update()
    {
      Vector2 gunDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
      float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
      Quaternion gunRotation = Quaternion.AngleAxis(angle, Vector3.forward);
      transform.rotation = Quaternion.Slerp(transform.rotation, gunRotation, speed * Time.deltaTime);
    }

}//class
