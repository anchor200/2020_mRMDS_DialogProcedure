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

            // Debug.Log(ImportedConst.PlayerOpn[0][1]);

        }
        catch (IOException)
        {
            Debug.Log("ファイルが見つかりません");
            SceneManager.LoadScene("ImportEtc");
        }
        catch (ArgumentNullException)
        {
            Debug.Log("ファイルパスが不正です");
            SceneManager.LoadScene("ImportEtc");
        }

        string ipOrHost = "127.0.0.1";
        int port = 1000;

        try
        {
            ipOrHost = ImportedConst.MNetworkSettings[0][0];
            port = int.Parse(ImportedConst.MNetworkSettings[0][1]);

            Debug.Log("Making Con " + ipOrHost + ":" + ImportedConst.MNetworkSettings[0][1]);
            myclient = new TCPClient(ipOrHost, port);

            try
            {
                myclient.Connect();

                myclient.Send("<ID>:" + ImportedConst.PlayerProf[0][0]);
            }
            catch (SocketException)
            {
                Debug.Log("接続再試行");
                // this.Start();
                SceneManager.LoadScene("ImportEtc");
            }
        }
        catch (NullReferenceException)
        {

        }


    }

    // Update is called once per frame
    void Update()
    {

    }


}
