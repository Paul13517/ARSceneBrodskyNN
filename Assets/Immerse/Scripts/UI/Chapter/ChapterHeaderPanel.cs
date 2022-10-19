using Immerse.Core.UI;
using TMPro;
using UnityEngine;

namespace Immerse.Brodsky.UI
{
    public class ChapterHeaderPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _chapterOrderText;
        [SerializeField] private TMP_Text _chapterNameText;
        [SerializeField] private DiscreteProgressBar _progressBar;


        public void Init(int chaptersCount)
        {
            _progressBar.Init(chaptersCount);
        }

        public void Setup(Chapter chapter)
        {
            _chapterOrderText.text = $"{BrodskyUIStrings.Chapter} {chapter.SortingOrder}";
            _chapterNameText.text = $"{chapter.Name}";
            _progressBar.UpdateProgress(chapter.SortingOrder);
        }
    }
}