namespace GorillaLevels.Scripts
{
    [System.Serializable]
    internal class PlayerData
    {
        public int CurrentExperience; // Current Experience
        public int CurrentLevel; // Current Level
        public int NeededExperience; // Needed experience to level up (Increases by 100 per level (level 1 = 100 xp, level 2 = 200 xp)

        internal PlayerData()
        {
            CurrentExperience = Plugin.Instance.CurrentExperience;
            CurrentLevel = Plugin.Instance.CurrentLevel;
            NeededExperience = Plugin.Instance.NeededExperience;
        }
    }
}
