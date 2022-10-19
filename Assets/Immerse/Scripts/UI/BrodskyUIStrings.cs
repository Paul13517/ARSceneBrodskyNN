namespace Immerse.Brodsky.UI
{
    public static class BrodskyUIStrings
    {
        public static string ViewersConnected = "viewers connected";
        
        //chapter
        public static string Chapter => BrodskySettings.Locale switch {
            BrodskyLocale.English => "Chapter",
            BrodskyLocale.Russian => "Глава"
        };

        public static string ChapterTimeOver = "The chapter is over";
        public static string ChapterTimeLeft = "until the end of the chapter";
        public static string ARChapterTimeLeft = "until the end of the AR chapter";
        public static string ARButtonMasterText = "View AR";
        public static string ARButtonClientText = "Run AR";

        
        //PopUp strings
        public static string FinishChapter = "Finish Chapter";
        public static string Cancel = "Cancel";
        public static string Yes = "Yes";
        public static string RunAnyway = "Run Anyway";
        
        public static string ChapterNotOver = "You haven’t finished the current chapter";
        public static string AreYouSure = "Are you sure?\nThis action cannot be undone.";

        // notification stripe
        public static string VideoSaved = "Video saved";
        public static string VideoNotSaved = "Error saving video";
    }
}