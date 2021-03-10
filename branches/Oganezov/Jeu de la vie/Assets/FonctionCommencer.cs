using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FonctionCommencer : MonoBehaviour
{
    public void boutonCommencer()
    {
        SceneManager.LoadScene(1);
    }
}
