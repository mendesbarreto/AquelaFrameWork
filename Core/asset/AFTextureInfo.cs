using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace AFFrameWork.Core.Asset
{
    internal class AFTextureInfo
    {
        private string m_name;
        private Rect m_region;
        private Rect m_frame;
        private Vector2 m_pivot;
        private bool m_rotated;

        public AFTextureInfo()
        {

        }

        public AFTextureInfo(string name,/* Rect region, */Rect frame, Vector2 pivot,bool rotated = false)
        {
            m_name = name;
            //m_region = region;
            m_frame = frame;
            m_rotated = rotated;
            m_pivot = pivot;
        }

        /*public void SetRegion(Rect value)
        {
            m_region = value;
        }

        public Rect GetRegion()
        {
            return m_region;
        }
*/

        public void SetPivot(Vector2 value)
        {
            m_pivot = value;
        }

        public Vector2 GetPivot()
        {
            return m_pivot;
        }
        

        public void SetFrame(Rect value)
        {
            m_frame = value;
        }

        public Rect GetFrame()
        {
            return m_frame;
        }
        
        public void SetRotated(bool value)
        {
            m_rotated = value;
        }

        public bool GetRotated()
        {
            return m_rotated;
        }

        public void SetName(string value)
        {
            m_name = value;
        }

        public string GetName()
        {
            return m_name;
        }


    }
}
