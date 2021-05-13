using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageInformation : MonoBehaviour
{
    public static bool infoMenu = false;
    public GameObject InfoGame;
    public GameObject AleaDisable;
    public GameObject AleaEnable;

    //private int densiteStat = 0;

    void Start()
    {
        //GameObject.Find("TextDensite").GetComponent<Text>().text = "Densit�: " + Cell.densite + "%";
    }


    void update()
    {
        /*
        GameObject.Find("TextDensite").GetComponent<Text>().text = "Densit�: " + densiteStat + "%";
        densiteStat = Cell.densite;
        */
    }
    

    public void afficherInfo()
    {
        infoMenu = !infoMenu;
        if (infoMenu)
            InfoGame.SetActive(true);
        else
            InfoGame.SetActive(false);

        InfoInGame();
    }


    public void Aleatoire()
    {
       
       
        if (Cell.choix == 0)
        {
            Cell.choix = 1;
            AleaDisable.SetActive(false);
            AleaEnable.SetActive(true);
            Cell.SousPop = 5;
            Cell.SurPop = 7;
            Cell.naitreMin = 6;
            Cell.naitreMax = 6;
            Cell.moore = true;
            Cell.toroidale = false;
        }
        else
        {
            Cell.choix = 0;
            AleaEnable.SetActive(false);
            AleaDisable.SetActive(true);
            Cell.SousPop = 5;
            Cell.SurPop = 7;
            Cell.naitreMin = 6;
            Cell.naitreMax = 6;
            Cell.moore = true;
            Cell.toroidale = true;
        }

    }


 /*
    public void TextTest()
    {
        GameObject.Find("TextInfo").GetComponent<Text>().text = Cell.densite.ToString();
    }
 */

    /*
    public void Densite()
    {
        GameObject.Find("TextDensite").GetComponent<Text>().text = "Densit�: " + Cell.densite + "%";
    }
 */

    public void InfoInGame()
    {

        GameObject.Find("TextInfoGame").GetComponent<Text>().text = "--------------------------------" +"\nMode couleur : " + Cell.codeCouleur.ToString() + "\nMode Toro�dal : " + Cell.toroidale.ToString() + "\nMode Moore : " + Cell.moore.ToString() + "\n-------------------------------" 
        +"\nMort sous-population : " + Cell.SousPop.ToString() + "\nMort sur-population : " + Cell.SurPop.ToString() + "\nNaissance minimal : " + Cell.naitreMin.ToString() 
        + "\nNaissance maximal : " + Cell.naitreMax.ToString() + "\nTaille de la grille :  " + Cell.NbCasesParAxe.ToString() + "^3"
        + "\n--------------------------------" + "\nNombre de voisins : " + Cell.voisinCases.ToString();

        //GameObject.Find("TextDensite").GetComponent<Text>().text = "Densit�: " + Cell.densite + "%";

    }


    //Ancienne m�thode 
    
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2, 0, 300, 100), "Densit�: " + Cell.densite + "%");
       // GUI.Label(new Rect(Screen.width / 2 -10, 20, 300, 100), "G�n�ration: " + Cell.generation );

        /*
        GUI.Label(new Rect(1070, 0, 300, 100), "Mode couleur : " + Cell.codeCouleur.ToString() + "\nMode Toro�dal : " + Cell.toroidale.ToString() + "\nMode Moore : " + Cell.moore.ToString() + "\n---------------------------------");
        GUI.Label(new Rect(1070, 60, 300, 100),"Mort sous-population : " + Cell.SousPop.ToString() + "\nMort sur-population : " + Cell.SurPop.ToString() + "\nNaissance minimal : " + Cell.naitreMin.ToString() + "\nNaissance maximal : " + Cell.naitreMax.ToString() + "\nTaille de la grille :  " + Cell.NbCasesParAxe.ToString()+"^3");
        GUI.Label(new Rect(1070, 140, 300, 100), "---------------------------------" + "\nNombre de voisins : " + Cell.voisinCases.ToString());
        */

        //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "O");

        //GUI.Label(new Rect(Screen.width/2,0,300,100),"x:"+transform.forward.x+"\ny:"+transform.forward.y+"\nz:"+transform.forward.z);
    }
   
}
