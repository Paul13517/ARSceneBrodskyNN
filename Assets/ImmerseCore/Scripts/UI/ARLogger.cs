﻿using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Core.UI
{
    public class ARLogger : MonoBehaviour
    {
        [SerializeField] Text m_LogText;

        public Text logText
        {
            get => m_LogText;
            set => m_LogText = value;
        }

        [SerializeField] int m_VisibleMessageCount = 40;

        public int visibleMessageCount
        {
            get => m_VisibleMessageCount;
            set => m_VisibleMessageCount = value;
        }

        int m_LastMessageCount;

        static List<string> s_Log = new List<string>();

        static StringBuilder m_StringBuilder = new StringBuilder();

        void Update()
        {
            lock (s_Log)
            {
                if (m_LastMessageCount != s_Log.Count)
                {
                    m_StringBuilder.Clear();
                    var startIndex = Mathf.Max(s_Log.Count - m_VisibleMessageCount, 0);
                    for (int i = startIndex; i < s_Log.Count; ++i)
                    {
                        m_StringBuilder.Append($"{i:000}> {s_Log[i]}\n");
                    }

                    var text = m_StringBuilder.ToString();

                    if (m_LogText)
                    {
                        m_LogText.text = text;
                    }
                    else
                    {
                        Debug.Log(text);
                    }
                }

                m_LastMessageCount = s_Log.Count;
            }
        }

        public static void Log(string message)
        {
            Debug.LogError(message);
            lock (s_Log)
            {
                if (s_Log == null)
                    s_Log = new List<string>();

                s_Log.Add(message);
            }
        }
    }
}
