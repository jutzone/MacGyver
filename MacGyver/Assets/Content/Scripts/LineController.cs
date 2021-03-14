using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer line;
    private float lineAlpha;

    private void Start()
    {
        transform.SetParent(null);
        line = GetComponent<LineRenderer>();
        lineAlpha = line.startColor.a;

        Destroy(gameObject, 1.5f);
    }

    private void Update()
    {
        lineAlpha = Mathf.Lerp(lineAlpha, 0, Time.deltaTime * 3);
        line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, lineAlpha);
    }
}
