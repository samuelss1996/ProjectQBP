using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEmissionColorAnimator : MonoBehaviour
{
    public const float Pi = 3.14159f;
    private Material material;
    [SerializeField]
    private float animationSpeed = 0.5f;
    [SerializeField]
    private Color colorOne = Color.black;
    [SerializeField]
    private Color colorTwo = Color.white;
    private float timer = 0.0f;


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
            timer = timer % (Pi / animationSpeed);
            Color emissionColor = Color.Lerp(colorOne, colorTwo, Mathf.Sin(timer * animationSpeed));
            material.SetColor("_EmissionColor", emissionColor * 8.5f);
            timer += Time.deltaTime;
        }
    }
}
