using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RimWorld;
using Verse;
using SettingsHelper;
using System.Text;

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
        public override string SettingsCategory() => "SettingsCategoryLabel".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            //Default settings
            listing_Standard.AddLabeledCheckbox("RPC_ColonyLabel".Translate() + " ", ref settings.RPC_Colony);
            listing_Standard.AddLabeledCheckbox("RPC_ColonistCountLabel".Translate() + " ", ref settings.RPC_ColonistCount);
            listing_Standard.AddLabeledCheckbox("RPC_YearLabel".Translate() + " ", ref settings.RPC_Year);
            listing_Standard.AddLabeledCheckbox("RPC_YearShortLabel".Translate() + " ", ref settings.RPC_YearShort);
            listing_Standard.AddLabeledCheckbox("RPC_QuadrumLabel".Translate() + " ", ref settings.RPC_Quadrum);
            listing_Standard.AddLabeledCheckbox("RPC_DayLabel".Translate() + "  ", ref settings.RPC_Day);
            listing_Standard.AddLabeledCheckbox("RPC_HourLabel".Translate() + "  ", ref settings.RPC_Hour);
            listing_Standard.AddLabeledCheckbox("RPC_TimeLabel".Translate() + "  ", ref settings.RPC_Time);
            //Custom field settings
            listing_Standard.AddLabeledCheckbox("CustomTopCheckbox".Translate() + "  ", ref settings.RPC_CustomTop);
            if (RWRPCMod.settings.RPC_CustomTop)
            {
                listing_Standard.AddLabeledTextField("CustomTopLabel".Translate() + " ", ref settings.RPC_CustomTopText);
            }
            listing_Standard.AddLabeledCheckbox("CustomBottomCheckbox".Translate() + "  ", ref settings.RPC_CustomBottom);
            if (RWRPCMod.settings.RPC_CustomBottom)
            {
                listing_Standard.AddLabeledTextField("CustomBottomLabel".Translate() + " ", ref settings.RPC_CustomBottomText);
            }
            if (listing_Standard.ButtonText("RPC_UpdateLabel".Translate()))
            {
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
