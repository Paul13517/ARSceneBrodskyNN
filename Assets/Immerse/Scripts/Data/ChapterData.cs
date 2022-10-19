using System.Collections.Generic;

namespace Immerse.Brodsky.Data
{
    public class ChapterData
    {
        public int sortingOrder;
        public string name;
        public bool hasAR;
        public string arDuration = "default";
        public string arAvailableAt = "default";
        public List<CommentData> comments;
    }
}