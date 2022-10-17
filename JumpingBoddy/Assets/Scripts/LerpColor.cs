using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour
{
    MeshRenderer imageMeshR;
    [SerializeField][Range(0f, 1f)] float lerpTime;

    [SerializeField] Color[] myColors;
    int colorIndex = 0;
    float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        imageMeshR = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        imageMeshR.material.color = Color.Lerp(imageMeshR.material.color, myColors[colorIndex], lerpTime);
        t = Mathf.Lerp(t, 1f, lerpTime);
        if (t>0.9f)
        {
            t = 0;
            colorIndex++;
            colorIndex = (colorIndex >= myColors.Length) ? 0: colorIndex;
        }
    }

}
