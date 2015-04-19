using UnityEngine;
using System.Collections;

public class AttackableObject : MonoBehaviour
{  
    public bool mortal;

    public int hp;
    public bool deleteOnDestroy;
    

    void Awake()
    {
        mortal = true;
        hp = 10;
        deleteOnDestroy = false;
    }


    public void receiveDamage(int damage = 0)
    {
        if (mortal)
        {
            hp -= damage;
            onTakeDamage();

            if (hp < 0)
                hp = 0;
            if (hp == 0)
            {
                onDestroy();
                if(deleteOnDestroy)
                    Destroy(gameObject);
            }
        }
        else
        {
            onTakeDamage();
        }


    }

    protected void onTakeDamage()
    { }

    protected void onDestroy()
    { }
}

