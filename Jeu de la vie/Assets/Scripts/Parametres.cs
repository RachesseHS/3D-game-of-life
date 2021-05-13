using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parametres : MonoBehaviour
{
    //public Slider mSlider;

    /*
    public InputField SousPopulationInput;
    public InputField SurPopulationInput;
    public InputField NaissanceMinimal;
    */

    public static int saveSsp = Cell.SousPop;
    public static int saveSrp = Cell.SurPop;
    public static int saveNMin = Cell.naitreMin;
    public static int saveNMax = Cell.naitreMax;
    public static int saveNbCases = Cell.NbCasesParAxe;
    public static bool saveMoore = Cell.moore;
    public static bool saveTor = Cell.toroidale;
    public static bool saveColor = Cell.codeCouleur;
    public static int saveVoisins = Cell.voisinCases;
    public static int saveMfps = Cell.maxfps;
    public static bool saveSphere = Cell.SphereCube;
    //
    void start()
    {
        //RetourSave();
        //ToggleVerif();
    }
    
        
    void Update()
    {

        /* mSlider.value = 1f;
         mSlider.maxValue = 1000f;
        */
        //gameObject.GetComponent<SousPopulationInput>().placeholder.GetComponent<Text>().text = Cell.SousPop;
        //gameObject.GetComponent.SousPopulationInput.placeholder.GetComponent<Text>().text = Cell.SousPop.ToString();
        //gameObject.GetComponent<inputField>().placeholder.GetComponent<Text>().text = Cell.SousPop.ToString();

        RecupererVariablesCell();
        SliderVerif();
        //TextTest();

        //ToggleVerif();



        //Debug.Log("Cell.naitreMin = " + Cell.naitreMin.ToString());
    }

    /*
    public void TextTest()
    {
        GameObject.Find("TextTest").GetComponent<Text>().text = "test " + Cell.NbCasesParAxe.ToString();
    }
    */

    //permet de changer la taille de la grille du jeu de la vie
    public void ChangeTailleGrille(string chaine){
        int tailleGrille;
        //Convertit la représentation sous forme de chaîne d'un nombre en son équivalent entier
        bool verif = int.TryParse(chaine, out tailleGrille);

        if (verif)
        {
            Cell.NbCasesParAxe = tailleGrille;
            Debug.Log("Cell.NbCasesParAxe = " + Cell.NbCasesParAxe.ToString());
        }

        else
            Debug.Log("erreur de saisie");
    }





    public void ChangeSousPop(string strSousPop)
    {
        int tailleSousPop;
        //Convertit la représentation sous forme de chaîne d'un nombre en son équivalent entier
        bool verif = int.TryParse(strSousPop, out tailleSousPop);

        if (verif)
        {
            Cell.SousPop = tailleSousPop;
            Debug.Log("Cell.SousPop = "+ Cell.SousPop.ToString());
        }

        else
            Debug.Log("erreur de saisie");
    }

    public void ChangeSurPop(string strSurPop)
    {
        int tailleSurPop;
        //Convertit la représentation sous forme de chaîne d'un nombre en son équivalent entier
        bool verif = int.TryParse(strSurPop, out tailleSurPop);

        if (verif)
        {
            Cell.SurPop = tailleSurPop;
            Debug.Log("Cell.SurPop = " + Cell.SousPop.ToString());

        }

        else
            Debug.Log("erreur de saisie");
    }

    public void NaissanceMin(string str)
    {
        int Min;
        //Convertit la représentation sous forme de chaîne d'un nombre en son équivalent entier
        bool verif = int.TryParse(str, out Min);

        if (verif)
        {
            Cell.naitreMin = Min;
            Debug.Log("Cell.naitreMin = " + Cell.naitreMin.ToString());
        }

        else
            Debug.Log("erreur de saisie");
    }

    public void NaissanceMax(string str)
    {
        int Max;
        //Convertit la représentation sous forme de chaîne d'un nombre en son équivalent entier
        bool verif = int.TryParse(str, out Max);

        if (verif)
        {
            Cell.naitreMax = Max;
            Debug.Log("Cell.naitreMax = " + Cell.naitreMax.ToString());
        }

        else
            Debug.Log("erreur de saisie");
    }

    public void ChangeVitesse(float valeur)
    {  

        Cell.maxfps = (int)valeur;
        Debug.Log("Cell.maxfps de param = " + Cell.maxfps.ToString());
        //Debug.Log("durr = " + speed.ToString());

    }
    

    public void ChangeVoisinCases(float speed)
    {

        Cell.voisinCases = (int)speed;
        Debug.Log("Cell.voisinCases de param = " + Cell.voisinCases.ToString());
        

    }


    
    public void ChangeForme(float forme)
    {

        Cell.SphereCube = (forme >= 0.5) ? true : false ;
        Debug.Log("Cell.SphereCube de param = " + Cell.SphereCube.ToString());
    }
    

    public void Couleur()
    {
        Cell.codeCouleur = !Cell.codeCouleur;
        Debug.Log("Cell.codeCouleur = " + Cell.codeCouleur.ToString());
    }
 


    public void ToiroidalMode()
    {
        Cell.toroidale = !Cell.toroidale;
        Debug.Log("Cell.toroidale = " + Cell.toroidale.ToString());
    }


 
    public void MooreMode()
    {
        Cell.moore = !Cell.moore;
        Debug.Log("Cell.moore = " + Cell.moore.ToString());
    }


    public void RecupererVariablesCell()
    {
        GameObject.Find("SousPopulationInput").GetComponent<InputField>().placeholder.GetComponent<Text>().text = Cell.SousPop.ToString();
        GameObject.Find("SurPopulationInput").GetComponent<InputField>().placeholder.GetComponent<Text>().text = Cell.SurPop.ToString();
        GameObject.Find("NaissanceMinimal").GetComponent<InputField>().placeholder.GetComponent<Text>().text = Cell.naitreMin.ToString();
        GameObject.Find("MaissanceMaximal").GetComponent<InputField>().placeholder.GetComponent<Text>().text = Cell.naitreMax.ToString();
        GameObject.Find("TailleGrille").GetComponent<InputField>().placeholder.GetComponent<Text>().text = Cell.NbCasesParAxe.ToString();
    }

    
    public void ToggleVerif()
    {
        GameObject.Find("Toroïdal").GetComponent<Toggle>().isOn = Cell.toroidale;



        //GameObject.Find("Couleur").GetComponent<Toggle>().isOn = !GameObject.Find("Couleur").GetComponent<Toggle>().isOn;

        GameObject.Find("Couleur").GetComponent<Toggle>().isOn = Cell.codeCouleur;


        /*
        if (Cell.moore = true)
            GameObject.Find("MooreToggle").GetComponent<Toggle>().isOn = true;
        else
            GameObject.Find("MooreToggle").GetComponent<Toggle>().isOn = false;
        */

    }
    

    
    public void SliderVerif() 
    {
        GameObject.Find("VitesseSlider").GetComponent<Slider>().value = Cell.maxfps;
        GameObject.Find("VoisinSlider").GetComponent<Slider>().value = Cell.voisinCases;

        GameObject.Find("CameraSpeedSlider").GetComponent<Slider>().value = View.sensitivityHor;
        GameObject.Find("MoveSpeedSlider").GetComponent<Slider>().value = Move.speed;

    }

    public void VitesseRotCam(float speed)
    {
        View.sensitivityHor = View.sensitivityVert = speed;
        Debug.Log("Cell.voisinCases de param = " + Move.speed.ToString());
    }

    public void VitesseDeplacment(float speed)
    {
        Move.speed= speed;
        Debug.Log("Cell.voisinCases de param = " + Move.speed.ToString());
            }


    /*
    public void TestMoore()
    {
        GameObject.Find("MooreToggle").GetComponent<Toggle>().isOn = !GameObject.Find("MooreToggle").GetComponent<Toggle>().isOn;


    }
    */

    /*
    public void RetourSave()
    {
        int saveSsp = Cell.SousPop;
        int saveSrp = Cell.SurPop;
        int saveNMin= Cell.naitreMin;
        int saveNMax = Cell.naitreMax;
        //int saveNbCases= Cell.NbCasesParAxe;
        bool saveMoore= Cell.moore;
        bool saveTor= Cell.toroidale;
        bool saveColor= Cell.codeCouleur;
        int saveVoisins=Cell.voisinCases;
        int saveMfps =Cell.maxfps;

    }
    */

    public void RetourCancel()
    {
        Cell.SousPop = saveSsp;
        Cell.SurPop = saveSrp;
        Cell.naitreMin = saveNMin;
        Cell.naitreMax = saveNMax;
        Cell.NbCasesParAxe = saveNbCases;
        Cell.moore = saveMoore;
        Cell.toroidale = saveTor;
        Cell.codeCouleur = saveColor;
        Cell.voisinCases = saveVoisins;
        Cell.maxfps = saveMfps;
        Cell.SphereCube = saveSphere;
}

}
