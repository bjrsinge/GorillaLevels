using BepInEx;
using System;
using UnityEngine;
using Utilla;
using HoneyLib;
using HoneyLib.Events;

namespace GorillaLevels
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public int xp; // ton xp (incréments par 50 pour chaque tag)
        public int lvl; // ton niveau
        public int nlvlxp; // le xp tu as besoin de pour le niveau prochain (incréments par 250 pour chaque niveau (niveau 1 = 100 xp, niveau 2 = 200 xp)
        public bool mod_init;
        bool inRoom;

        void Start()
        {
            HoneyLib.Events.Events.TagHitLocal += TagHitLocal;
            Utilla.Events.GameInitialized += OnGameInitialized;
            Application.quitting += Application.Quit; // jsp si ça marche
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            mod_init = true;
            nlvlxp = lvl * 250;
        }

        void Update()
        {

        }

        void TagHitLocal(object sender, TagHitLocalArgs e)
        {
            xp += 50;
            if (xp == nlvlxp)
            {
                lvl += 1;
                xp = 0;
            }
            else
            {
                // jsp
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}
