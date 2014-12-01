using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Sound;
using AquelaFrameWork.View;
using AquelaFrameWork.Utils;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;

public class CharacterFactory : ASingleton<CharacterFactory>
{
   public AFStatesController buildHeroAnimation()
    {
       AFStatesController m_heroStates;

       UnityEngine.Debug.Log(AFAssetManager.GetPathTargetPlatformWithResolution() + "/" + "hero_sprites");
       AFTextureAtlas heroAtlas = AFAssetManager.Instance.Load<AFTextureAtlas>(AFAssetManager.GetPathTargetPlatformWithResolution() + "/" + "hero_sprites");
       m_heroStates = AFObject.Create<AFStatesController>("Hero Controller");

       AFMovieClip animation = AFObject.Create<AFMovieClip>("small_walk");
       animation.Init(heroAtlas.GetSprites("small_walk"));
       animation.gameObject.AddComponent<BoxCollider2D>();
       m_heroStates.Add("small_walk", animation, false);

       animation = AFObject.Create<AFMovieClip>("small_stop");
       animation.Init(heroAtlas.GetSprites("small_stop"));
       animation.gameObject.AddComponent<BoxCollider2D>();
       m_heroStates.Add("small_stop", animation, true);

       m_heroStates.gameObject.AddComponent<Rigidbody2D>();
      
       m_heroStates.transform.localScale = new UnityEngine.Vector3(3, 3, 3);

       return m_heroStates;
    }
}

