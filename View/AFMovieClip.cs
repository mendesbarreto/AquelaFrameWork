using System;
using System.Collections.Generic;


using UnityEngine;
using AFFrameWork.Core;
using AFFrameWork.Sound;
using Signals;

namespace AFFrameWork.View
{
    public class AFMovieClip : AFObject , IAnimatable
    {
        protected bool m_loop;
        protected bool m_playing;

        protected int m_currentFrame;

        protected float m_defaultFrameDuration;
        protected double m_currentTime;

        protected List<Sprite> m_sprites;
        protected List<AFSound> m_sounds;
        protected List<float> m_durations;
        protected List<float> m_startTimes;

        public Signal<bool> OnComplete = new Signal<bool>();

        protected SpriteRenderer m_spriteRender; 


        public AFMovieClip( List<Texture2D> textures , float fps = 12 )
        {
            m_spriteRender = this.gameObject.AddComponent<SpriteRenderer>();

            if (textures.Count == 0)
            {
                throw new Exception("Empty texture array");
            }
            else if ( fps <= 0)
            {
                throw new Exception("Invalid fps: " + fps);
            }
            else
            {
                Init(textures, fps);
            }
        }

        private void Init( List<Texture2D> textures , float fps )
        {
            int numFrames = textures.Count;

            m_defaultFrameDuration = 1.0f / fps;
            m_loop = true;
            m_playing = true;
            m_currentTime = 0.0f;
            m_currentFrame = 0;
            m_sprites = new List<Sprite>(numFrames);
            m_sounds = new List<AFSound>(numFrames);
            m_startTimes = new List<float>(numFrames);
            m_durations = new List<float>(numFrames);

            Texture2D tx;
            for (int i = 0; i < numFrames; ++i)
            {
                tx = textures[i];
                m_sprites[i] = Sprite.Create(tx , new Rect( 0,0,tx.width, tx.height ), Vector2.zero );
                m_durations[i] = m_defaultFrameDuration;
                m_startTimes[i] = i * m_defaultFrameDuration;
            }

            m_spriteRender.sprite = m_sprites[0];
        }

        public void AddFrame(Sprite sprite , AFSound sound, float duration=-1.0f)
        {
            AddFrameAt(GetTotalFrames(), sprite, sound, duration);
        }

        public void AddFrameAt(int frameID, Sprite sprite, AFSound sound = null, float duration = -1.0f)
        {
            if (frameID < 0 || frameID > GetTotalFrames()) throw new ArgumentException("Invalid frame id");
            if (duration < 0) duration = m_defaultFrameDuration;

            m_sprites.Insert(frameID, sprite);
            m_sounds.Insert(frameID, sound);
            m_durations.Insert(frameID, duration);


            if (frameID > 0 && frameID == GetTotalFrames())
            {
                m_startTimes[frameID] = m_startTimes[(int)(frameID - 1)] + m_durations[(int)(frameID - 1)];
            }
            else
            {
                UpdateStartTimes();
            }
        }

        public void AddFrame(Texture2D texture, AFSound sound, float duration = -1.0f)
        {
            AddFrameAt(
                GetTotalFrames(),
                texture,
                sound,
                duration);
        }

        public void AddFrameAt(int frameID, Texture2D texture, AFSound sound = null, float duration = -1.0f)
        {
            AddFrameAt(
                frameID,
                Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero),
                sound,
                duration);
        }

        public void Play()
        {
            m_playing = true;
        }

        public void Stop()
        {
            m_playing = false;
        }

        public void Pause()
        {
            m_playing = false;
            m_currentFrame = 0;
        }

        public void AdvanceTime(double time)
        {
            if (!m_playing || time <= 0.0f) return;


            int finalFrame;
            int previousFrame = m_currentFrame;
            float frameDuration;
            double restTime = 0.0f;

            bool breakAfterFrame = false;
            bool dispatchCompleteEvent = false;
            double totalTime = GetTotalTime();


            if(m_loop && m_currentFrame >= totalTime )
            {
                m_currentTime = 0.0f;
                m_currentFrame = 0;
            }


            if( m_currentTime < totalTime )
            {
                m_currentTime += time;
                finalFrame = m_sprites.Count - 1;
                
                frameDuration = m_startTimes[m_currentFrame] + m_durations[m_currentFrame];

                while (m_currentTime > frameDuration)
                {
                    if (m_currentFrame == finalFrame)
                    {
                        if (m_loop && OnComplete.NumListeners == 0)
                        {
                            m_currentTime -= totalTime;
                            m_currentFrame = 0;
                        }
                        else
                        {
                            breakAfterFrame = true;
                            restTime = m_currentTime - totalTime;
                            dispatchCompleteEvent = true;
                            m_currentFrame = finalFrame;
                            m_currentTime = totalTime;

                        }
                    }
                    else
                    {
                        m_currentFrame++;
                    }

                    AFSound sound = m_sounds[m_currentFrame];
                    if (sound) sound.Play();
                    if (breakAfterFrame) break;
                }

                if (m_currentFrame == finalFrame && m_currentTime == totalTime)
                   dispatchCompleteEvent = true;

            }


            if (m_currentFrame != previousFrame)
                m_spriteRender.sprite = m_sprites[m_currentFrame];

            if(dispatchCompleteEvent)
                OnComplete.Dispatch(true);

            if(m_loop && restTime > 0.0f )
            {
                AdvanceTime(restTime);
            }
        }

        private void UpdateStartTimes()
        {
            int numFrames = GetTotalFrames();

            m_startTimes[0] = 0;

            for (int i = 1; i < numFrames; ++i)
                m_startTimes[i] = m_startTimes[i - 1] + m_durations[i - 1];
        }


        public Sprite GetSprite()
        {
            return m_sprites[m_currentFrame];
        }

        public Texture2D GetTexture()
        {
            return m_sprites[m_currentFrame].texture;
        }

        public float GetTotalTime()
        {
            int numFrames = GetTotalFrames() - 1;
            return m_startTimes[numFrames] + m_durations[numFrames];
        }

        public int GetTotalFrames()
        {
            return m_sprites.Count;
        }

        public Sprite GetFrameTexture(int frameID)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");

            return m_sprites[frameID];
        }

        public void SetFrameTexture(int frameID, Sprite texture)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");
            m_sprites[frameID] = texture;
        }

        public AFSound GetFrameSound(int frameID)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");

            return m_sounds[frameID];
        }

        public void SetFrameSound(int frameID, AFSound sound)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");
            m_sounds[frameID] = sound;
        }
       
        public float GetFrameDuration(int frameID)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");

            return m_durations[frameID];
        }

        public void SetFrameDuration(int frameID, float duration)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");
            m_durations[frameID] = duration;
        }


        public bool GetIsLoop()
        {
            return m_loop;
        }

        public void SetIsLoop(bool value)
        {
            m_loop = value;
        }

        public Sprite GetCurrentFrameSprite()
        {
            return m_spriteRender.sprite;
        }

        public int GetCurrentFrameNumber()
        {
            return m_currentFrame;
        }

        public bool isPlaying()
        {
            if (m_playing)
                return m_loop || m_currentTime < GetTotalTime();

            return false;
        }

        public bool IsComplete()
        {
            return !m_loop && m_currentTime >= GetTotalTime();
        }

    }
}
