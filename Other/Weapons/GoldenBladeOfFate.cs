//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class GoldenBladeOfFate
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetGBoF();

        Core.SetOptions(false);
    }

    public void GetGBoF()
    {
        if (Core.CheckInventory("Golden Blade of Fate"))
            return;

        Story.PreLoad(this);

        if (!Core.isCompletedBefore(5679))
        {
            Core.Logger("Doing for the Golden Blade of Fate");

            // The Lost Teacher
            Story.KillQuest(5669, "tutor", "Horc Tutor Trainer");

            // Big Gold Coins
            Story.KillQuest(5670, "prison", "Piggy Drake");

            // Light as a Feather
            Story.KillQuest(5671, "lavarun", "Phedra");

            // Shard Shard Shard
            Story.KillQuest(5672, "chaoscrypt", "Chaorrupted Armor");

            // White Scales, Light Scales
            Story.KillQuest(5673, "j6", "Sketchy Frogzard");

            // The Stench of Defeat
            Story.MapItemQuest(5674, "orcpath", 5143, 3);

            // If you can't stand the heat...
            Story.KillQuest(5675, "lair", "Red Dragon");

            // The Depths of Despair
            Story.MapItemQuest(5676, "well", 5144);

            // All Things Green and Small...
            Story.KillQuest(5677, "cellar", "GreenRat");

            // Doom... Or Redemption?
            if (!Story.QuestProgression(5678))
            {
                Core.EnsureAccept(5678);
                if (!Core.CheckInventory(54131))
                    Core.HuntMonster("sepulchure", "Dark Sepulchure");
                Core.EnsureComplete(5678);
            }

            Story.MapItemQuest(5679, "yulgar", 5145);
            Bot.Wait.ForPickup("Golden Blade of Fate");
        }
    }
}