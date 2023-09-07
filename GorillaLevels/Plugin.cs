using BepInEx;
using System;
using UnityEngine;
using HoneyLib;
using HoneyLib.Events;

namespace GorillaLevels
{
    [BepInDependency("org.legoandmars.gorillatag.utilla")] /* Removed required version since there is no need for a required version. */
    [BepInPlugin(GUID, PROJECT, VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        internal int CurrentExperience; // Current Experience (Increases by 50 per tag)
        internal int CurrentLevel; // Current Level

        internal int NeededExperience; // Needed experience to level up (Increases by 100 per level (level 1 = 100 xp, level 2 = 200 xp)
        internal int NextLevel; // Next level (not sure if this is gonna be used tho

        /* I changed the variable names so they're more understandable and also tried to translate it lol.*/

        internal const string
            GUID = "bjrsinge.gorillalevels",
            PROJECT = "GorillaLevels",
            VERSION = "1.0.0";

        private void Start()
        {
            HoneyLib.Events.Events.TagHitLocal += TagHitLocal;
            Utilla.Events.GameInitialized += OnGameInitialized;
            Application.quitting += Application.Quit; // ??
        }

        private void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {

        }

        private void Update()
        {

        }

        private void TagHitLocal(object sender, TagHitLocalArgs e)
        {
            CurrentExperience += 50; // 50 is the granded experience for a tag.
            CheckExperience();
        }

        private void LevelUp(int level){CurrentLevel = level; CurrentExperience = 0;}
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

        /* I removed the ModdedGamemode methods since we don't need that for this mod. */
    }
}
