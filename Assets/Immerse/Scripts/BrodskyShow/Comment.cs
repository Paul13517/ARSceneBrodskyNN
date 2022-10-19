using Immerse.Brodsky.Data;
using UnityEngine;

namespace Immerse.Brodsky
{
    public class Comment
    {
        public string Header { get; }
        public string Text { get; }
        
        public string ImagePath { get; }
        public bool HasImage => !string.IsNullOrEmpty(ImagePath);
        public Sprite Image { get; set; }

        public float EnterTime { get; }
        public float ExitTime { get; }

        
        public Comment(CommentData data)
        {
            Header = data.header;
            Text = data.text;
            ImagePath = data.imagePath;
            EnterTime = data.enterTime.ToSeconds();
            ExitTime = data.exitTime.ToSeconds();
        }
    }
}