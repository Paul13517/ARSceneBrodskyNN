using System;
using System.IO;

namespace Immerse.Brodsky.Data
{
    public static class DataLoadingStrings
    {
        private static string _chaptersFolderPath = "ChaptersData";
        
        public static string ArFolderPath = String.Concat(_chaptersFolderPath,@"\ar");
        public static string ChaptersFilePath = String.Concat(_chaptersFolderPath,@"\chapters");
        
        private static string _audioFileName = "audio";
        private static string _arAudioFileName = "audio_ar";
        private static string _imagesFolderName = "images";

        public static string GetChaptersFolderPath(BrodskyLocale locale) => locale switch
        {
            BrodskyLocale.Russian => String.Concat(_chaptersFolderPath, @"\ru"),
            BrodskyLocale.English => String.Concat(_chaptersFolderPath, @"\en"),
            _ => throw new ArgumentException("Can't construct chapters folder path: given locale is invalid.")
        };

        public static string GetChapterDataFilePath(BrodskyLocale locale, string chapter) =>
            String.Concat(GetChaptersFolderPath(locale), @"\", chapter, @"\",chapter);

        public static string GetChapterArPath(string chapter) =>
            String.Concat(ArFolderPath, @"\", $"{chapter}");

        public static string GetChapterAudioPath(BrodskyLocale locale, string chapter) =>
            String.Concat(GetChaptersFolderPath(locale), @"\", chapter, @"\", _audioFileName);
        
        public static string GetChapterArAudioPath(BrodskyLocale locale, string chapter) =>
            String.Concat(GetChaptersFolderPath(locale),@"\", chapter,@"\", _arAudioFileName);

        public static string GetChapterSpritePath(BrodskyLocale locale, string chapter, string fileName) =>
            String.Concat(GetChaptersFolderPath(locale), @"\",chapter,@"\", _imagesFolderName,@"\", fileName);
    }
}