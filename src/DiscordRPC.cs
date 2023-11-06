using System;
using System.Runtime.InteropServices;

namespace RimRPC
{
    internal class DiscordRPC
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReadyCallback();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DisconnectedCallback(int errorCode, string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ErrorCallback(int errorCode, string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JoinCallback(string secret);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SpectateCallback(string secret);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RequestCallback(JoinRequest request);

        public struct EventHandlers
        {
            public ReadyCallback ReadyCallback;
            public DisconnectedCallback DisconnectedCallback;
            public ErrorCallback ErrorCallback;
            public JoinCallback JoinCallback;
            public SpectateCallback SpectateCallback;
            public RequestCallback RequestCallback;
        }

        [Serializable]
        public struct RichPresence
        {
            public string State;
            public string Details;
            public long StartTimestamp;
            public long EndTimestamp;
            public string LargeImageKey;
            public string LargeImageText;
            public string SmallImageKey;
            public string SmallImageText;
            public string PartyId;
            public int PartySize;
            public int PartyMax;
            public string MatchSecret;
            public string JoinSecret;
            public string SpectateSecret;
            public bool Instance;
        }

        [Serializable]
        public struct JoinRequest
        {
            public string UserId;
            public string Username;
            public string Avatar;
        }

        public enum Reply
        {
            No,
            Yes,
            Ignore
        }

        [DllImport("0discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Initialize")]
        public static extern void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);

        [DllImport("0discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Shutdown")]
        public static extern void Shutdown();

        [DllImport("0discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RunCallbacks")]
        public static extern void RunCallbacks();

        [DllImport("0discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UpdatePresence")]
        public static extern void UpdatePresence(ref RichPresence presence);

        [DllImport("0discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Respond")]
        public static extern void Respond(string userId, Reply reply);
    }
}