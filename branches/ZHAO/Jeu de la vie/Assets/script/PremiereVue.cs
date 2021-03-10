using UnityEngine;
using System.Collections;
 
public class FirstView : MonoBehaviour {
 
	//La sensibilité directionnelle
	public float sensitivityX = 10f; 
	public float sensitivityY = 10f; 
	
	//Vue maximale de sur axe Y
	public float minimumY = -180f;
	public float maximumY = 180f;
	
	float rotationY = 0F;
	
	void Update ()
	{
		//Obtenir l'angle de rotation de la caméra sur axe horizontal en fonction du mouvement de la souris 
		float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		
		//Obtenir l'angle de rotation de la caméra sur axe vertical en fonction du mouvement de la souris 
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		//Limite d'angle. Si la rotationY est inférieure à min, renvoyer min. Si elle est supérieure à max, renvoyer max. sinon renvoyer la valeur 
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		
		// Réglage de l'angle de la caméra
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0); 
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
}