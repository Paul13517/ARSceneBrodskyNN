using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Immerse.Brodsky.UI
{
    public class CommentsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _idlePanel;
        [SerializeField] private GameObject _commentPanel;
        
        [Space, SerializeField] private Image _image;
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _text;

        private float _clientCommentOffset = 230;
        

        public void RoleChosen(bool isMaster)
        {
            if (isMaster)
            {
                _image.enabled = false;
                return;
            }

            _header.rectTransform.anchoredPosition += new Vector2(0, _clientCommentOffset);
            _text.rectTransform.anchoredPosition += new Vector2(0, _clientCommentOffset);
        }
        
        public void SetComment(Comment comment)
        {
            _header.text = comment.Header;
            _text.text = comment.Text;
            _image.sprite = comment.Image;
            SetIdleEnabled(false);
        }

        public void RemoveComment()
        {
            SetIdleEnabled(true);
            _header.text = null;
            _text.text = null;
            _image.sprite = null;
        }

        private void SetIdleEnabled(bool isEnabled)
        {
            _idlePanel.SetActive(isEnabled);
            _commentPanel.SetActive(!isEnabled);
        }
    }
}