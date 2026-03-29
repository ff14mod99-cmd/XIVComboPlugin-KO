using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVCombo.Combos;

internal static class SMN
{
    public const byte ClassID = 26;
    public const byte JobID = 27;

    public const uint

        Ruin = 163,
        Ruin2 = 172,
        Ruin3 = 3579,
        Ruin4 = 7426,
        Fester = 181,
        Painflare = 3578,
        DreadwyrmTrance = 3581,
        Deathflare = 3582,
        SummonBahamut = 7427,
        EnkindleBahamut = 7429,
        Physick = 16230,
        EnergySyphon = 16510,
        Outburst = 16511,
        EnkindlePhoenix = 16516,
        EnergyDrain = 16508,
        SummonCarbuncle = 25798,
        RadiantAegis = 25799,
        Aethercharge = 25800,
        SearingLight = 25801,
        SummonRuby = 25802,
        SummonTopaz = 25803,
        SummonEmerald = 25804,
        SummonIfrit = 25805,
        SummonTitan = 25806,
        SummonGaruda = 25807,
        AstralFlow = 25822,
        TriDisaster = 25826,
        Rekindle = 25830,
        SummonPhoenix = 25831,
        CrimsonCyclone = 25835,
        MountainBuster = 25836,
        Slipstream = 25837,
        SummonIfrit2 = 25838,
        SummonTitan2 = 25839,
        SummonGaruda2 = 25840,
        CrimsonStrike = 25885,
        Gemshine = 25883,
        PreciousBrilliance = 25884,
        Necrosis = 36990,
        SearingFlash = 36991,
        SummonSolarBahamut = 36992,
        Sunflare = 36996,
        LuxSolaris = 36997,
        EnkindleSolarBahamut = 36998;

    public static class Buffs
    {
        public const ushort
            Aetherflow = 304,
            FurtherRuin = 2701,
            SearingLight = 2703,
            IfritsFavor = 2724,
            GarudasFavor = 2725,
            TitansFavor = 2853,
            RubysGlimmer = 3873,
            LuxSolarisReady = 3874,
            CrimsonStrikeReady = 4403;
    }

    public static class Debuffs
    {
        public const ushort
            Placeholder = 0;
    }

    public static class Levels
    {
        public const byte
            SummonCarbuncle = 2,
            RadiantAegis = 2,
            Gemshine = 6,
            EnergyDrain = 10,
            Fester = 10,
            PreciousBrilliance = 26,
            Painflare = 40,
            EnergySyphon = 52,
            Ruin3 = 54,
            Ruin4 = 62,
            SearingLight = 66,
            EnkindleBahamut = 70,
            Rekindle = 80,
            ElementalMastery = 86,
            SummonPhoenix = 80,
            Necrosis = 92,
            SummonSolarBahamut = 100,
            LuxSolaris = 100;
    }
}

internal class SummonerFester : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerEDFesterFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == SMN.Fester || actionID == SMN.Necrosis)
        {
            var gauge = GetJobGauge<SMNGauge>();

            if (level >= SMN.Levels.EnergyDrain && !gauge.HasAetherflowStacks)
                return SMN.EnergyDrain;
        }

        return actionID;
    }
}

internal class SummonerRuin : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerRuinGemFeature;
    protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level)
    {
        if (actionID == SMN.Ruin || actionID == SMN.Ruin2 || actionID == SMN.Ruin3)
        {
            if (CanUseAction(OriginalHook(SMN.Gemshine)))
            {
                return OriginalHook(SMN.Gemshine);
            }
        }
        else if (actionID == SMN.TriDisaster)
        {
            if (CanUseAction(OriginalHook(SMN.PreciousBrilliance)))
            {
                return OriginalHook(SMN.PreciousBrilliance);
            }
        }

        return actionID;
    }
}

internal class SummonerPainflare : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerESPainflareFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == SMN.Painflare)
        {
            var gauge = GetJobGauge<SMNGauge>();

            if (level >= SMN.Levels.EnergySyphon && !gauge.HasAetherflowStacks)
                return SMN.EnergySyphon;

            if (level >= SMN.Levels.EnergyDrain && !gauge.HasAetherflowStacks)
                return SMN.EnergyDrain;

            if (level < SMN.Levels.Painflare)
                return SMN.Fester;
        }

        return actionID;
    }
}

internal class SummonerLuxSolarisFeature : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SummonerSummonLuxSolarisFeature;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID is SMN.SummonBahamut or SMN.SummonSolarBahamut)
        {
            if (HasEffect(SMN.Buffs.LuxSolarisReady))
                return SMN.LuxSolaris;
        }

        return actionID;
    }
}