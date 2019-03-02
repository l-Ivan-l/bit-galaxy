using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
  private static Singleton instance = null;
  private Singleton() { }

  private int highScore = 0;
  private int totalPlanets = 0;

  public static Singleton GetInstance()
  {
      if (instance == null)
          instance = new Singleton();

      return instance;
  }

  public int HighScore
  {
    get {return highScore;}
    set {highScore = value;}
  }

  public int TotalPlanets
  {
    get {return totalPlanets;}
    set {totalPlanets = value;}
  }

}//class
