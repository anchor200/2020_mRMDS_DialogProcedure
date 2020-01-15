using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageHandler : MonoBehaviour
{
    public static List<string[]> CurrentChoices;  // 直前に来た選択肢の一覧を持っている

    public GameObject ChoiceField;
    Transform choiceTransform;
    public GameObject ChoicePrefab;

    public GameObject manager;
    DialogManager dialogManager;

    private static int quitCounter = 0;  // 連打しないとescしないように
    private static bool buttonflag = false;  // ボタン連打ができないように

    void Start()
    {
        dialogManager = manager.GetComponent<DialogManager>();
    }
    void Update()
    {
        if (ChoiceClass.WaitOperation == true)
        {
            this.OnReceive(ChoiceClass.InputHolder);  // うけとっためっせーじから選択肢を生成
            ChoiceClass.WaitOperation = false;
        }

    }

    public void OnQuitRequest()
    {
        quitCounter++;
        if (quitCounter > 5)
        {
            Quit();
        }
    }

    void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
                UnityEngine.Application.Quit();
        #endif
    }


    public static bool IsChoice(string str)
    {
        string[] temp = str.Split(':');
        Debug.Log(temp[0]);
        if (temp[0] == "<Choice>")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static List<string[]> ParserC(string str)
    {
        List<string[]> temp = new List<string[]>();
        string[] calcTemp = str.Split(';');

        foreach (string calcDetail in calcTemp)
        {
            temp.Add(calcDetail.Split(','));
        }

        return temp;
    }

    private void OnReceive(string message)
    {
        quitCounter = 0;

        if (IsChoice(message))
        {
            CurrentChoices = ParserC(message.Split(':')[1]);  // List<string[]>で全選択肢の情報を持っている→これを各ボタンに割り当てる
            Debug.Log(message.Split(':')[1]);
        }
        else
        {
            Debug.Log("不正な入力（選択肢ではない）");
            return;
        }

        choiceTransform = ChoiceField.transform;
        foreach (Transform n in choiceTransform)
        {
            GameObject.Destroy(n.gameObject);
        }
        choiceTransform.DetachChildren();  // 過去の子供を全員抹消

        int i = 0;
        foreach (string[] choice in CurrentChoices)
        {
            //プレハブからボタンを生成
            GameObject listChoice = Instantiate(ChoicePrefab) as GameObject;
            //Vertical Layout Group の子にする
            listChoice.transform.SetParent(choiceTransform, false);

            listChoice.transform.Find("Text").GetComponent<Text>().text = choice[2];

            int n = i;
            //引数に何番目のボタンかを渡す
            listChoice.GetComponent<Button>().onClick.AddListener(() => SearchOnClick(n));

            i++;
        }
    }

    void SearchOnClick(int index)
    {

        if (buttonflag == true)
        {
            return;
        }

        string log = "<Command>:" + ImportedConst.YourID + "," + CurrentChoices[index][0] + "," + CurrentChoices[index][2];  // ロボID、発話ID、文言
        Debug.Log("pressed by " + log);
        int i = 0;

        foreach (Transform n in choiceTransform)
        {
            if (i != index)
            {
                // GameObject.Destroy(n.gameObject);
                // n.gameObject.SetActive(false);
                n.gameObject.GetComponent<Button>().interactable = false;

            }
            else
            {
                //n.gameObject.GetComponent<Button>().isActiveAndEnabled = false;
            }
            i++;
        }

        buttonflag = true;

        Invoke("WaitBeforeDestroy", 2);

        dialogManager.myclient.Send(log);

    }

    void WaitBeforeDestroy()
    {
        foreach (Transform n in choiceTransform)
        {
            GameObject.Destroy(n.gameObject);
        }
        Debug.Log("invoked");
        // choiceTransform.DetachChildren();  // 過去の子供を全員抹消
        buttonflag = false;
    }



}
