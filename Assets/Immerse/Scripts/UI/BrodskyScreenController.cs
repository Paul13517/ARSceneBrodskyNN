using System.Collections.Generic;
using UnityEngine;

namespace Immerse.Brodsky.UI
{
    public class BrodskyScreenController : MonoBehaviour
    {
        [SerializeField] private IntroScreen _introScreen;
        [SerializeField] private ARCheckingScreen _arCheckingScreen;
        [SerializeField] private PermissionScreen _permissionScreen;
        [SerializeField] private LocalizationScreen _localizationScreen;
        [SerializeField] private DownloadingScreen _downloadingScreen;
        [SerializeField] private ChooseRoleScreen _chooseRoleScreen;
        [SerializeField] private ConnectionScreen _connectionScreen;
        [SerializeField] private WaitingScreen _waitingScreen;
        [SerializeField] private ChapterScreen _chapterScreen;
        [SerializeField] private OutroScreen _outroScreen;
        [SerializeField] private List<BrodskyScreen> _screensCanvas;

        private Queue<BrodskyScreen> _screens = new Queue<BrodskyScreen>();
        private BrodskyScreen _currentScreen;
        
        private static BrodskyScreenController _instance;
        public static BrodskyScreenController Instance => _instance ??= FindObjectOfType<BrodskyScreenController>();
        
        private void Awake()
        {
            _currentScreen = _introScreen;
            _introScreen.Open();
            _introScreen.Init();
            
            
        }
        public void OpenScreen(string screenName, bool closeCurrentscene = true)
        {
            //print("HU HU HE HE");
            if (closeCurrentscene)
            {
                _currentScreen.gameObject.SetActive(false);
            }

            foreach (BrodskyScreen screen in _screensCanvas)
            {
                if (screen.gameObject.name.Contains(screenName))
                {
                    _currentScreen = screen; 
                }
            }
            _currentScreen.Open();
            _currentScreen.Init();
        }
        private void Init()
        {
            _screens.Enqueue(_introScreen);
            _screens.Enqueue(_arCheckingScreen);
            _screens.Enqueue(_permissionScreen);
            _screens.Enqueue(_localizationScreen);
            _screens.Enqueue(_downloadingScreen);
            _screens.Enqueue(_chooseRoleScreen);
            _screens.Enqueue(_connectionScreen);
            _screens.Enqueue(_waitingScreen);
            _screens.Enqueue(_chapterScreen);
            _screens.Enqueue(_outroScreen);

            foreach (BrodskyScreen screen in _screens)
            {
                screen.Init();
                screen.HideToBase();
            }
        }

        private void DequeueScreen()
        {
            if (_currentScreen && _screens.Count <= 0)
            {
                return;
            }
            
            _currentScreen = _screens.Dequeue();
            _currentScreen.Open();
            _currentScreen.Exited += OnScreenExit;
        }


        private void OnScreenExit()
        {
            _currentScreen.Exited -= OnScreenExit;
            DequeueScreen();
        }
    }
}