using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVCombo.Combos;

internal static class SGE
{
    public const byte JobID = 40;

    public const uint
        Dosis = 24283,
        Dosis2 = 24306,
        Dosis3 = 24312,
        Diagnosis = 24284,
        Kardia = 24285,
        Prognosis = 24286,
        Egeiro = 24287,
        Physis = 24288,
        Phlegma = 24289,
        Eukrasia = 24290,
        EukrasianDiagnosis = 24291,
        EukrasianPrognosis = 24292,
        Soteria = 24294,
        Druochole = 24296,
        Dyskrasia = 24297,
        Kerachole = 24298,
        Ixochole = 24299,
        Zoe = 24300,
        Pepsis = 24301,
        Physis2 = 24302,
        Taurochole = 24303,
        Toxikon = 24304,
        Haima = 24305,
        Phlegma2 = 24307,
        Rhizomata = 24309,
        Holos = 24310,
        Panhaima = 24311,
        Phlegma3 = 24313,
        EukrasianDosis = 24314,
        Dyskrasia2 = 24315,
        Krasis = 24317,
        Pneuma = 24318,
        EukrasianDyskrasia = 37032,
        EukrasianPrognosis2 = 37034,
        Psyche = 37033;

    public static class Buffs
    {
        public const ushort
            Kardion = 2604,
            Eukrasia = 2606;
    }

    public static class Debuffs
    {
        public const ushort
            EukrasianDosis = 2614,
            EukrasianDosis2 = 2615,
            EukrasianDosis3 = 2616;
    }

    public static class Levels
    {
        public const ushort
            Dosis = 1,
            Prognosis = 10,
            Egeiro = 12,
            Phlegma = 26,
            Soteria = 35,
            Druochole = 45,
            Dyskrasia = 46,
            Kerachole = 50,
            Ixochole = 52,
            Physis2 = 60,
            Taurochole = 62,
            Toxicon = 66,
            Haima = 70,
            Phlegma2 = 72,
            Dosis2 = 72,
            Rhizomata = 74,
            Holos = 76,
            Panhaima = 80,
            Phlegma3 = 82,
            Dosis3 = 82,
            Dyskrasia2 = 82,
            Toxikon2 = 82,
            Krasis = 86,
            Pneuma = 90,
            Psyche = 92,
            Philosophia = 100;
    }
}

internal class SageToxikonCombo : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SageToxiconCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        if (actionID == OriginalHook(SGE.Dosis) || actionID == OriginalHook(SGE.Dyskrasia))
        {
            var gauge = GetJobGauge<SGEGauge>();
            if (gauge.Addersting > 0)
            {
                return OriginalHook(SGE.Toxikon);
            }
        }
        return actionID;
    }
}

internal class SageTargetAreaSwap : CustomCombo
{
    protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.SageSwapCombo;

    protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
    {
        switch (actionID)
        {
            case SGE.Prognosis or SGE.EukrasianPrognosis or SGE.EukrasianPrognosis2 when HasTarget():
                return TargetIsEnemy() ? actionID : OriginalHook(SGE.Diagnosis);
            case SGE.Kerachole:
            {
                if (level >= SGE.Levels.Taurochole)
                {
                    if (CanUseAction(SGE.Taurochole))
                    {
                        if (HasTarget() && TargetIsEnemy() == false)
                        {
                            return SGE.Taurochole;
                        }
                    }
                }
                break;
            }
            case SGE.Druochole:
            {
                if (level >= SGE.Levels.Ixochole)
                {
                    if (CanUseAction(SGE.Ixochole))
                    {
                        if (HasTarget() == false || TargetIsEnemy())
                        {
                            return SGE.Ixochole;
                        }
                    }
                }
                break;
            }
            case SGE.Haima:
            {
                if (level >= SGE.Levels.Panhaima)
                {
                    if (CanUseAction(SGE.Panhaima))
                    {
                        if (HasTarget() == false || TargetIsEnemy())
                        {
                            return SGE.Panhaima;
                        }
                    }
                }
                break;
            }
        }


        return actionID;
    }
}
