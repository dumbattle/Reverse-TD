using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LPE;
using Core.Campaign;


namespace MainMenu {
    public class MainMenu_Main : MonoBehaviour {
        public Image fadeImage;
        public MainButtons mainButtons;
        public CampaignMenu campaignMenu;
        public EndlessMenu endlessMenu;

        void Start() {
            fadeImage.color = new Color(0, 0, 0, 0);
            endlessMenu.rootObject.SetActive(false);
            campaignMenu.rootObject.SetActive(false);
            Core.FrameUtility.SetFrameRate(Core.FPSMode._60);
            Application.targetFrameRate = 60;
            MainMenuStateController.Init(this);
        }

        void Update() {
            if (Input.GetKey(KeyCode.Escape)) {
                MainMenuInputManager.Set.Cancel();
            }

            MainMenuStateController.Update(this);

        }

        [Serializable]
        public class CampaignMenu {
            public GameObject rootObject;
            public LPEButtonBehaviour startButton;


            public MainMenu_LevelSelectEntryBehaviour levelEntrySrc;
            public Sprite levelEntryBackground_unselected;
            public Sprite levelEntryBackground_selected;
            public Sprite starBlank;
            public Sprite starFilled1;
            public Sprite starFilled2;
            public Sprite starFilled3;

            List<MainMenu_LevelSelectEntryBehaviour> levelEntries = new List<MainMenu_LevelSelectEntryBehaviour>();

            public int currentWorldIndex { get; private set; } = 0;
            public int currentSelectedLevelIndex { get; private set; }

            public void Open() {
                // precation
                levelEntrySrc.gameObject.SetActive(false); 

                // open current world
                OpenWorld(currentWorldIndex);
            }

            public void OpenWorld(int worldIndex) {
                // open menu
                rootObject.SetActive(true);

                // close all level entries
                foreach (var e in levelEntries) {
                    e.gameObject.SetActive(false);
                }

                //get world
                var world = WorldCollection.GetWorld(currentWorldIndex);

                // set entries
                currentSelectedLevelIndex = -1;
                for (int i = 0; i < world.NumLevels(); i++) {
                    if (levelEntries.Count <= i) {
                        // create new entry
                        int index = i;
                        var newEntry = Instantiate(levelEntrySrc, levelEntrySrc.transform.parent);
                        newEntry.button.SetClickListener(() => SelectLevel(index));
                        levelEntries.Add(newEntry);
                    }

                    // get entry
                    var e = levelEntries[i];

                    // set values
                    e.gameObject.SetActive(true);
                    e.bakground.sprite = levelEntryBackground_unselected;
                    e.levelText.text = $"{currentWorldIndex + 1}.{i + 1}";
                }

                // select first level
                SelectLevel(0);
            }
            public void SelectLevel(int level) {
                if (currentSelectedLevelIndex >= 0) {
                    levelEntries[currentSelectedLevelIndex].bakground.sprite = levelEntryBackground_unselected;
                }
                currentSelectedLevelIndex = level;
                levelEntries[currentSelectedLevelIndex].bakground.sprite = levelEntryBackground_selected;
            }
        }

        [Serializable]
        public struct EndlessMenu {
            public GameObject rootObject;
            public LPEButtonBehaviour smallMapButton;
            public LPEButtonBehaviour mediumMapButton;
            public LPEButtonBehaviour largeMapButton;
        }
        [Serializable]
        public struct MainButtons {
            public Sprite unselectedFrame;
            public Sprite selectedFrame;
            
            public LPEButtonBehaviour campaignButton;
            public Image campaignButtonFrame;
            //public RectTransform campaignRect;

            public LPEButtonBehaviour endlessButton;
            public Image endlessButtonFrame;
            //public RectTransform endlessRect;
        }
    }
}
