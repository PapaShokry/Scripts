/*
name: Army Ravenloss Rep
description: Farm reputation with your army. Faction: Ravenloss
tags: army, reputation, ravenloss
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyRavenlossRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyRavenlossRep";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6, //adjust if needed, check maps limit on wiki
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        if (Farm.FactionRank("Ravenloss") >= 10)
            return;

        if (!Bot.Quests.IsAvailable(3445))
        {
            Core.Logger("Quest Locked \"Slay the Spiderkin\" (/twilightedge)", messageBox: true);
            return;
        }

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(3445); //Slay the Spiderkin 3445
        Farm.ToggleBoost(BoostType.Reputation);
        Army.SmartAggroMonStart("twilightedge", "ChaosWeaver Mage");
        while (!Bot.ShouldExit && Farm.FactionRank("Ravenloss") < 10)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.CancelRegisteredQuests();
    }
}
