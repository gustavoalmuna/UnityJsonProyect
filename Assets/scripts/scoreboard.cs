using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;

[Serializable]
public struct DatosJson
{
    public string StrIds;
    public string StrNames;
    public string StrRoles;
    public string StrNicknames;
}

[Serializable]
public struct ElementosUI
{
    public Text Ids;
    public Text Names;
    public Text Roles;
    public Text Nicknames;

}

public class scoreboard : MonoBehaviour
{

    public ElementosUI elem1;
    public List<DatosJson> Fdata = new List<DatosJson>();

    public Text titulo;
    public Text ids;
    public Text names;
    public Text roles;
    public Text nicks;

    public Button btn;

    public GameObject padre;
    public GameObject hijo;

    GameObject newGO;
    int posY = 0;

    JsonData MiJson;
    private string jsonString;

    // Start is called before the first frame update
    void Start()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/StreamingAssets/JsonChallenge.json");
        Debug.Log("jsonString: " + jsonString);

        MiJson = JsonMapper.ToObject(jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        jsonString = File.ReadAllText(Application.dataPath + "/StreamingAssets/JsonChallenge.json");

        MiJson = JsonMapper.ToObject(jsonString);

    }

    public void Score()
    {
        DatosJson d = new DatosJson();

        //print(File.ReadAllText("Assets/StreamingAssets/JsonChallenge.json"));


        Debug.Log("validamos JSON: " + MiJson);

        string title = MiJson["Title"].ToString();
        string ides = MiJson["ColumnHeaders"][0].ToString();
        string nms = MiJson["ColumnHeaders"][1].ToString();
        string rls = MiJson["ColumnHeaders"][2].ToString();
        string nks = MiJson["ColumnHeaders"][3].ToString();

        titulo.text = title.ToString();
        ids.text = ides.ToString();
        names.text = nms.ToString();
        roles.text = rls.ToString();
        nicks.text = nks.ToString();

        //string[] data = new string[MiJson["Data"].Count];

        for (int i = 0; i < MiJson["Data"].Count; i++)
        {

            d.StrIds = MiJson["Data"][i]["ID"].ToString();
            d.StrNames = MiJson["Data"][i]["Name"].ToString();
            d.StrRoles = MiJson["Data"][i]["Role"].ToString();
            d.StrNicknames = MiJson["Data"][i]["Nickname"].ToString();

            Fdata.Add(d);

            elem1.Ids.text = Fdata[i].StrIds.ToString();
            elem1.Names.text = Fdata[i].StrNames.ToString();
            elem1.Roles.text = Fdata[i].StrRoles.ToString();
            elem1.Nicknames.text = Fdata[i].StrNicknames.ToString();

            newGO = Instantiate(hijo);
            newGO.transform.parent = padre.transform;
            newGO.transform.position = new Vector2(padre.transform.position.x, padre.transform.position.y + posY);
            posY = posY - 60;
            newGO.SetActive(true);

            
        }

        
    }

    public void Click()
    {
        Score();
    }

    public void Erase()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
