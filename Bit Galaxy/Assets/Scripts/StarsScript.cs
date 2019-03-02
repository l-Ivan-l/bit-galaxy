using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsScript : MonoBehaviour
{
    private float starsTranslationSpeed;
    [HideInInspector]
    public int starsState;
    public Transform centerPos;
    public Transform leftPos;
    public Transform rightPos;
    // Start is called before the first frame update
    void Start()
    {
      starsTranslationSpeed = 15f;
      if(transform.position == centerPos.position) {
        starsState = 2;
      } else if(transform.position == leftPos.position) {
        starsState = 1;
      } else if(transform.position == rightPos.position) {
        starsState = 3;
      }
      if(starsState == 2) {
        starsState = 1;
      } else if(starsState == 3) {
        starsState = 2;
      }
    }

    // Update is called once per frame
    void Update()
    {
      switch(starsState) {
        case 1:
          StarsToLeft();
        break;

        case 2:
          StarsToCenter();
        break;

        case 3:
          StarsToRight();
        break;
      }
      if(transform.position == centerPos.position) {
        starsState = 2;
      } else if(transform.position == leftPos.position) {
        starsState = 1;
      } else if(transform.position == rightPos.position) {
        starsState = 3;
      }
    }

    void StarsToCenter() {
      transform.position = Vector3.MoveTowards(transform.position, centerPos.position, starsTranslationSpeed * Time.deltaTime);
    }

    void StarsToLeft() {
      transform.position = Vector3.MoveTowards(transform.position, leftPos.position, starsTranslationSpeed * Time.deltaTime);
      if(transform.position == leftPos.position) {
        StarsToRight();
      }
    }

    void StarsToRight() {
      transform.position = rightPos.position;
    }

}//class
