using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using AFFrameWork.Core;
using AFFrameWork.Utils;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AFFrameWork.Core.Asset
{
    public class AFTextureAtlas
    {
        private string m_name;
        private string m_path;
        private Texture2D m_texture;
        private Dictionary<string, AFTextureInfo> m_texturesInfo;

        public AFTextureAtlas( string name , Texture2D texture, TextAsset json )
        {
            m_name = name;
            m_texturesInfo = new Dictionary<string, AFTextureInfo>();
            m_texture = texture;

            ParseJsonFile(json);
        }

        public Sprite GetSprite(string name)
        {
            AFTextureInfo txInfo = m_texturesInfo[name];

            if (txInfo == null)
            {
                UnityEngine.Debug.LogWarning("THE SPRITE WAS NOT FOUND: " + name);
                return null;
            }

            return Sprite.Create(
                m_texture , txInfo.GetFrame(),  
                Vector2.zero,
                100.0f);
        }



        private void ParseJsonFile(TextAsset json)
        {
            
            AFTextureInfo info;
            TextAsset txt = Resources.Load<TextAsset>("TexturePackerJson/spritesheet");
            JObject obj = JObject.Parse(txt.text);
            JArray arr = obj.GetValue("frames").ToObject<JArray>();
            JObject textureObject;

            int i;
            int hashValue;
            int count = arr.Count;

            for (i = 0; i < count; ++i)
            {
                info = new AFTextureInfo();
                textureObject = arr[i].ToObject<JObject>();

                foreach (KeyValuePair<string, JToken> property in textureObject)
                {
                    hashValue = Utility.Hash(property.Key);

                    if ( AFTexturePackerProperties.FILE_NAME == hashValue )
                    {
                        info.SetName(property.Value.ToString());
                    }
                    else if (AFTexturePackerProperties.FRAME == hashValue)
                    {
                        JObject frame = JObject.Parse(property.Value.ToString());
                        info.SetFrame( new Rect( 
                            (float)frame.GetValue("x") , 
                            (float)frame.GetValue("y") , 
                            (float)frame.GetValue("w") , 
                            (float)frame.GetValue("h")) );
                    }
                    else if (AFTexturePackerProperties.ROTATED == hashValue)
                    {
                        info.SetRotated(property.Value.ToString() == "false" ? false : true);
                    }
                    else if (AFTexturePackerProperties.PIVOT == hashValue)
                    {
                        JObject pivot = JObject.Parse(property.Value.ToString());

                        //Debug.Log("P . I . V . O . T : " + float.Parse(pivot.GetValue("x").ToString()));

                        info.SetPivot(new Vector2(
                            float.Parse(pivot.GetValue("x").ToString()),
                            float.Parse(pivot.GetValue("y").ToString()) ) );
                    }

                    m_texturesInfo[info.GetName()] = info;

                    //Debug.Log(property.Key);
                    //Debug.Log(property.Value.ToString());
                    /**
                     * IF SOME DAY USE THE FOLLOW PROPERTIES 
                     * 
                    else if (AFTexturePackerProperties.SPRITE_SOURCE_SIZE == hashValue)
                    {

                    }
                    else if (AFTexturePackerProperties.TRIMED == hashValue)
                    {

                    }

                    else if (AFTexturePackerProperties.SOURCE_SIZE == hashValue)
                    {

                    }**/
                }

                

                /*
                info = new AFTextureInfo();
                info.SetName(consult.GetValue(AFTexturePackerProperties.FILE_NAME).ToString());
                info.SetRotated(consult.GetValue(AFTexturePackerProperties.ROTATED).ToString() == "false " ? false : true);
                
                consult.GetValue(AFTexturePackerProperties.FILE_NAME)
                info.SetFrame(new Rect( ))
                */
            }

        }

    }
}
