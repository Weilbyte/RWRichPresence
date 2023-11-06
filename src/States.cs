using Verse;

namespace RimRPC
{
    internal class States
    {
        public static string GetState(string state, string[] args)
        {
            var years = args[0];
            var days = args[1];
            var dayhour = args[2];
            var quadrum = args[3];
            var biome = args[4];

            if (RWRPCMod.Settings.RpcCustomBottom)
                return RWRPCMod.Settings.RpcCustomBottomText;

            if (RWRPCMod.Settings.RpcDay)
                state = GetRpcDay(state, days, dayhour);

            if (RWRPCMod.Settings.RpcQuadrum)
                state = GetRpcQuadrum(state, quadrum);

            if (RWRPCMod.Settings.RpcYear)
                state = GetRpcYear(state, years);

            if (RWRPCMod.Settings.RpcBiome)
                state += biome;

            return state;
        }

        private static string GetRpcDay(string state, string days, string dayhour)
        {
            state += $"{"RPC_Day".Translate()} {days}";

            if (RWRPCMod.Settings.RpcDay && RWRPCMod.Settings.RpcHour)
                state += $" ({dayhour}{"RPC_Hour".Translate()})";

            return state;
        }

        private static string GetRpcYear(string state, string years)
        {
            if (RWRPCMod.Settings.RpcQuadrum || RWRPCMod.Settings.RpcDay)
                state += " | ";

            if (RWRPCMod.Settings.RpcYearShort)
            {
                state += $"{years}{"RPC_ShortYears".Translate()}";
                return state;
            }
            state += $"{"RPC_Years".Translate()} {years}";

            return state;
        }

        private static string GetRpcQuadrum(string state, string quadrum)
        {
            if (RWRPCMod.Settings.RpcDay)
            {
                state += " | ";
            }
            state += quadrum;

            return state;
        }
    }
}
