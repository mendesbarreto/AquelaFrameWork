// /////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Sound Manager.
//
// /////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using AFFrameWork.Core;
using Signals;

namespace AFFrameWork.Sound
{
    public class MSSoundManager : ASingleton<MSSoundManager>
    {
        private string m_relativePath = "";
        private bool m_autoLoad = false;
        private Dictionary<string, AFSoundGroup> m_groups = new Dictionary<string, AFSoundGroup>();
        private Dictionary<string, AFSound> m_audios = new Dictionary<string, AFSound>();

        public readonly Signal<AFSound> OnAudioPlay = new Signal<AFSound>();
        public readonly Signal<AFSound> OnAudioAdd = new Signal<AFSound>();
        public readonly Signal<AFSound> OnAudioPause = new Signal<AFSound>();
        public readonly Signal<AFSound> OnAudioStop = new Signal<AFSound>();

        public int audiosInList = 0;


        public readonly Signal<bool> OnMute = new Signal<bool>();

        public AFSound Add( string name )
        {
            //TODO: GetFrom AssetManager
            UnityEngine.Debug.LogWarning("ADD SOUND FROM ASSET MANAGER >>>>> NOT IMPLEMENTED" );
            return null;
        }

        public AFSound Add(string name, AudioClip audioClip, Transform emitter = null, float volume = 1.0f , float pitch = 1.0f , bool loop = false)
        {
            AFSound audio;

            if( name != null && name.Length > 0 )
            {
                if (m_audios.ContainsKey(name))
                {
                    audio = m_audios[name];
                    UnityEngine.Debug.LogWarning("The audio was not added, because the name already in list of audios");
                }
                else
                {
                    audio = new AFSound(name, audioClip, emitter, volume, pitch, loop);
                    m_audios[name] = audio;
                }
            }
            else
            {
                throw new Exception("The name of sound was not valid");
            }

            audiosInList++;

            return audio;
        }

        public void Remove(string name)
        {
           if( m_audios.ContainsKey(name))
           {
               Remove(m_audios[name]);
           }
           else
           {
               UnityEngine.Debug.LogWarning("The name was not found in the audio list");
           }
        }

        public void Remove( AFSound audio )
        {
            audio.Destroy();
            m_audios.Remove(audio.GetName());
            audiosInList--;
        }

        public void RemoveAll(string name)
        {
            foreach( KeyValuePair<string, AFSound> ent in m_audios )
            {
                Remove(ent.Value);
            }

            audiosInList = 0;
        }
        

        public AudioSource Stop(string name)
        {
            if (!m_audios.ContainsKey(name))
                throw new Exception("Audio not exists");

            AFSound audio = m_audios[name];
            OnAudioStop.Dispatch(audio);

            return audio.Stop();
        }

        public void StopAll()
        {

        }

        public AFSound GetAudio( string name )
        {
            if( m_audios.ContainsKey(name) )
                return m_audios[name];

            return null;
        }

        public AudioSource Pause(string name)
        {
            if (!m_audios.ContainsKey(name))
                throw new Exception("Audio not exists");

            return m_audios[name].Pause();
        }

        public void PauseAll()
        {

        }

        public AudioSource Play( string name )
        {
            if (!m_audios.ContainsKey(name))
                throw new Exception("Audio not exists");

            AFSound audio = m_audios[name];
            OnAudioPlay.Dispatch(audio);

            return audio.Play();
        }

        public AudioSource Play(AudioClip clip)
        {
            return Play(clip, 1f, 1f, null, false);
        }

        public AudioSource Play(AudioClip clip, Transform emitter)
        {
            return Play(clip, 1f, 1f, emitter, false);
        }

        public AudioSource Play(AudioClip clip, float volume, Transform emitter, bool loop = false)
        {
            return Play(clip, volume, 1f, emitter, loop);
        }

        public AudioSource Play(AudioClip clip, Vector3 point)
        {
            return Play(clip, point, 1f, 1f);
        }

        public AudioSource Play(AudioClip clip, Vector3 point, float volume)
        {
            return Play(clip, point, volume, 1f);
        }

        public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch, bool loop = false)
        {
            return Play(clip, volume, pitch, null, loop, point);
        }

        public AudioSource Play(AudioClip clip, float volume, float pitch, Transform emitter, bool loop)
        {
            return Play( clip, volume, pitch, emitter, loop, Vector3.zero );
        }

        public AudioSource Play(AudioClip clip, float volume, float pitch, Transform emitter, bool loop, Vector3 point)
        {
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = point;

            if (emitter != null)
            {
                go.transform.position = emitter.position;
                go.transform.parent = emitter;
            }


            AudioSource source = go.AddComponent<AudioSource>();

            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;

            source.Play();

            Destroy(go, clip.length);

            return source;
        }

        public void SetRelativePath(string path)
        {
            m_relativePath = path;
        }

        public string GetRelativePath()
        {
            return m_relativePath;
        }

        public void SetAutoLoad(bool value)
        {

        }

        public bool GetAutoLoad()
        {
            return m_autoLoad;
        }

    }
}