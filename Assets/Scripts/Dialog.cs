using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : UIBehaviour
{
    public Text DialogText;
    public string DialogString;
    public Text AuxText;
    public GameObject PainelObj;

    void Awake()
    {
        dialogText = DialogText;
        dialogString = DialogString;
        auxText = AuxText;
        painelObj = PainelObj; 
        painelObj.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
