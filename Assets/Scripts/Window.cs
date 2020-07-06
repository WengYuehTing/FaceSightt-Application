﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface WindowBase {
    Vector3 position { get; set; }
    Vector3 eulerAngles { get; set; }
    Vector3 scale { get; set; }
    bool visibility { get; set; }
    float distance {get; set;}
    void Open();
    void Close();
}

public class Window : MonoBehaviour, WindowBase
{
    
    public Vector3 position { 
        get {
            return transform.position;
        }
        set {
            transform.position = value;
        }
    }

    public Vector3 eulerAngles { 
        get {
            return transform.eulerAngles;
        }
        set {
            transform.eulerAngles = value;
        }
    }

    public Vector3 scale { 
        get {
            return transform.localScale;
        }
        set {
            transform.localScale = value;
        }
    }

    
    public bool visibility { 
        get {
            return gameObject.activeSelf;
        }
        set {
            gameObject.SetActive(value);
        }
    }
    
    public float distance { get; set; }
    public Vector3 eulerAngleOffsets { get; set; }
    public AnimationCurve curve;
    protected float speedMultifier;
    protected float targetScale;
    // Start is called before the first frame update

    protected virtual void Awake() {
        position = Vector3.zero;
        eulerAngles = Vector3.zero;
        eulerAngleOffsets = Vector3.zero;
        scale = new Vector3(1.0f, 1.0f, 1.0f);
        distance = 20.0f;
        visibility = false;
        speedMultifier = 2.0f;
        targetScale = 1.0f;
    }
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update() {}

    public virtual void Open() {
        print("Open");
        StartCoroutine(OpenAnimation()); 
        position = Camera.main.transform.GetComponent<HeadSetTracking>().GetSlotPosition();
        eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0) + eulerAngleOffsets;
        
    }

    public void Close() {
        StartCoroutine(CloseAnimation()); 
    }

    IEnumerator OpenAnimation() {
        float curveTime = 0.0f;
        float curveAmount = curve.Evaluate(curveTime);
        visibility = true;
        scale = new Vector3(1,1,1);
        
        while(curveAmount<1.0f) {
            
            curveTime += Time.deltaTime * speedMultifier;
            curveAmount = curve.Evaluate(curveTime);
            scale = new Vector3(targetScale*curveAmount, targetScale*curveAmount, 1);
            yield return null;
        }

    }

    IEnumerator CloseAnimation() {
        float curveTime = 1.0f;
        float curveAmount = curve.Evaluate(curveTime);

        while(curveAmount>0.0f) {
            curveTime -= Time.deltaTime * speedMultifier;
            curveAmount = curve.Evaluate(curveTime);
            scale = new Vector3(targetScale*curveAmount, targetScale*curveAmount, 1);
            yield return null;
            
        }
        position = Vector3.zero;
        eulerAngles = Vector3.zero;
    }
}
