using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
  public string scene;
    // Start is called before the first frame update
  public void DESGRAÇADEUMAFUNCAO()
  {
		SceneManager.LoadScene(scene);
	}
  public void BackToMenu()
  {
		SceneManager.LoadScene("Menu");
	}
  public void QuitGame()
  {
		Application.Quit();
	}
  public void TryAgain()
  {
		SceneManager.LoadScene(PlayerStatus.previousScene);
    print("Entrou Aqui");
	}
}
