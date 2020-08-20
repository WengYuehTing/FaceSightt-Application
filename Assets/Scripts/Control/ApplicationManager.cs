﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Window[] supportedApps;
    private Queue<string> actions;

    void Start()
    {
        actions = new Queue<string>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(actions.Count > 0) {
            string action = actions.Dequeue();
            Mapping(action);

        }
    }

    private Window Find(string name) {
        foreach(Window app in supportedApps) {
            if(app.PACKAGE_NAME == name) {
                return app;
            }
        }

        return null;
    }

    public void Mapping(string action) {
        switch(action) {
            
            case "left nose wing":
                if(Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow) {
                    (Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow).ShortBackward();
                }
                break;
            
            case "right nose wing":
                if(Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow) {
                    (Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow).ShortForward();
                }
                break;
            
            case "nose tip":
                if(Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow) {
                    (Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow).Play_OR_Pause();
                }
                break;
            
            case "right cheek":
                if(Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow) {
                    (Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow).Next();
                }
                break;

            case "left cheek":
                if(Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow) {
                    (Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow).Last();
                }
                break;
            
            case "middle of upper or lower lips":
                if(Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow) {
                    (Camera.main.transform.parent.GetComponent<Attention>().hoveredWindow as VideoPlayerWindow).Mute();
                }
                break;

            case "chin":
            case "h":
                Window prefab = Find("Home");
                if(prefab != null) {
                    if(GameObject.FindObjectOfType<HomeWindow>() != null) {
                        Window window = GameObject.FindObjectOfType<Window>();
                        window.Close();
                    } else {
                        Window window = GameObject.Instantiate(prefab) as Window;
                        window.Open();
                    }
                }
                break;

            case "t":
                Attention attention = Camera.main.transform.parent.GetComponent<Attention>();
                if (attention.hoveredIcon != null) {
                    attention.hoveredIcon.Activate();
                }
                else if (attention.hoveredSlider != null)
                {
                    attention.hoveredSlider.OnTapped(attention.hitPosition);
                }
                else if (attention.hoveredWindow != null) {
                    attention.hoveredWindow.OnTapped();
                } 
                    
                break;
            
        }
    }
  
    public void Push(string action) {
        actions.Enqueue(action);
    }
}