using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using TCPetc;

public class DialogManager : MonoBehaviour
{

    public TCPClient myclient;

    // Start is called before the first frame update
    void Start()
    {
        try
        {

            Debug.Log("Loading Files");

            ImportedConst.PlayerProf = ImportedConst.ReadCSVFromOutOfBuild(ImportedConst.NamePath, true);
            ImportedConst.PlayerOpn = ImportedConst.ReadCSVFromOutOfBuild(ImportedConst.OpnPath, true);
            ImportedConst.MNetworkSettings = ImportedConst.ReadCSVFromOutOfBuild("PRESET/network_to_M.csv");
        }
        catch (IOException e)
        {
            Debug.Log("ファイルが見つかりません");
            SceneManager.LoadScene("ImportEtc");
        }
        catch (ArgumentNullException e)
        {
            Debug.Log("ファイルパスが不正です");
            SceneManager.LoadScene("ImportEtc");
        }

        string ipOrHost = "127.0.0.1";
        int port = 1000;

        ipOrHost = ImportedConst.MNetworkSettings[0][0];
        port = int.Parse(ImportedConst.MNetworkSettings[0][1]);

        Debug.Log("Making Con " + ipOrHost + ":" + ImportedConst.MNetworkSettings[0][1]);


        myclient = new TCPClient(ipOrHost, port);
        while (true)
        {
            try
            {
                myclient.Connect();
                break;
            }
            catch (SocketException)
            {
                Debug.Log("接続再試行");
                continue;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


}
