using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetAnimator : MonoBehaviour
{
    private Material material;
    [SerializeField]
    private float offsetChangePerSecond = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (material != null)
        {
            //material.SetTextureOffset("_EmissionMap", material.GetTextureOffset("_EmissionMap") + new Vector2(0, offsetChangePerSecond * Time.deltaTime));
            Debug.Log("It is " + material.GetTextureOffset("_EmissionMap"));
        }
    }
}
