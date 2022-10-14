//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CrescentMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public static CoreAdvanced sAdv = new();

    public List<IOption> Generic = sAdv.MergeOptions;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = sAdv.OptionsStorage;
    // [Can Change] This should only be changed by the author.
    //              If true, it will not stop the script if the default case triggers and the user chose to only get mats
    private bool dontStopMissingIng = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Royce's Direclaw", "Spectral Lycan", "Spectral Lycan’s Hood", "Spectral Lycan’s Morph", "Spectral Lycan’s Backfur", "Howling Spectral Lycan", "Spectral Ground Flames", "Spectral Lycan’s Spear", "Spectral Lycan’s Claws " });
        Core.SetOptions();

        BuyAllMerge();

        Core.SetOptions(false);
    }

    public void BuyAllMerge()
    {
        if (!Core.isSeasonalMapActive("crescentmoon"))
            return;
        //Only edit the map and shopID here
        Adv.StartBuyAllMerge("crescentmoon", 2172, findIngredients);

        #region Dont edit this part
        void findIngredients()
        {
            ItemBase req = Adv.externalItem;
            int quant = Adv.externalQuant;
            int currentQuant = req.Temp ? Bot.TempInv.GetQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name);
            if (req == null)
            {
                Core.Logger("req is NULL");
                return;
            }

            switch (req.Name)
            {
                default:
                    bool shouldStop = Adv.matsOnly ? !dontStopMissingIng : true;
                    Core.Logger($"The bot hasn't been taught how to get {req.Name}." + (shouldStop ? " Please report the issue." : " Skipping"), messageBox: shouldStop, stopBot: shouldStop);
                    break;
                #endregion

                case "Royce's Direclaw":
                case "Spectral Lycan":
                case "Spectral Lycan’s Hood":
                case "Spectral Lycan’s Morph":
                case "Spectral Lycan’s Backfur":
                case "Howling Spectral Lycan":
                case "Spectral Ground Flames":
                case "Spectral Lycan’s Spear":
                case "Spectral Lycan’s Claws":
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("crescentmoon", "Royce", req.Name, quant, false);
                    break;
            }
        }
    }

    public List<IOption> Select = new()
    {
        new Option<bool>("71271", "Enchanted Spectral Lycan", "Mode: [select] only\nShould the bot buy \"Enchanted Spectral Lycan\" ?", false),
        new Option<bool>("71272", "Enchanted Spectral Hood", "Mode: [select] only\nShould the bot buy \"Enchanted Spectral Hood\" ?", false),
        new Option<bool>("71273", "Enchanted Spectral Morph", "Mode: [select] only\nShould the bot buy \"Enchanted Spectral Morph\" ?", false),
        new Option<bool>("71274", "Enchanted Spectral Backfur", "Mode: [select] only\nShould the bot buy \"Enchanted Spectral Backfur\" ?", false),
        new Option<bool>("71275", "Enchanted Howling Lycan", "Mode: [select] only\nShould the bot buy \"Enchanted Howling Lycan\" ?", false),
        new Option<bool>("71276", "Enchanted Ground Flames", "Mode: [select] only\nShould the bot buy \"Enchanted Ground Flames\" ?", false),
        new Option<bool>("71277", "Enchanted Spectral Spear", "Mode: [select] only\nShould the bot buy \"Enchanted Spectral Spear\" ?", false),
        new Option<bool>("71278", "Enchanted Spectral Claws", "Mode: [select] only\nShould the bot buy \"Enchanted Spectral Claws\" ?", false),
    };
}
