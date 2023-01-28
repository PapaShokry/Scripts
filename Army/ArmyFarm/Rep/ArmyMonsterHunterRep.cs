/*
name: Army Monster Hunter Rep
description: Farm reputation with your army. Faction: Monster Hunter
tags: army, reputation, monster hunter
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyMonsterHunterRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyMonsterHunterRep";
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
        if (Farm.FactionRank("Monster Hunter") >= 10)
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(ClassType.Farm);
        if (!Bot.Quests.IsAvailable(5850))
        {
            Core.KillMonster("pilgrimage", "r5", "Left", "SpiderWing", "Spiderwing Captured", 4, log: false);
            Core.KillMonster("pilgrimage", "r5", "Left", "Urstrix", "Urstrix Captured", 4, log: false);
            Core.Logger($"Completed Quest Capture the Misshapen");
        }
        Core.RegisterQuests(5849, 5850); //Capture the Misshapen 5849, Defeat the Parasites 5850
        Farm.ToggleBoost(BoostType.Reputation);
        Army.SmartAggroMonStart("pilgrimage", "SpiderWing", "Urstrix", "Ravenous Parasite");
        while (!Bot.ShouldExit && Farm.FactionRank("Monster Hunter") < 10)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.CancelRegisteredQuests();
    }
}
