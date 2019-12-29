using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    /*
    public static Action callParticles;
    public Action onDestroy;
    
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private AnimationCurve colorCurve;
    [SerializeField] private Material material1;
    [SerializeField] private Material material2;

    private Joint _joint;
    private Vector3 _initialScale;
    private int _clicks;
    private Renderer _renderer;
    
    private void Awake()
    {
        _initialScale = transform.localScale;
        //_joint = GetComponent<FixedJoint>();
        _renderer = GetComponent<Renderer>();
    }

    public void linkToTrunkBelow(GameObject trunk)
    {
        //_joint.connectedBody = trunk.GetComponent<Rigidbody>();
    }
    
    private IEnumerator takeDamage (AnimationCurve scaleCurve, AnimationCurve colorCurve)
    {
        callParticles?.Invoke();
        float i = 0;
        float rate = 1 / 0.2f;
        while (i < 1) {
            i += rate * Time.deltaTime;
            transform.localScale = Vector3.Lerp (_initialScale, finalScale, scaleCurve.Evaluate (i));
            _renderer.material.Lerp(material1, material2, colorCurve.Evaluate (i));
            _renderer.material = material2;
            //yield return new WaitUntil(() => _clicks > oldNumberOfClicks);
            yield return null;
        }
        onDestroy?.Invoke();
        Destroy(gameObject);
    }
    private void OnMouseDown()
    {
        StartCoroutine(takeDamage(scaleCurve, colorCurve ));
    }
    /**/

    [SerializeField] private Material flashMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private AnimationCurve _colorCurve;
    
    private Renderer _renderer;

    public void explode()
    {
        //flashMaterial.   
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        StartCoroutine(takeDamage(_colorCurve));
    }

    private IEnumerator takeDamage (AnimationCurve colorCurve)
    {
        _renderer.material.EnableKeyword("_EMISSION");
        float i = 0;
        float rate = 1 / 2f;
        while (i < 1) {
            i += rate * Time.deltaTime;
            //transform.localScale = Vector3.Lerp (_initialScale, finalScale, scaleCurve.Evaluate (i));

            _renderer.material.color = Color.Lerp(Color.white, Color.clear, colorCurve.Evaluate(i));
            
            //_renderer.material.Lerp(defaultMaterial, flashMaterial, colorCurve.Evaluate (i));
            //_renderer.material.color = Color.Lerp()
            //yield return new WaitUntil(() => _clicks > oldNumberOfClicks);
            yield return null;
        }
        _renderer.material.DisableKeyword("_EMISSION");
        //onDestroy?.Invoke();
        //Destroy(gameObject);
        //_renderer.material = defaultMaterial;
    }

}
