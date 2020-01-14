using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    GameObject Manager;
    DialogManager dialogManager;
    public Text Expression;  // 自分自身の発話

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("Manager");
        dialogManager = Manager.GetComponent<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPressed()
    {
        Debug.Log("pressed " + Expression.text);
    }
}
