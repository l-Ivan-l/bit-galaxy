using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
  public GameObject MainPanel, PanelControls, PanelCredits;

  public void Play(string sceneName)
  {
      StartCoroutine(LoadScene(sceneName));
  }

  public void Controles()
  {
      StartCoroutine(ControlesCo());
  }
  public void Creditos()
  {
      StartCoroutine(CreditosCo());
  }
  public void Regresar()
  {
      StartCoroutine(RegresarCo());
  }

  IEnumerator LoadScene(string sceneName) {
    yield return new WaitForSeconds(0.7f);
    SceneManager.LoadScene(sceneName);
  }
  IEnumerator ControlesCo()
  {
      yield return new WaitForSeconds(0.5f);
      MainPanel.SetActive(false);
      PanelControls.SetActive(true);
  }
  IEnumerator CreditosCo()
  {
      yield return new WaitForSeconds(0.5f);
      MainPanel.SetActive(false);
      PanelCredits.SetActive(true);
  }
  IEnumerator RegresarCo()
  {
      yield return new WaitForSeconds(0.5f);
      PanelCredits.SetActive(false);
      PanelControls.SetActive(false);
      MainPanel.SetActive(true);
  }

}//class
