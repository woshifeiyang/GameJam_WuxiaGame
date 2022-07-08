using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class BasicPropJson
{
    public int Id;

    public string KeyName;

    public float Value;

    public string Description;

    public string ResAddress;
}
    
public class JsonManager : MonoSingleton<JsonManager>
{
    public List<BasicPropJson> basicPropList;
    
    // Start is called before the first frame update
    void Start()
    {
        basicPropList = new List<BasicPropJson>();
        ParseBasicPropJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ParseBasicPropJson()
    {
        string text = GetTextForStreamingAssets("Basic Properties.json");
        JsonData jsonData = JsonMapper.ToObject(text);
        if (jsonData != null)
        {
            Debug.Log("JsonData数量"+ jsonData.Count);
            for (int i = 0; i < jsonData.Count; ++i)
            {
                BasicPropJson basicPropObj = new BasicPropJson();
                basicPropObj.Id = (int)jsonData[i]["Id"];
                basicPropObj.KeyName = jsonData[i]["KeyName"].ToString();
                basicPropObj.Value = (float)(double)jsonData[i]["Value"];
                basicPropObj.Description = jsonData[i]["Description"].ToString();
                basicPropObj.ResAddress = jsonData[i]["ResAddress"].ToString();
                basicPropList.Add(basicPropObj);
            }
        }
    }
    
    private string GetTextForStreamingAssets(string path)
    {
        string localPath;
        if ( Application.platform == RuntimePlatform.Android )
        {
            localPath = Application.streamingAssetsPath + "/"  + path;
        }
        else
        {
            localPath = "file:///" + Application.streamingAssetsPath + "/"+ path;
        }          
        WWW t_WWW = new WWW(localPath);     //格式必须是"ANSI"，不能是"UTF-8"

        if ( t_WWW.error != null )
        {
            Debug.LogError("error : " + localPath);
            return "";          //读取文件出错
        }

        while ( !t_WWW.isDone )
        {

        }
        //Debug.Log("t_WWW.text=  " + t_WWW.text);
      
        return t_WWW.text;
    }

}