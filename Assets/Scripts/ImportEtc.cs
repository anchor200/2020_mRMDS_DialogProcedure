using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //Input Field用に使う
using System.Windows.Forms; //OpenFileDialog用に使う


public class ImportEtc : MonoBehaviour
{

    public InputField input_field_path_1;
    public InputField input_field_path_2;
    public Dropdown IDField;

    // Use this for initialization
    void Start()
    {
        input_field_path_1.text = "C:/Users/taiwajikken/Documents/unity_projects/DialogProcedure/Assets/DiaImportData/SavedData/userProfAaaa.txt";
        input_field_path_2.text = "C:/Users/taiwajikken/Documents/unity_projects/DialogProcedure/Assets/DiaImportData/SavedData/OpnInputAaaa.txt";

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenExistFileName()
    {

        OpenFileDialog open_file_dialog = new OpenFileDialog();

        //InputFieldの初期値を代入しておく(こうするとダイアログがその場所から開く)
        open_file_dialog.FileName = input_field_path_1.text;


        //csvファイルを開くことを指定する
        open_file_dialog.Filter = "txtファイル|*.txt";

        //ファイルが実在しない場合は警告を出す(true)、警告を出さない(false)
        open_file_dialog.CheckFileExists = false;

        //ダイアログを開く
        open_file_dialog.ShowDialog();

        //取得したファイル名をInputFieldに代入する
        input_field_path_1.text = open_file_dialog.FileName;
        ImportedConst.NamePath = open_file_dialog.FileName;
        Debug.Log("NameFile :" + open_file_dialog.FileName);

    }

    public void OpenExistFileOpinion()
    {

        OpenFileDialog open_file_dialog = new OpenFileDialog();

        //InputFieldの初期値を代入しておく(こうするとダイアログがその場所から開く)
        open_file_dialog.FileName = input_field_path_2.text;


        //csvファイルを開くことを指定する
        open_file_dialog.Filter = "txtファイル|*.txt";

        //ファイルが実在しない場合は警告を出す(true)、警告を出さない(false)
        open_file_dialog.CheckFileExists = false;

        //ダイアログを開く
        open_file_dialog.ShowDialog();

        //取得したファイル名をInputFieldに代入する
        input_field_path_2.text = open_file_dialog.FileName;
        ImportedConst.OpnPath = open_file_dialog.FileName;
        Debug.Log("OpnFile :" + open_file_dialog.FileName);

    }

    public void OnButtonProceed()
    {
        SceneManager.LoadScene("DialogMaster");
        ImportedConst.YourID = IDField.captionText.text;
    }

}