using System.Collections.Generic;
using UnityEngine;

public class ActiveOnStart : MonoBehaviour
{
    public List<GameObject> activates = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < activates.Count; i++){
            activates[i].SetActive(true);
        }
    }
}
