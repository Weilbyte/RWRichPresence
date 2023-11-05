using UnityEngine;
using Verse;

namespace RimRPC
{
    class RwrpcSettings : ModSettings
    {
        #region variables
        public bool RpcColony = true;
        public bool RpcColonistCount;
        public bool RpcYear;
        public bool RpcYearShort = true;
        public bool RpcQuadrum = true;
        public bool RpcDay = true;
        public bool RpcHour = true;
        public bool RpcTime = true;
        public bool RpcCustomTop;
        public bool RpcCustomBottom;
        public string RpcCustomTopText = "Example";
        public string RpcCustomBottomText = "Example";
        #endregion

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref RpcColony, "RPC_Colony", true);
            Scribe_Values.Look(ref RpcColonistCount, "RPC_ColonistCount");
            Scribe_Values.Look(ref RpcYear, "RPC_Year");
            Scribe_Values.Look(ref RpcYearShort, "RPC_YearShort", true);
            Scribe_Values.Look(ref RpcQuadrum, "RPC_Quadrum", true);
            Scribe_Values.Look(ref RpcDay, "RPC_Day", true);
            Scribe_Values.Look(ref RpcHour, "RPC_Hour", true);
            Scribe_Values.Look(ref RpcTime, "RPC_Time", true);
            Scribe_Values.Look(ref RpcCustomTop, "RPC_CustomTop");
            Scribe_Values.Look(ref RpcCustomTop, "RPC_CustomBottom");
            Scribe_Values.Look(ref RpcCustomTopText, "RPC_CustomTopText", "Example");
            Scribe_Values.Look(ref RpcCustomTopText, "RPC_CustomBottomText", "*Example");
        }
    }

    class RWRPCMod : Mod
    {
        public static RwrpcSettings Settings;
        public RWRPCMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<RwrpcSettings>();
        }
        public override string SettingsCategory() => "RPC_CategoryLabel".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            //Default settings
            SetDefaultSettings(listingStandard);

            //Custom field settings
            SetCustomFields(listingStandard);

            listingStandard.End();
            Settings.Write();
        }

        private static void SetDefaultSettings(Listing_Standard listingStandard)
        {
            listingStandard.CheckboxLabeled("RPC_ColonyLabel".Translate() + " ", ref Settings.RpcColony);
            listingStandard.CheckboxLabeled("RPC_ColonistCountLabel".Translate() + " ", ref Settings.RpcColonistCount);
            listingStandard.CheckboxLabeled("RPC_YearLabel".Translate() + " ", ref Settings.RpcYear);
            listingStandard.CheckboxLabeled("RPC_YearShortLabel".Translate() + " ", ref Settings.RpcYearShort);
            listingStandard.CheckboxLabeled("RPC_QuadrumLabel".Translate() + " ", ref Settings.RpcQuadrum);
            listingStandard.CheckboxLabeled("RPC_DayLabel".Translate() + "  ", ref Settings.RpcDay);
            listingStandard.CheckboxLabeled("RPC_HourLabel".Translate() + "  ", ref Settings.RpcHour);
            listingStandard.CheckboxLabeled("RPC_TimeLabel".Translate() + "  ", ref Settings.RpcTime);
        }

        private static void SetCustomFields(Listing_Standard listingStandard)
        {
            listingStandard.CheckboxLabeled("CustomTopCheckbox".Translate() + "  ", ref Settings.RpcCustomTop);

            if (Settings.RpcCustomTop)
                Settings.RpcCustomTopText = listingStandard.TextEntryLabeled("CustomTopLabel".Translate() + " ", Settings.RpcCustomTopText);

            listingStandard.CheckboxLabeled("CustomBottomCheckbox".Translate() + "  ", ref Settings.RpcCustomBottom);

            if (Settings.RpcCustomBottom)
                Settings.RpcCustomBottomText = listingStandard.TextEntryLabeled("CustomBottomLabel".Translate() + " ", Settings.RpcCustomBottomText);


            if (listingStandard.ButtonText("RPC_UpdateLabel".Translate()))
            {
                var world = Current.Game?.World;
                if (world != null)
                    StateHandler.PushState(Current.Game.CurrentMap);
            }
        }
    }
}
