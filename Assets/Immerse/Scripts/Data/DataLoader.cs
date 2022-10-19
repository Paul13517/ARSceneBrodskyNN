using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Immerse.Core.Data;
using Newtonsoft.Json;
using UnityEngine;


namespace Immerse.Brodsky.Data
{
    public static class DataLoader
    {
        public static void ChapterLoad()
       {
            BrodskyLocale locale = BrodskySettings.Locale;
            
            List<Chapter> chapters = new List<Chapter>();
            List<string> failedToLoadChapters = new List<string>();
            int loadedChapters = 0;

            List<string> chaptersNames = new List<string>
            {
                "intro",
                "prologue",
                "chapter-1",
                "chapter-1-2",
                "chapter-2",
                "chapter-2-3",
                "chapter-3-1",
                "chapter-3-2",
                "chapter-3-4-1",
                "chapter-3-4-2",
                "chapter-4",
                "chapter-4-5",
                "nobel",
                "cat",
                "case",
                "piers"
                
            };
            foreach (string chapterName in chaptersNames)
            {

                string chapterDataPath = DataLoadingStrings.GetChapterDataFilePath(locale, chapterName);
                TextAsset textAsset = Resources.Load<TextAsset>(chapterDataPath);
                ChapterData data = JsonConvert.DeserializeObject<ChapterData>(textAsset.text);
                Chapter chapter = new Chapter(data);
                
                string audioPath = DataLoadingStrings.GetChapterAudioPath(locale, chapterName);
                chapter.audio = Resources.Load<AudioClip>(audioPath);

                if (chapter.HasAR)
                {
                    string arAudioPath = DataLoadingStrings.GetChapterArAudioPath(locale, chapterName);
                    string arPath = DataLoadingStrings.GetChapterArPath(chapterName);
                    chapter.audioAR = Resources.Load<AudioClip>(arAudioPath);
                    chapter.ar = Resources.Load<GameObject>(arPath);
                }

                if (data.comments != null)
                {
                    foreach (var comment in chapter.comments.Where(comment => comment.HasImage))
                    {
                        string imagePath = DataLoadingStrings.GetChapterSpritePath(locale, chapterName, comment.ImagePath);
                       // Debug.Log(imagePath);
                        comment.Image = Resources.Load<Sprite>(imagePath);
                    }
                }

                chapters.Add(chapter);

                loadedChapters++;
                BrodskyEvents.OnDownloadProgressUpdated((float) loadedChapters / chaptersNames.Count);
            }

            BrodskyEvents.OnContentDownloaded(chapters, failedToLoadChapters.Count > 0);
       }

    }

    
}