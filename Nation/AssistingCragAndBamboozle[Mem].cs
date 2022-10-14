//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using System.Collections.Generic;

public class AssistingCragAndBamboozle
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] {"Nulgath Larvae",
                     "Sword of Nulgath", "Gem of Nulgath", "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath",
                     "Totem of Nulgath", "Blood Gem of the Archfiend", "Unidentified 19", "Elders' Blood", "Voucher of Nulgath", "Voucher of Nulgath (non-mem)"});
        Core.SetOptions();

        AssistingCandB();

        Core.SetOptions(false);
    }

    public void AssistingCandB(string Reward = "any")
    {
        if (!Core.IsMember || !Core.CheckInventory(Nation.CragName) || (!Core.CheckInventory("Sparrow's Blood") && !Daily.CheckDaily(803, true, "Sparrow's Blood")))
            return;

        if (!Core.CheckInventory("Tendurrr The Assistant"))
            Core.HuntMonster("tercessuinotlim", "Dark Makai", "Tendurrr The Assistant", 1, false);

        Core.AddDrop("Nulgath Larvae",
                     "Sword of Nulgath", "Gem of Nulgath", "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath",
                     "Totem of Nulgath", "Blood Gem of the Archfiend", "Unidentified 19", "Elders' Blood", "Voucher of Nulgath", "Voucher of Nulgath (non-mem)");

        Core.EnsureAccept(5817);
        Nation.EssenceofNulgath(20);
        if (!Core.CheckInventory("Sparrow's Blood"))
            Daily.SparrowsBlood();
        Nation.ApprovalAndFavor(100, 100);
        Nation.NationRound4Medal();

        if (!Core.CheckInventory("Sparrow's Blood"))
        {
            Core.Logger($"Not enough \"Sparrow's Blood\", please do the daily 1 more time (not today)", messageBox: true);
            return;
        }

        if (Reward == "any")
        {
            if (!Core.CheckInventory("Sword of Nulgath"))
                Core.EnsureComplete(5817, 4670);
            if (!Core.CheckInventory("Gem of Nulgath", 300))
                Core.EnsureComplete(5817, 6136);
            if (!Core.CheckInventory("Tainted Gem", 1000))
                Core.EnsureComplete(5817, 4769);
            if (!Core.CheckInventory("Dark Crystal Shard", 1000))
                Core.EnsureComplete(5817, 4770);
            if (!Core.CheckInventory("Diamond of Nulgath", 1000))
                Core.EnsureComplete(5817, 4771);
        }
        else
        {
            List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(5817).Rewards;
            Core.EnsureComplete(5817, RewardOptions.First(x => x.Name == Reward).ID);
        }
        Bot.Wait.ForPickup("*");
    }
}