using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [Header("Dialog In Game")]
    public LayerMask playerLayer;
    protected bool dialogActive;
    public float dialogDistance = 2f;
    protected Text dialogText;
    protected string dialogString;
    protected Text auxText;
    protected GameObject painelObj;
    protected bool destroyFlag = true;

    void Awake()
    {
       painelObj = GameObject.FindWithTag("Painel");
       painelObj.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DialogController();
    }

    protected void DialogController()
    {
        dialogActive = Physics2D.CircleCast(transform.position, dialogDistance, Vector2.up, dialogDistance, playerLayer);

        if(dialogActive)
        {
            painelObj.SetActive(true);
            dialogText.text = dialogString;
            if(destroyFlag)
            {
                Time.timeScale = 0;
                auxText.text = "Press E to continue...";
                if(Input.GetKeyDown(KeyCode.E))
                {
                    auxText.text = "";
                    dialogText.text = "";
                    painelObj.SetActive(false);
                    Time.timeScale = 1;
                    Destroy(transform.gameObject);                       
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color  = Color.red;
        Gizmos.DrawWireSphere(transform.position, dialogDistance);
        
    }
}
