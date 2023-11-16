using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefabObject;
    public int objectNumberOnStart;

    private List<GameObject> poolObjects = new List<GameObject>();

    private void Start()
    {
        //Create the object at the begining of the game
        for(int i = 0; i < objectNumberOnStart; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// Instantiate new object and add it to the list
    /// </summary>
    /// <returns></returns>
    private GameObject CreateNewObject()
    {
        GameObject gameObject = Instantiate(prefabObject);
        gameObject.SetActive(false);
        poolObjects.Add(gameObject);
        return gameObject;
    }

    /// <summary>
    /// Take from the list an available object, if none is available create a new one
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject()
    {
        //Find an object that is inactive 
        GameObject gameObject = poolObjects.Find(x=> x.activeInHierarchy == false);

        //if none exists create one
        if(gameObject == null)
        {
            gameObject= CreateNewObject();
        }
        gameObject.SetActive(true);
        return gameObject;
    }
}
