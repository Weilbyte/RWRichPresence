using Harmony;
using System;
using System.Reflection;
using Verse;

namespace RimRPC
{
    public class Mod : Verse.Mod
    {
        public Mod(ModContentPack content) : base(content)
        {
            HarmonyInstance femboyfoxes = HarmonyInstance.Create("neon.rimworld.rimrpc.main");
            MethodInfo targetmethod = AccessTools.Method(typeof(Verse.GenScene), "GoToMainMenu");
            HarmonyMethod postfixmethod = new HarmonyMethod(typeof(RimRPC).GetMethod("GoToMainMenu_Postfix"));
            femboyfoxes.Patch(targetmethod, null, postfixmethod);
            Log.Message("[RichPresence] Patched");
            RimRPC.BootMeUp();
        }
    }

    public class RimRPC
    {
        internal static DiscordRPC.RichPresence prsnc;
        internal static string Colony;
        internal static int onDay;

        public static void BootMeUp()
        {
            DiscordRPC.EventHandlers eventHandlers = default(DiscordRPC.EventHandlers);
            eventHandlers.readyCallback = (DiscordRPC.ReadyCallback)Delegate.Combine(eventHandlers.readyCallback, new DiscordRPC.ReadyCallback(ReadyCallback));
            eventHandlers.disconnectedCallback = (DiscordRPC.DisconnectedCallback)Delegate.Combine(eventHandlers.disconnectedCallback, new DiscordRPC.DisconnectedCallback(DisconnectedCallback));
            eventHandlers.errorCallback = (DiscordRPC.ErrorCallback)Delegate.Combine(eventHandlers.errorCallback, new DiscordRPC.ErrorCallback(ErrorCallback));
            eventHandlers.joinCallback = (DiscordRPC.JoinCallback)Delegate.Combine(eventHandlers.joinCallback, new DiscordRPC.JoinCallback(JoinCallback));
            eventHandlers.spectateCallback = (DiscordRPC.SpectateCallback)Delegate.Combine(eventHandlers.spectateCallback, new DiscordRPC.SpectateCallback(SpectateCallback));
            eventHandlers.requestCallback = (DiscordRPC.RequestCallback)Delegate.Combine(eventHandlers.requestCallback, new DiscordRPC.RequestCallback(RequestCallback));
            DiscordRPC.Initialize("428272711702282252", ref eventHandlers, true, "0612");
            prsnc = default(DiscordRPC.RichPresence);
            prsnc.largeImageKey = "logo";
            prsnc.state = "Main Menu";
            DiscordRPC.UpdatePresence(ref RimRPC.prsnc);
            ReadyCallback();
        }

        private static void RequestCallback(DiscordRPC.JoinRequest request)
        {
        }

        private static void SpectateCallback(string secret)
        {
        }

        private static void JoinCallback(string secret)
        {
        }

        private static void ErrorCallback(int errorCode, string message)
        {
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            Log.Message("[Rich PresOwOnce] oopsie woopsie. we made a wittle fucky wucky!");
        }

        private static void ReadyCallback()
        {
            Log.Message("[Rich Presence] Discord Rich Presence is loaded and running!");
        }

        public static void GoToMainMenu_Postfix()
        {
            StateHandler.MenuState();
        }
    }
}