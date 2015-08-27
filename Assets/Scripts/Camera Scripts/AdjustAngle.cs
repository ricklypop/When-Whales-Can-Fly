using UnityEngine;
using System.Collections;

public class AdjustAngle : MonoBehaviour {
    private float currentAdjustment;

    public float adjustBy;
    public bool adjust;

    void Update()
    {
        if (adjust)
        {
            transform.position += transform.up * Time.deltaTime;
            currentAdjustment += transform.up.y * Time.deltaTime;
        }

        if(currentAdjustment >= adjustBy)
        {
            adjust = false;
            currentAdjustment = 0;
        }
    }
}
