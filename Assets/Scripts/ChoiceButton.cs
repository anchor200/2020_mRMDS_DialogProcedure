using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    GameObject Manager;
    DialogManager dialogManager;
    public Text Expression;  // 自分自身の発話
    public Button button;

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
        Color btnColor1 = new Color(0.9f, 0.5f, 0.5f, 1.0f);
        Debug.Log("pressed " + Expression.text);
        button = gameObject.GetComponent<Button>();
        button.image.color = btnColor1;
    }
}
