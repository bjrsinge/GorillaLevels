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

        internal int CurrentExperience; // Current Experience (Increases by 100 per tag)
        internal int CurrentLevel; // Current Level
        internal int NeededExperience; // Needed experience to level up (Increases by 250 per level (level 1 = 250 xp, level 2 = 500 xp)
        private int MaxExperience = 2500; // The max experience can get for the level system

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
                CurrentExperience += 100; // 100 is the granted experience for a tag.
                LevelUp();
                DataSystem.SaveData();
            }
        }

        private void LevelUp()
        {
            if (CurrentExperience >= NeededExperience && CurrentExperience < MaxExperience)
            {
                CurrentLevel += 1;
                CurrentExperience = 0;
                NeededExperience += 250;
                DataSystem.SaveData();
            }
            else if (CurrentExperience >= MaxExperience)
            {
                // make the gui or whatever interface i implement say "Max Level!" (may add some rebirth system like [NEW UPDATE] CLICK SIMULATOR 7@8283 BY #8CJDJS PAY ME NOWOW (im tired)
            }
        }
    }
}
