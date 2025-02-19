/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FrozenNorthlands
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (!Core.IsMember)
        {
            Core.Logger("This is a member-only storyline.");
            return;
        }

        if (Core.isCompletedBefore(3638))
            return;

        Story.PreLoad(this);

        // Cleanse the Chaorruption 3634
        Story.KillQuest(3634, "chaosnorth", new[] { "Chaotic Symbiote", "Chaorruption" });

        // Book of Chaos and Flames 3635
        Story.KillQuest(3635, "chaosnorth", new[] { "Chaorrupted Mage", "Chaorrupted Mage", "Chaorrupted Mage" });

        // Facets of Chaos 3636
        if (!Story.QuestProgression(3636))
        {
            Core.EnsureAccept(3636);
            Core.HuntMonster("chaosnorth", "Chaos Gemrald", "Shard of Chaos", 13);
            Core.EnsureComplete(3636);
        }

        // Chaos Eye Spy 3637
        Story.KillQuest(3637, "chaosnorth", new[] { "Chaorrupted Imp", "Chaorrupted Mage", "Chaorruption", "Chaos Sp-Eye" });

        // Defeat Chaorrupted Xan Illusion 3638
        Story.KillQuest(3638, "chaosnorth", "Chaorrupted Xan");

    }
}
