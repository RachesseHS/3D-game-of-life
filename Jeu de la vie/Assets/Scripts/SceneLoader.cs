using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void AllerMenuPrin()
    {
        SceneManager.LoadScene(0);
    }

    public void AllerMenuOptions()
    {
        SceneManager.LoadScene(2);
    }


    public void AllerMenuParam()
    {
        SceneManager.LoadScene(3);
    }

    public void AllerMenuOptionsGame()
    {
        SceneManager.LoadScene(4);
    }

    public void AllerCredit()
    {
        SceneManager.LoadScene(5);
    }

    public void AllerMenuParamGame()
    {
        SceneManager.LoadScene(6);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void AllerSimulationEnregistre()
    {
        SceneManager.LoadScene(7);
    }

    public void AllerGuide()
    {
        SceneManager.LoadScene(8);
    }

    public void AllerMotif()
    {
        SceneManager.LoadScene(9);
    }
}