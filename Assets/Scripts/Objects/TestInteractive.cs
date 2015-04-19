using UnityEngine;
using System.Collections;

public class TestInteractive : InteractiveObject {

    protected override void onEnterCollide()
    {
        Debug.Log("collision");
    }
    protected override void onCollide()
    {
        Debug.Log("InsideObject");
    }

    protected override void onExitCollide()
    {
        Debug.Log("not collide");
    }

    protected override void onUse()
    {
        Debug.Log("use");
    }

    protected override void onLongUse()
    {
        Debug.Log("long use");
    }
}
