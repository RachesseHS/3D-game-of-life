using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using Random = System.Random;

using UnityEngine.SceneManagement;

public class Cell : MonoBehaviour
{
    /**
     * Param modifiables par utilisateur avec des valeurs >=0
     * les entrées doivent être vérifiée impérativement dans les zones de saisie 
     **/

    public static int NbCasesParAxe = 12; // NbCasesParAxe donne le nombre de "cases" ou "cellules" maximal par axe (donc le jeu contient au maximum NbCasesParAxe*NbCasesParAxe*NbCasesParAxe ici donc 12*12*12=1728cellules maximal)

    public static int SousPop = 5;  // Le variable SousPop donne le nombre de voisin pour considérer comme Sous population >=0

    public static int SurPop = 7; // Le variable SourPop donne le nombre de voisin pour considérer comme Sur population >=0

    public static int naitreMin = 6;     // Le variable naitre donne le nombre de voisin pour faire naitre la cellules >=0
    public static int naitreMax = 6;     // Le variable naitre donne le nombre de voisin pour faire naitre la cellules >=0
    public static int voisinCases = 1;   // Le variable voisinCases Nombres de cases requises pour comsidérer comme voisin. (si voisinCases = 1 alors cellules a 26voisins, et si voisinCases = 2 alors 124 voisins

    public static bool toroidale = true; // Le variable toroidale permet de controler le jeu si il sera toroidale (signifie qu'un des voisins de (0,0,0) sera (11,11,11) dans le cas si sur un axe a maximum 12 cases ) 

    public static bool codeCouleur = false;    // Permet de laisser à utilisateur de choisir s'il veut utiliser la code couleur 

    public static bool moore = true;   // Permet de choisir si l'utilisateur de choisir entre le voisinage de Moore ou de Von-Neumann que 

    public static int endGame = 0; //Variable pour le mode de jeu fini
    public static int generation = 0; //compteur du nombre de generation
    public static bool endGamemode = false; // verifie si le jeu est en mode "fin du monde"

    // Variables permettent de faire pause/play
    public bool pause = false;
    public GameObject BtnPlay;
    public GameObject BtnPause;

    public static int densite = 0;

    public static int choix = 1; // Permet de choisir le mode de jeu
    
    public static bool SphereCube = true; // permet à l'utilisateur de choisir entre cellule sphérique ou cubique
    public bool reset = false;  // Permet de lancer une autre  simulation

    public bool replay = false;  //Permet de relancer  la meme simulation
    
    public bool sauvegarderActuelle = false;

    public GameObject PanelMessage;
    public GameObject PanelMessage2;
    public InputField BtnNom;
    public GameObject Non;
    public GameObject Oui;
    private bool envoyer = true;
    private bool ecraseFichier = false;


    public GameObject[,,] GameObjectList;
    GameObject parent;
    public bool[,,] Grille;  // A changer le type si plusieurs etats 
    public bool[,,] GrilleAjour;
    public bool[,,] GrilleRand;
    public static bool[,,] GrilleSauvegarde;

    public float dis = 3; //distance de camera
    public float sensitivity = 1;
    public bool start = true;
    private int tmps = 0;
    
    public static int maxfps = 60;



    // Use this for initialization
    void Start()
    {
        densite = 0;
        // generation = 0;
        if (Motif.motif == true)
        {
            Grille = Motif.Grille;
            NbCasesParAxe = Motif.NbCasesParAxe;
            SousPop = Motif.SousPop;
            SurPop = Motif.SurPop;
            naitreMin = Motif.naitreMin;
            naitreMax = Motif.naitreMax;
            voisinCases = Motif.voisinCases;
            toroidale = Motif.toroidale;
            codeCouleur = Motif.codeCouleur;
            moore = Motif.moore;
            GrilleAjour = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
            Motif.motif = false;
        }

        if (AffichageSauvegarde.passage == true)
        {
            Grille = AffichageSauvegarde.Grille;
            NbCasesParAxe = AffichageSauvegarde.NbCasesParAxe;
            SousPop = AffichageSauvegarde.SousPop;
            SurPop = AffichageSauvegarde.SurPop;
            naitreMin = AffichageSauvegarde.naitreMin;
            naitreMax = AffichageSauvegarde.naitreMax;
            voisinCases = AffichageSauvegarde.voisinCases;
            toroidale = AffichageSauvegarde.toroidale;
            codeCouleur = AffichageSauvegarde.codeCouleur;
            moore = AffichageSauvegarde.moore;
            GrilleAjour = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
            AffichageSauvegarde.passage = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");
        //Debug.Log("maxfps = " + maxfps.ToString());

        /*
        if(!endGamemode){
            if(generation == endGame) {
                pause = true;
                // eventuelement ajouter un affichage de message "fin de la simulation le monde n'existe plus ...."
            }
        }
        */

       // densite = 0;
       // densite = 0;

        if (pause)
        {
            return;
        }
        if (start == false)
        {
            initGrilleMan();
        }
           

        if (replay == true)
        {
            InitGrilleReplay();
            replay = false;
        }

        if (reset == true)
        {
            InitGrilleReset();
            reset = false;
        }

        UpdateGrille();

    }
 
    void UpdateGrille()
    {

        if (tmps == maxfps)
        {
            if (start)
            { 
                RegenererGrille();
            }
            affiche();
            tmps = 0;
        }
        else
        {
            tmps += 1;
        }
        
    }

    public void Replay()
    {
        replay = true;
    }

    public void Reset()
    {
        reset = true;
        densite = 0;
        choix = 1;
        SceneManager.LoadScene(1);
        generation = 0;
    }

    /**
     * Fonction qui permet de mettre en Pause une simulation
     */
    public void BoutonPause()
    {
        if (pause == false)
        {
            pause = true;
            BtnPlay.SetActive(true);
            BtnPause.SetActive(false);

        }
        else
        {
            pause = false;
            BtnPause.SetActive(true);
            BtnPlay.SetActive(false);
            
        }

    }


    /**
     * Fonction qui initialise au début du jeux
     **/
    void initGrille(int choix)
    {
        Grille = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        GrilleAjour = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        GrilleRand = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];

        // choix à améliorer en fonction des fontionnalités
        switch (choix)
        {
            case 0: // choix random
                initGrillerandom();// abandonée a ne pas utiliser 
                return;
            case 1:
            {
                start = false; //choix manuel
                initGrilleMan();
				
                return;
            }
            
            default:
                break;
        }
    }

    
    /**
     * Fonction qui permet de retourner un tableau des chiffres hazard servi pour les initialisation random
    **/
    int[] random(int seed, int min, int max)
    {
        Random r = new System.Random();
        int[] array = new int[NbCasesParAxe];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = r.Next(min, max);
            if (i > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[i] == array[j])
                    {
                        i--;
                    }
                }
            }
        }
        return array;
    }
    
    /**
     * Fonction qui générer aléatoirement la grille
     * 
     **/
    void initGrillerandom()
    {
        int[] array = new int[NbCasesParAxe];
        for (int i = 0; i < NbCasesParAxe; i++)
        {
            for (int j = 0; j < NbCasesParAxe; i++)
            {

                for (int k = 0; k < NbCasesParAxe; i++)
                {
                    array = random(8, 0, 99);
                    GrilleRand[i, j, k] = (array[k] % 2 == 0);
                    Debug.Log(GrilleRand[i,j,k].ToString());
                }
            }
        }
        System.Array.Copy(GrilleRand, Grille, Grille.Length);
        
    }
    
    void initGrilleMan()
    {

        initGrilleFichier();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
            
        Vector3 worldPos1 = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        //Debug.Log(worldPos1.x + "," + worldPos1.y + "," + worldPos1.z);
        int X, Y, Z;
        float x = Camera.main.transform.localEulerAngles.x;
        float y = Camera.main.transform.localEulerAngles.y;
        float disTmp = dis;
      
            
            
        disTmp += (sensitivity * Input.GetAxis("Mouse ScrollWheel"));
        X = (int)Math.Round(disTmp * Math.Sin(y * Math.PI / 180) * Math.Cos((x - Math.PI/2) * Math.PI / 180)+ worldPos1.x);
        Z = (int)Math.Round(disTmp * Math.Cos(x * Math.PI / 180) * Math.Cos(y * Math.PI / 180)+ worldPos1.z);
        Y = (int)Math.Round(disTmp * Math.Sin((-x ) * Math.PI / 180) + worldPos1.y);

 
        if (Input.GetMouseButtonDown(0)) 
        {

            Changer(X, Y, Z);
        }

        if (Input.GetKey(KeyCode.E))
        {
            start = true;
            return;
        }
        
    }
    
    void Changer(int X, int Y, int Z)
    {
        if(X<0||X>NbCasesParAxe|| Y < 0 || Y > NbCasesParAxe || Z < 0 || Z > NbCasesParAxe)
        {
            return;
        }
        X = (X < 0) ? 0 : X;
        X = (X < 0) ? 0 : X;
        X = (X < 0) ? 0 : X;
        GrilleAjour[X, Y, Z] = !Grille[X, Y, Z];
        System.Array.Copy(GrilleAjour, Grille, Grille.Length);
    }
    
    void initGrilleFichier()
    {
        GrilleSauvegarde = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        Array.Copy(Grille, GrilleSauvegarde, GrilleSauvegarde.Length);
    }

   
    

    /*Méthode pour entrer le nom  d'une simulation que 
     * l'utilisateur veut enregistrer
     */
    public void EntrerNomSimulation()
    {
        PanelMessage.SetActive(true);
        sauvegarderActuelle = true;       
    }
    public void EcraseFichierOui()
    {
        ecraseFichier = true;
        PanelMessage2.SetActive(false);
    }


    public void EcraseFichierNon()
    {
        ecraseFichier = false;
        PanelMessage2.SetActive(false);
    }
    public void Annuler()
    {
        PanelMessage.SetActive(false);
        sauvegarderActuelle = false;
        pause = false;
    }

    /*
     * Méthode pour enregistrer une simulation avec son nom donné par l'utilisateur
     */
    public void Sauvegarder()
    {
        envoyer = true;
        if (envoyer)
        {
            PanelMessage.SetActive(false);
            envoyer = false;
            string[] sauvegardeTab = new string[GrilleSauvegarde.Length];
            int i = 0;
            for (int x = 0; x < NbCasesParAxe; x++)
            {
                for (int y = 0; y < NbCasesParAxe; y++)
                {
                    for (int z = 0; z < NbCasesParAxe; z++)
                    {

                        sauvegardeTab[i] = GrilleSauvegarde[x, y, z].ToString();
                        i++;

                    }
                }
            }
            string sauvegardeString = NbCasesParAxe.ToString() + "/" + SousPop.ToString() + "/" + SurPop.ToString() + "/" + naitreMin.ToString() + "/" +naitreMax.ToString()+"/"+ voisinCases.ToString() + "/" + toroidale.ToString() + "/" + codeCouleur.ToString() + "/" + moore.ToString() + "/";

            sauvegardeString += string.Join("/", sauvegardeTab);
            string name = Application.dataPath + "/sauvegardes/"+ BtnNom.text + ".txt";
            if (File.Exists(name))
            {
                PanelMessage2.SetActive(true);

                if (ecraseFichier == true)
                {

                    File.WriteAllText(name, sauvegardeString);
                }
                if (ecraseFichier == false)
                {
                    int z = 2;
                    do
                    {
                        name = Application.dataPath+"/sauvegardes/" + BtnNom.text + "(" + z + ")" + ".txt";
                        z++;
                    } while (File.Exists(name));

                    File.WriteAllText(name, sauvegardeString);
                }
            }

            if (!(File.Exists(name)))
            {
                File.WriteAllText(name, sauvegardeString);
            }
        }
        sauvegarderActuelle = false;
    }

    public void InitGrilleReplay()
    {
                GrilleAjour = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
                Array.Copy(GrilleSauvegarde, Grille, Grille.Length);
                RegenererGrille();
    }

    public void InitGrilleReset()
    {
        Grille = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        GrilleAjour = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
        RegenererGrille();
    }
    
    /**
     * Fonction pour generer la nouvelle grille3D 
     **/

    void RegenererGrille()
    {
        int compteur = 0;


       // Debug.Log("RegenererGrille");
        if (Grille == null)
        {
            Grille = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
            GrilleAjour = new bool[NbCasesParAxe, NbCasesParAxe, NbCasesParAxe];
      
            for (int x = 0; x < NbCasesParAxe; x++)
            {

                for (int y = 0; y < NbCasesParAxe; y++)
                {

                    for (int z = 0; z < NbCasesParAxe; z++)
                    {
                        Grille[x, y, z] = false;
                        
                    }
                }
            }
            affiche();
            initGrille(choix);
            initGrilleFichier();
            return ;
        }

        if (choix == 0)
        {
            Debug.Log("init");
            initGrillerandom();
        }

        int sur_pop = SurPop;
        int sous_pop = SousPop;
        int naitre_min = naitreMin;
        int naitre_max = naitreMax;
        int voisin_cases = voisinCases;

        if (sur_pop <0)
        {
            sur_pop = 0;
        }  
        if (sous_pop <0)
        {
            sous_pop = 0;
        }
        if (naitre_max <0)
        {
            naitre_max = 0;
        }
        if (naitre_min <0)
        {
            naitre_min = 0;
        }
        if (voisin_cases <1)
        {
            naitre_max = 1;
        }
        
        if (sous_pop>sur_pop)
        {
            int t = sous_pop;
            sous_pop = sur_pop;
            sur_pop = t;
            
        }

        if (naitre_min>naitre_max)
        {
            int t = naitre_min;
            naitre_min= naitre_max;
            naitre_max = t;
        }
        
        // au cas ou si le seuil de la surpopulation dépasse la limite
        if (moore == false )
        {
            if ((sur_pop >(6*voisin_cases)))
            {
                sur_pop = 6*voisin_cases;
               
            }
             
        }
        else
        {
            if ((sur_pop >(26*voisin_cases)))
            {
                sur_pop = 26*voisin_cases;
            }
            
        }
       


        
        // au cas ou si le seuil de la surpopulation dépasse la limite
        if (moore == true && (sur_pop >26*voisin_cases))
        {
            sur_pop = 26*voisin_cases;
            if (sous_pop > sur_pop)
            {
                sous_pop = sur_pop / 2;
            }
        }

        //Boucle pour reinitialiser la grille
        for (int x = 0; x < NbCasesParAxe; x++)
        {
            for (int y = 0; y < NbCasesParAxe; y++)
            {
                for (int z = 0; z < NbCasesParAxe; z++)
                {
                    bool cellActuelle = Grille[x, y, z];
                    int cmpt = GetNbcellVivantVoisin(x, y, z);
                    //Cas si la cellule actuelle est vivant 
                    if (cellActuelle == true)
                    {
                        if (cmpt < sous_pop || cmpt > sur_pop)
                        {
                            GrilleAjour[x, y, z] = false;
                        }
                        else
                        {
                            compteur += 1;
                            
                        }
                    }
                    else
                    {
                        // cas si on veut mettre un plage de nombre voisin pour creer une cellule
                        if (cmpt>= naitre_min && cmpt<= naitre_max)
                        {
                            GrilleAjour[x, y, z] = true;
                            compteur += 1;
                            
                        }
                    }
                }
            }
        }
        int totalCube = NbCasesParAxe * NbCasesParAxe * NbCasesParAxe;
        densite = compteur * 100 / totalCube;
       // generation += 1;
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
        if (moore == true)
        {
            /**
             * Cas si utilsateur veut le voisinage Moore mais pas le mode Toroïdale
             **/
            if (toroidale == false)
            {  
            
                for (int i = -v; i <= v; i++){
                    if (i + x >= 0 && i + x < NbCasesParAxe)
                    {
                        for (int j = -v; j <= v; j++)
                        {
                            if (j + y >= 0 && j + y < NbCasesParAxe)
                            {
                                for (int k = -v; k <= v; k++)
                                {
                                    if (k + z >= 0 && k + z < NbCasesParAxe)
                                    {
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
            }
            /**
             * Cas si utilsateur veut le voisinage Moore et aussi le mode Toroïdale
             **/
            else
            {
                for (int i = -v; i <= v; i++)
                {
                    for (int j = -v; j <= v; j++)
                    {
                        for (int k = -v; k <= v; k++)
                        {
                            int voisin_x, voisin_y, voisin_z;
                            voisin_x = (i + x + NbCasesParAxe) % NbCasesParAxe;
                            voisin_y = (j + y + NbCasesParAxe) % NbCasesParAxe;
                            voisin_z = (k + z + NbCasesParAxe) % NbCasesParAxe;
                            if (Grille[voisin_x, voisin_y, voisin_z] == true)
                            {
                                cmpt++;
                            }
                        }
                    }
                }
            }
        }
        else
        { 
            /**
             * Cas si utilsateur ne veut ni le voisinage Moore ni le mode Toroïdale
             **/
            if (toroidale == false)
            {
                for (int i = -v; i < v; i++)
                {
                    if (i + x >= 0 && i + x < NbCasesParAxe)
                    {
                        if (Grille[x + i, y, z] == true)
                        {
                            cmpt++;
                        }
                    }
                }
                for (int i = -v; i < v; i++)
                {
                    if (i + y >= 0 && i + y < NbCasesParAxe)
                    {
                        if (Grille[x, y + i, z] == true)
                        {
                            cmpt++;
                        }
                    }
                }
                for (int i = -v; i < v; i++)
                {
                    if (i + z >= 0 && i + z < NbCasesParAxe)
                    {
                        if (Grille[x, y, z + i] == true)
                        {
                            cmpt++;
                        }
                    }
                }
            }
            /**
             * Cas si utilsateur ne veut pas le voisinage Moore mais avec le mode Toroïdale
             **/
            else
            { 
                for (int i = -v; i < v; i++)
                {
                    int voisin_x;
                    voisin_x = (i + x + NbCasesParAxe) % NbCasesParAxe;
                    if (Grille[voisin_x, y, z] == true)
                    {
                        cmpt++;
                    }

                }
                
                for (int i = -v; i < v; i++)
                {
                    int voisin_y;
                    voisin_y = (i + x + NbCasesParAxe) % NbCasesParAxe;
                    if (Grille[x, voisin_y, z] == true)
                    {
                        cmpt++;
                    }

                }
                
                for (int i = -v; i < v; i++)
                {
                    int voisin_z;
                    voisin_z = (i + x + NbCasesParAxe) % NbCasesParAxe;
                    if (Grille[x, y, voisin_z] == true)
                    {
                        cmpt++;
                    }
                }
            }
        }
        return cmpt;
    }
    
    /**
     * Fonction qui affiche la grille avec les cellules 
     */

    public void affiche()
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
                        if (SphereCube)
                        {
                            gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);  
                        }
                        else
                        {
                            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        }
                        
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
                            if (codeCouleur == true)
                            {
                                couleur = setCouleur(x, y, z);
                            }
                            else
                            {
                                couleur = (1, 0, 0, 1);
                            }
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
    (float, float, float, float) setCouleur(int x, int y, int z)
    {
        float NbMax = SurPop;
        float NbMin = SousPop;
        float moy = (NbMax + NbMin) / 2;
        float cmpt = GetNbcellVivantVoisin(x, y, z);

        if (cmpt >= NbMax) // voisin = seuil de la sur population
        {
            return (0f, 0f, 0.9f, 1f);
        }

        if (cmpt == moy) // La moyenne
        {
            return (0f, 0.9f, 0f, 1f);
        }

        if (cmpt <= NbMin) // voisin = seuil de la sous population
        {
            return (0.9f, 0f, 0f, 1f);
        }
        else
        {
            if (cmpt > moy)
            {
                float tempV = (cmpt - NbMin) / (moy - NbMin); //valeur Vert entre ]0,1]
                float tempB = (moy - cmpt) / (moy - NbMin); //valeur Bleu entre ]0,1]
                return (0f, tempV, tempB, 1f);
            }
            else
            {
                float tempV = (NbMax - cmpt) / (NbMax - moy); // valeur Vert entre ]0,1]
                float tempR = (cmpt - moy) / (NbMax - moy); // valeur Rouge entre ]0,1]
                return (tempR, tempV, 0f, 1f);
            }
        }
    }
}