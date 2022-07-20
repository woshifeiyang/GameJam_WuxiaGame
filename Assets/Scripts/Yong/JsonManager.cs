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

public class SkillListJson
{
    public int Id;

    public string KeyName;

    public string Category;

    public string Description;

    public string Buff;

    public string ResAddress;
}

public class JsonManager : MonoSingleton<JsonManager>
{
    public List<BasicPropJson> basicPropList;

    public List<SkillListJson> skillList;

    public void InitJsonManager()
    {
        basicPropList = new List<BasicPropJson>();
        skillList = new List<SkillListJson>();
        ParseBasicPropJson();
        ParseSkillListJson();
    }
    public void ParseBasicPropJson()
    {
        string text = GetTextForStreamingAssets("Basic Properties.json");
        JsonData jsonData = JsonMapper.ToObject(text);
        if (jsonData != null)
        {
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

    public void ParseSkillListJson()
    {
        string text = GetTextForStreamingAssets("SkillList.json");
        JsonData jsonData = JsonMapper.ToObject(text);
        if (jsonData != null)
        {
            //Debug.Log("JsonData SkillList数量"+ jsonData.Count);
            for (int i = 0; i < jsonData.Count; ++i)
            {
                SkillListJson skillListJson = new SkillListJson();
                skillListJson.Id = (int)jsonData[i]["Id"];
                skillListJson.KeyName = jsonData[i]["KeyName"].ToString();
                skillListJson.Category = jsonData[i]["Category"].ToString();
                skillListJson.Description = jsonData[i]["Description"].ToString();
                skillListJson.Buff = jsonData[i]["Buff"].ToString();
                skillListJson.ResAddress = jsonData[i]["ResAddress"].ToString();
                skillList.Add(skillListJson);
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
