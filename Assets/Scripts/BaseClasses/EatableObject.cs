using UnityEngine;
using System.Collections;

public class EatableObject : MonoBehaviour
{
    public string interactionContainerTag;
    public bool eatable;
    public float nutritiveValue;
    private bool eaten; // flag to destroy on update

    void Awake()
    {
        eatable = true;
        nutritiveValue = 1;
        eaten = false;
    }

    bool canBeAte()
    {
        return eatable;
    }
 /*   void onEat()
    {
        if (eatable)
        {
            eaten = true;
            return nutritiveValue;
        }
    
        return -nutritiveValue;
        
    }*/

    void OnUpdate()
    {
        if(eaten)
            Destroy(gameObject);
    }
}

