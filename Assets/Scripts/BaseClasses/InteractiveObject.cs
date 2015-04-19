using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour
{

    public string interactionContainerTag;
    protected bool used;
    private bool longUsed;

    void Awake()
    {
        used = false;
        interactionContainerTag = "ObjectInteractor";
        longUsed = false;
    }

	void OnTriggerEnter2D(Collider2D otherObject) 
	{
	    if (!Global.isPaused())
	    {
	        if (otherObject.tag == interactionContainerTag)
	        {
	            onEnterCollide();
	        }
	    }
	}

	void OnTriggerExit2D(Collider2D otherObject ) 
	{
	    if (!Global.isPaused())
	    {
	        if (otherObject.tag == interactionContainerTag)
	        {
	            onExitCollide();
	        }
	    }
	}

	void  OnTriggerStay2D(Collider2D otherObject)
	{
	    if (!Global.isPaused())
	    {           
            if (otherObject.tag == interactionContainerTag)
            {              
	            onCollide();

                if (Input.GetButtonUp("Use") && longUsed == false)
                    onUse();
                else if (Input.GetAxis("Use") == 1)
                {
                    longUsed = true;
                    onLongUse();
                }
                else
                {
                    longUsed = false;
                }
            }
	    }
	}



    protected virtual  void onEnterCollide()
    {      
    }

    protected virtual void onCollide()
    {}

    protected virtual void onUse()
    {}

    protected virtual void onLongUse()
    { }

    protected virtual void onExitCollide()
    {}

}
