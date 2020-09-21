using UnityEngine;
using Verse;

namespace RimRPC
{
    class RWRPCSettings : ModSettings
    {
        #region variables
        public bool RPC_Colony = true;
        public bool RPC_ColonistCount = false;
        public bool RPC_Year = false;
        public bool RPC_YearShort = true;
        public bool RPC_Quadrum = true;
        public bool RPC_Day = true;
        public bool RPC_Hour = true;
        public bool RPC_Time = true;
        public bool RPC_CustomTop = false;
        public bool RPC_CustomBottom = false;
        public string RPC_CustomTopText = "OwO";
        public string RPC_CustomBottomText = "*nuzzles u*";
        #endregion

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref this.RPC_Colony, "RPC_Colony", true);
            Scribe_Values.Look<bool>(ref this.RPC_ColonistCount, "RPC_ColonistCount", false);
            Scribe_Values.Look<bool>(ref this.RPC_Year, "RPC_Year", false);
            Scribe_Values.Look<bool>(ref this.RPC_YearShort, "RPC_YearShort", true);
            Scribe_Values.Look<bool>(ref this.RPC_Quadrum, "RPC_Quadrum", true);
            Scribe_Values.Look<bool>(ref this.RPC_Day, "RPC_Day", true);
            Scribe_Values.Look<bool>(ref this.RPC_Hour, "RPC_Hour", true);
            Scribe_Values.Look<bool>(ref this.RPC_Time, "RPC_Time", true);
            Scribe_Values.Look<bool>(ref this.RPC_CustomTop, "RPC_CustomTop", false);
            Scribe_Values.Look<bool>(ref this.RPC_CustomTop, "RPC_CustomBottom", false);
            Scribe_Values.Look<string>(ref this.RPC_CustomTopText, "RPC_CustomTopText", "OwO");
            Scribe_Values.Look<string>(ref this.RPC_CustomTopText, "RPC_CustomBottomText", "*nuzzles u*");
        }
    }

    class RWRPCMod : Mod
    {
        public static RWRPCSettings settings;
        public RWRPCMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<RWRPCSettings>();
        }
        public override string SettingsCategory() => "RPC_CategoryLabel".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            //Default settings
            listing_Standard.CheckboxLabeled("RPC_ColonyLabel".Translate() + " ", ref settings.RPC_Colony);
            listing_Standard.CheckboxLabeled("RPC_ColonistCountLabel".Translate() + " ", ref settings.RPC_ColonistCount);
            listing_Standard.CheckboxLabeled("RPC_YearLabel".Translate() + " ", ref settings.RPC_Year);
            listing_Standard.CheckboxLabeled("RPC_YearShortLabel".Translate() + " ", ref settings.RPC_YearShort);
            listing_Standard.CheckboxLabeled("RPC_QuadrumLabel".Translate() + " ", ref settings.RPC_Quadrum);
            listing_Standard.CheckboxLabeled("RPC_DayLabel".Translate() + "  ", ref settings.RPC_Day);
            listing_Standard.CheckboxLabeled("RPC_HourLabel".Translate() + "  ", ref settings.RPC_Hour);
            listing_Standard.CheckboxLabeled("RPC_TimeLabel".Translate() + "  ", ref settings.RPC_Time);
            //Custom field settings
            listing_Standard.CheckboxLabeled("CustomTopCheckbox".Translate() + "  ", ref settings.RPC_CustomTop);
            if (RWRPCMod.settings.RPC_CustomTop)
            {
                settings.RPC_CustomTopText = listing_Standard.TextEntryLabeled("CustomTopLabel".Translate() + " ", settings.RPC_CustomTopText);
            }
            listing_Standard.CheckboxLabeled("CustomBottomCheckbox".Translate() + "  ", ref settings.RPC_CustomBottom);
            if (RWRPCMod.settings.RPC_CustomBottom)
            {
                settings.RPC_CustomBottomText = listing_Standard.TextEntryLabeled("CustomBottomLabel".Translate() + " ", settings.RPC_CustomBottomText);
            }
            if (listing_Standard.ButtonText("RPC_UpdateLabel".Translate()))
            {
                Verse.Log.Message("UPDATED");
                var world = Current.Game != null ? Current.Game.World : null;
                if (world != null)
                {
                    StateHandler.PushState(Current.Game.CurrentMap);
                }

            }
            listing_Standard.End();
            settings.Write();
        }
    }
}
