using UnityEngine;
using System.Collections;




public class Global : MonoBehaviour
{
    private static bool Pause = false;
    // for menu pause on and off
    public static void pause(bool setPause = true)
    {
        Pause = setPause;
    }

    public static bool isPaused()
    {
        return Pause;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause = !Pause;
        }
    }
}
