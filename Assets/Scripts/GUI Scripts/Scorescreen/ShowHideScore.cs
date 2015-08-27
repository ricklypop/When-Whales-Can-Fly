using UnityEngine;
using System.Collections;

public class ShowHideScore : MonoBehaviour {
    public bool show;
    private float a;

    void Update()
    {
        if (show && a < 1)
            a += 0.05f;
        else if(show)
            a = 1;

        if (!show && a > 0)
            a -= 0.05f;
        else if (!show)
            a = 0;

        GetComponent<CanvasGroup>().alpha = a;
    }
}
