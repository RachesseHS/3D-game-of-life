using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AffichageSauvegarde : MonoBehaviour
{
    public GameObject canvas;
    public GameObject simulation;
    public Text text;
    public GameObject panel;
    public GameObject panel2;
    private bool panelOuvert = false;
    public Vector3 position;
    private List<string> nomBouton;
    private int y = 150;
    private int x = -200;
    private string nom;
    /********************************************************/
    public static bool passage = false;
    public static int NbCasesParAxe;
    public static int SousPop;
    public static int SurPop;
    public static int naitreMin;
    public static int naitreMax;
    public static int voisinCases;
    public static bool toroidale;
    public static bool codeCouleur;
    public static bool moore;
    public static bool[,,] Grille ;
    private GameObject monObjet;

    private string nomObjet;
    Cell grilleSauvegarde;
    public InputField BtnNouveauNom;
    void Start()
    {

        panel.SetActive(false);
        nomBouton = new List<string>();
        string[] files = Directory.GetFiles(Application.dataPath+ "/sauvegardes/");
        int nombre;
        for (int i=0; i < files.Length; i++)
        {
            if(files[i].Substring(files[i].Length-3,3) == "txt")
            {
                nombre = files[i].LastIndexOf("/") + 1;
                nom = files[i].Substring(nombre, files[i].Length - 4 - nombre);
                text.text = nom;
                
                simulation.name = nom;
                
                position = new Vector3(x, y, 0);

                GameObject boutonCopy = Instantiate(simulation, canvas.transform);
                if (x == 600)
                {
                    x = -400;
                    y -= 120;
                }
                x += 200;
                boutonCopy.transform.localPosition = position;
                nomBouton.Add(boutonCopy.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool[,,] lireFichier(string nomfichier)
    {
        string sauvegardeString = File.ReadAllText(Application.dataPath + "/sauvegardes/" + nomfichier + ".txt");
        string[] sauvegardetab = sauvegardeString.Split(new[] { "/" }, System.StringSplitOptions.None);
        NbCasesParAxe = int.Parse(sauvegardetab[0]);
        SousPop = int.Parse(sauvegardetab[1]);
        SurPop = int.Parse(sauvegardetab[2]);
        naitreMin = int.Parse(sauvegardetab[3]);
        naitreMax = int.Parse(sauvegardetab[4]);
        voisinCases = int.Parse(sauvegardetab[5]);
        toroidale = bool.Parse(sauvegardetab[6]);
        codeCouleur = bool.Parse(sauvegardetab[7]);
        moore = bool.Parse(sauvegardetab[8]);
        int i = 8;
        bool[,,] grille = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        //grilleSauvegarde = new Cell(NbCasesParAxe, SousPop, SurPop, naitre, voisinCases, toroidale, codeCouleur, moore);

        for (int x = 0; x < NbCasesParAxe; x++)
        {
            for (int y = 0; y < NbCasesParAxe; y++)
            {
                for (int z = 0; z < NbCasesParAxe; z++)
                {

                    grille[x, y, z] = bool.Parse(sauvegardetab[i]);
                    i++;

                }
            }
        }
        return grille;
    }

    public void AppuyerBouton(GameObject sender) 
    {      
        if (panelOuvert == false)
        {
            monObjet = sender;
            panelOuvert = true;
            panel.SetActive(panelOuvert);
            nomObjet = sender.name.Substring(0, sender.name.Length - 7);
            
        }
          
    }

    public void lectureFichier()
    {
        passage = true;
        Grille = lireFichier(nomObjet);
        SceneManager.LoadScene(1);        
    }

    public void supprimerFichier()
    {
        File.Delete(Application.dataPath + "/sauvegardes/" + nomObjet + ".txt");
        File.Delete(Application.dataPath + "/sauvegardes/" + nomObjet + ".txt" + ".meta");

        monObjet.SetActive(false);
        panel.SetActive(false);
        panelOuvert = false;
    }

    public void retour()
    {
        panel.SetActive(false);
        panel2.SetActive(false);
        panelOuvert = false;
    }

    public void modifierNom()
    {
        panel.SetActive(false);
        panel2.SetActive(true);
    }

    public void validerNouveauNom()
    {
        File.Copy(Application.dataPath + "/sauvegardes/"+ nomObjet + ".txt", Application.dataPath + "/sauvegardes/" + BtnNouveauNom.text+ ".txt" );
        File.Delete(Application.dataPath + "/sauvegardes/" + nomObjet + ".txt");
        File.Delete(Application.dataPath + "/sauvegardes/" + nomObjet + ".txt" + ".meta");
        panel2.SetActive(false);
    }

}
