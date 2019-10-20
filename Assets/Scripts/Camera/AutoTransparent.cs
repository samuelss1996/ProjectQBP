using UnityEngine;
using System.Collections;


public class AutoTransparent : MonoBehaviour
{
    private float m_Transparency = 0.3f;
    private const float m_TargetTransparancy = 0.3f;
    private const float m_FallOff = 0.1f; // returns to 100% in 0.1 sec


    public void BeTransparent()
    {
        // reset the transparency;
        m_Transparency = m_TargetTransparancy;
    }

    void Update()
    {
        if (m_Transparency < 1.0f)
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }

        m_Transparency += ((1.0f - m_TargetTransparancy) * Time.deltaTime) / m_FallOff;
    }
}