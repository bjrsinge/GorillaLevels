using BepInEx;
using UnityEngine;
using HoneyLib.Events;
using HarmonyLib;
using System.Reflection;
using GorillaLevels.Scripts;

namespace GorillaLevels
{
    [BepInDependency("dev.auros.bepinex.bepinject")] // Why was utilla a dependency theres no need.
    [BepInPlugin("bjrsinge.gorillalevels", "GorillaLevels", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        internal int CurrentExperience; // Current Experience (Increases by 50 per tag)
        internal int CurrentLevel; // Current Level
        internal int NeededExperience; // Needed experience to level up (Increases by 100 per level (level 1 = 100 xp, level 2 = 200 xp)
        private int MaxExperience = 500; // The max experience can get for the level system

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            HoneyLib.Events.Events.TagHitLocal += TagHitLocal;
            Application.quitting += Application.Quit; // ??

            PlayerData playerData = DataSystem.GetPlayerData();
            CurrentExperience = playerData.CurrentExperience;
            CurrentLevel = playerData.CurrentLevel;
            NeededExperience = playerData.NeededExperience;

            new Harmony("bjrsinge.gorillalevels").PatchAll(Assembly.GetExecutingAssembly());
        }

        private void TagHitLocal(object sender, TagHitLocalArgs e)
        {
            if (CurrentExperience < MaxExperience)
            {
                CurrentExperience += 50; // 50 is the granted experience for a tag.
                CheckExperience();
                DataSystem.SaveData();
            }
        }

        private void LevelUp(int level)
        {
            CurrentLevel = level;
            CurrentExperience = 0;

            #if DEBUG
            Debug.Log("LEVELED UP!");
            #endif
        }

        private void CheckExperience()
        {
            switch (CurrentExperience)
            {
                case 100:
                    LevelUp(1);
                    break;

                case 200:
                    LevelUp(2);
                    break;

                case 300:
                    LevelUp(3);
                    break;

                case 400:
                    LevelUp(4);
                    break;

                case 500:
                    LevelUp(5);
                    break;
            }
        }
    }
}
