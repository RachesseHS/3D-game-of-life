using UnityEngine;
using System.Collections;

public abstract class Cell : MonoBehaviour {
	// A modifier si ajout d'etats(optionnel)
	public enum Etats {
		Mort, Vivant
	}

	[HideInInspector] public int x, y, z;
	[HideInInspector] public Cell[] voisins;

	[HideInInspector] public Etats etat;
	private Etats etatSuivant;
		
	public void MiseAJourCell () {
		etat_suiv = etat;
		int CellVivant = GetNbCellVivant ();
		if (etat == Etats.Alive) {
			if (CellVivant <= nbSousPop && CellVivant >= nbSurPop) //  nbSousPop, nbSurPop non défini encore
				etat_suiv = States.Dead;
		} else { 
			if (CellVivant == true)  // true a remplacer par la listes des voisin qui fait naitre la cell
				etat_suiv = Etats.Vivant;
		}
	}

		
	public void Init ( int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z
	}

	public void InitEtat (Etat s) {
		etat = s;
	}

	public void changerEtat () {

	}
	
	private int GetNbCellVivant () {
		int ret = 0;
		for (int i = 0; i < voisins.Length; i++) {
			if (voisins [i].state == Etats.Vivant)
					ret++;
		}
		return ret;
	}
}