using UnityEngine;
using System.Collections;


public class ClearSight : MonoBehaviour
{
    public float DistanceToPlayer;
    void Update()
    {
        RaycastHit[] hits;
        Collider[] colliders = Physics.OverlapSphere(transform.position, DistanceToPlayer);

        foreach (Collider collider in colliders)
        {
            Renderer R = collider.GetComponent<Renderer>();

            if (R == null)
            {
                continue; // no renderer attached? go to next hit
            }

            AutoTransparent AT = R.GetComponent<AutoTransparent>();
            if (AT != null) // if no script is attached, attach one
            {
                AT.BeTransparent(); // get called every frame to reset the falloff
            }
        }
    }

}