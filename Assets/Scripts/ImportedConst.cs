using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO; //System.IO.FileInfo, System.IO.StreamReader, System.IO.StreamWriter
using System; //Exception
using System.Text;


public static class ImportedConst
{
    public static string NamePath;
    public static string OpnPath;

    public static List<string[]> PlayerProf;  //[0][0]がID、[0][1]が名前とかだった気がする
    public static List<string[]> PlayerOpn;

    // 母艦とのアドレス　上のこみゅーのは使わない
    public static List<string[]> MNetworkSettings;


    /*public static string YourName;
    public static string RoboName;
    public static string ID;

    public static List<string[]> PlayerInputs = new List<string[]>() {
    new string[]{ "<Example>", "" },
    new string[]{ "<Detail>", "" },
    new string[]{ "<Refute>", "" },
    new string[]{ "<Rerefu>", "" },
    new string[]{ "<Perspec>", "" }
    };*/


    public static List<string[]> ReadCSVFromOutOfBuild(string path, bool absolute = false)
    {
        List<string[]> tempList = new List<string[]>();
        FileInfo fi = new FileInfo(Application.dataPath + "/DiaImportData/" + path);
        if (absolute == true)  // 絶対パスだったとき
        {
            fi = new FileInfo(path);
        }

        try
        {
            StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8);

            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                tempList.Add(line.Split(','));
                // Debug.Log(line);
            }
        }
        catch (Exception)
        {
            string[] err = { "われ思う、故に我在り。" };
            tempList.Add(err);
        }

        return tempList;
    }



}
