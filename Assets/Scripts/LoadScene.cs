using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public LayerMask playerLayer;
    private bool stageClear = false;
    public string sceneName; 
    public bool finalFlag = false;

    public string phaseName;
    public static string phaseNameAux;
    
    
    // Start is called before the first frame update
    void Start()
    {
        phaseNameAux = phaseName;
    }

    // Update is called once per frame
    void Update()
    {
        stageClear = Physics2D.CircleCast(transform.position, 3f, Vector2.up, 3f, playerLayer);

        if(stageClear)
        {
            if(finalFlag)
            {
                if(PlayerStatus.mindPoints==48)
                {
                    SceneManager.LoadScene("GoodEnding");
                }
                else
                {
                    SceneManager.LoadScene("BadEding");
                }

                
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color  = Color.green;
        Gizmos.DrawWireSphere(transform.position, 3f);
        
    }
}
