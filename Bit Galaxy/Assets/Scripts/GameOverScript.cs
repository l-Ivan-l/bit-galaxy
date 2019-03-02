using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
  public Text highScoreText;
  public Text totalPlanetsText;

  void Start() {
    highScoreText.text = "Highscore: " + Singleton.GetInstance().HighScore.ToString();
    totalPlanetsText.text = "Planets Saved: " + Singleton.GetInstance().TotalPlanets.ToString();
  }

}//class
