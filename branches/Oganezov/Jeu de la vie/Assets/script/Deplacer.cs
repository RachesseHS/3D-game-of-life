using UnityEngine;
using System.Collections;
 
public class Deplacer: MonoBehaviour
{
    public CharacterController controller;
    public Rigidbody rigidbody;
    public float vitesse  = 1;
 
    // initialisation 
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        controller = this.GetComponent<CharacterController>();
    }
 
    // MAJ par trame
    void Update()
    {
 
        if (Input.GetKey("q"))
            controller.SimpleMove(transform.right * -speed);
        if (Input.GetKey("d"))
            controller.SimpleMove(transform.right * speed);
        if (Input.GetKey("z"))
            controller.SimpleMove(transform.forward * speed);
        if (Input.GetKey("s"))
            controller.SimpleMove(transform.forward * -speed);
    }
}