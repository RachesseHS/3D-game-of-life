using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //Param modifiables par utilisateur 
    public int NbCasesParAxe = 12; // Nombre de cases/cellules maximal par axe 
    public int SousPop = 5; // Nombre de voisin pour considérer comme Sous population 
    public int SurPop = 7;// Nombre de voisin pour considérer comme Sur population 
    public int naitre = 0; //Nombre de voisin pour faire naitre la cellules
    public int voisinCases = 1; // Nombres de cases requises pour comsidérer comme voisin. (si voisinCases = 1 alors cellules a 26voisins, et si voisinCases = 2 alors 124 voisins)
        
    
    public GameObject[,,] GameObjectList;
    GameObject parent;
    public bool[,,] Grille;
    public bool[,,] GrilleAjour;



    // Use this for initialization
    void Start()
    {
        RegenererGrille();
        affiche();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");
        Update1();
    }

    void Update1()
    {
        RegenererGrille();
        affiche();
    }

    /**
     * Fonction qui initialise au début du jeux 
     * 
     **/
    void initGrille(int choix)
    {
        Grille = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        GrilleAjour = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        //Debug.Log("initGrille");

        // choix à améliorer en fonction des fontionnalités
        switch (choix)
        {
            case 0: // choix random
                initGrillerandom();
                return;
            default:
                break;
        }

    }

    
    /**
     * Fonction qui permet de retourner des chiffres hazard servi pour les initialisation random
    **/
    int random()
    {
        //Debug.Log("random");
        System.Random rand = new System.Random();
        int randomInt = rand.Next(0, (99));
        return randomInt;
    }
    
    
    
    
    /**
     * Fonction qui générer aléatoirement la grille
     * 
     **/
    void initGrillerandom()
    {
        //Debug.Log("initGrillerandom");
        for (int x = 0; x < NbCasesParAxe; x++)
        {
            for (int y = 0; y < NbCasesParAxe; y++)
            {
                for (int z = 0; z < NbCasesParAxe; z++)
                {
                    Grille[x, y, z] = true;
                    if (random() % 8 ==0)
                    {
                        Grille[x, y, z] = false;
                    }
                }
            }
        }
    }

    void initGrilleFichier()
    {
        Debug.Log("En cours");
    }
    
    

    /**
     * Fonction pour generer la nouvelle grille3D 
     **/
    void RegenererGrille()
    {
        
        //Debug.Log("RegenererGrille");
        if (Grille == null)
        {
            initGrille(0);
           return;
        }
        
        for (int x = 0; x < NbCasesParAxe; x++)
        {
            for (int y = 0; y < NbCasesParAxe; y++)
            {
                for (int z = 0; z < NbCasesParAxe; z++)
                {
                    bool cellActuelle = Grille[x, y, z];
                    int cmpt = GetNbcellVivantVoisin(x, y, z);
                    //Debug.Log("end get"); 
                    if (cellActuelle == true)
                    {
                        if (cmpt < SousPop || cmpt > SurPop)
                        {
                            GrilleAjour[x, y, z] = false;
                        }
                    }
                    else
                    {
                        if (cmpt == naitre)
                        {
                            GrilleAjour[x, y, z] = true;
                        }
                    }
                }
            }
        }

        System.Array.Copy(GrilleAjour, Grille, Grille.Length);
    }
    
    /**
	* Fonction pour compter le nombre de voisins vivant de cette cellule
	* La fonction recupère les repères 26 voisins de cette celulle [(x+(-1),y+(-1),z+(-1)), ..., (x+(-1),y+1,z+0),...,(x+1,y+1,z+1)] et des voir leurs etats actuelle
	* En cas de pas de voisin sur un(plusieurs) axe(s) (ex: sur la premier cellules avec repère(0,0,0) il aura que (2*2*2)-1=7voisins)
    * Entrée: les repère de la cellule int x, int y, int z 
	* Sortie: nombres de voisins vivant cmpt(compteur)
	**/
    int GetNbcellVivantVoisin(int x, int y, int z)
    {
        int v = voisinCases;
        //Debug.Log("GetNbcellVivantVoisin");
        int cmpt = 0;
        if (Grille[x, y, z] == true)
        {
            cmpt = -1;
        }
        //Debug.Log("for x");
        for (int i = -v; i <= v; i++){
            if (i + x >= 0 && i + x < NbCasesParAxe)
            {  
                //Debug.Log("for y");
                for (int j = -v; j <= v; j++)
                {
                    if (j + y >= 0 && j + y < NbCasesParAxe)
                    {
                  //      Debug.Log("for z");
                        for (int k = -v; k <= v; k++)
                        {
                            if (k + z >= 0 && k + z < NbCasesParAxe)
                            {
                    //            Debug.Log("if");
                                int voisin_x, voisin_y, voisin_z;
                                voisin_x = i + x;
                                voisin_y = j + y;
                                voisin_z = k + z;
                                if (Grille[voisin_x, voisin_y, voisin_z] == true)
                                { 
                                    cmpt++;
                                }
                            }
                        }
                    }
                }
            } 
        } 
        return cmpt;
    }
    
    /**
     * Fonction qui affiche la grille avec les cellules 
     */
    
    
    void affiche()
    {
        //Debug.Log("affiche");
        // creer un objet parent en cas si parent exixte pas 
        if (GameObjectList == null) {
            parent = new GameObject("parent");
            parent.transform.parent = transform;
            GameObjectList = new GameObject[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        }
        for (int x = 0; x < NbCasesParAxe; x++) 
        {
            for (int y = 0; y < NbCasesParAxe; y++)
            {
                for (int z = 0; z < NbCasesParAxe; z++)
                {
                    GameObject gameObject = GameObjectList[x, y, z];
                    if (gameObject == null) {
                        gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        gameObject.transform.parent = parent.transform;
                        gameObject.name = "(" + x + " " + y + " " + z+")";
                        gameObject.transform.position = new Vector3(x, y, z);
                        float s = .7f;
                        gameObject.transform.localScale = new Vector3(s, s, s);
                        GameObjectList[x, y, z] = gameObject; 
                    }

                    switch (Grille[x, y, z])
                    {
                        
                        case true:
                        {
                            (float r,float v,float b,float a ) couleur;
                            couleur = setCouleur(x, y, z);
                            Color color = new Color(couleur.r, couleur.v, couleur.b, couleur.a);
                            gameObject.GetComponent<Renderer>().material.color= color;
                            gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
                            break;
                        }

                        case false:
                        {   
                            
                            Color color = new Color(.5f, .5f, .5f, 0.125f);
                            gameObject.GetComponent<Renderer>().material.color = color;
                            gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
                            break;

                        }
                    }
                }
            }
        }
    }
    
    /**
     * setCouleur est une fonction qui recupère le repère de chaque cellules vivant et de modifier leur couleur en
     * fonction du nombre des voisins. Le couleur va de Vert vers du Rouge, plus elle a des voisin plus elle est vert sinon rouge
     * et en passant par bleu,
     *
     * Input: coordonnées de la cellule
     * Output un quatre-uplet de float represente la couleur.
     **/
    (float,float,float,float) setCouleur(int x, int y, int z)
    {
        int NbMax = SurPop;
        int NbMin = SousPop;
        int moy = (int) (NbMax + NbMin) / 2;
        int cmpt = GetNbcellVivantVoisin( x, y, z);
       
        if (cmpt== NbMax) // voisin = seuil de la sur population
        {
            return (0f, 0f, 0.9f, 1f);
        }

        if (cmpt == moy) // La moyenne
        {
            return (0f, 0.9f, 0f, 1f);
        }

        if (cmpt == NbMin) // voisin = seuil de la sous population
        {
            return (0.9f, 0f, 0f, 1f);
        }
        else
        {
            if (cmpt>moy)
            {
                float tempV = (cmpt - NbMin) / (moy - NbMin); //valeur Vert entre ]0,1]
                float tempB = (moy - cmpt) / (moy - NbMin); //valeur Bleu entre ]0,1]
                return (0f, tempV, tempB, 1f);
            }
            else 
            {
                float tempV = (NbMax - cmpt) / (NbMax - moy);// valeur Vert entre ]0,1]
                float tempR = (cmpt - moy) / (NbMax - moy); // valeur Rouge entre ]0,1]
                return (tempR, tempV, 0f, 1f);
            }
        }
    }
}