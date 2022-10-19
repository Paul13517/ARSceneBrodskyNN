using System.Collections.Generic;
using System.Linq;

namespace Immerse.Brodsky
{
    public static class BrodskyShow
    {
        public static bool IsStarted { get; private set; }
        public static List<Chapter> Chapters { get; private set; }

        private static Chapter _currentChapter;
        private static int _currentIndex = -1;

        public static int CurrentIndex => _currentIndex;
        public static float CurrentTime => _currentChapter.GetCurrentTime();
        

        public static void Init(List<Chapter> chapters)
        {
            Chapters = chapters.OrderBy(x => x.SortingOrder).ToList();

            BrodskyEvents.ShowStarted += ()=>
            {
                IsStarted = true;
                GetNext();
            };
            BrodskyEvents.ChapterFinished += OnChapterFinished;
        }

        public static int GetChapterIndex(Chapter chapter) => Chapters.FindIndex(x => x == chapter);


        public static void SetProgress(int chapterIndex, float time)
        {
            Chapters[chapterIndex].StartTime = time;
            OnChapterFinished(chapterIndex);
        }

        public static void OnChapterFinished(int nextChapterIndex)
        {
            _currentChapter.Exit();

            if (nextChapterIndex == -1)
            {
                GetNext();
            }
            else
            {
                SelectChapter(nextChapterIndex);
            }
        }

        private static void GetNext()
        {
            _currentIndex++;
            SelectChapter();
        }

        private static void SelectChapter(int index)
        {
            _currentIndex = index;
            SelectChapter();
        }

        private static void SelectChapter()
        {
            if (_currentIndex > Chapters.Count - 1)
            {
                BrodskyEvents.OnShowFinished();
                return;
            }

            _currentChapter = Chapters[_currentIndex];
            BrodskyEvents.OnChapterChanged(_currentChapter);
        }
    }
}