using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Image image;
    public AnimationCurve curve;
    public Color init;
    public Color final;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(takeDamage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator takeDamage ()
    {
        float i = 0;
        float rate = 1 / duration;
        while (i < 1) {
            i += rate * Time.deltaTime;
            image.color = Color.Lerp(init, final, curve.Evaluate (i));
            yield return null;
        }
    }

    public void myEvent()
    {
        StopAllCoroutines();
        StartCoroutine(takeDamage());
    }
}
