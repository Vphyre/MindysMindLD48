using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public GameObject img;
    public string scene;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        img.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E))
       {
           img.SetActive(true);
           count++;
       }
       if(count>=2)
       {
           SceneManager.LoadScene(scene);
       }
    }
}
