using RimWorld;
using Verse;
using System;

namespace BoardgameDev
{
    public class RpgCampaignCompProperties : CompProperties
    {
        public RpgCampaignCompProperties()
        {
            compClass = typeof(RpgCampaignComp);
        }
    }

    public class RpgCampaignComp : ThingComp
    {
        private int sessionsLeft = 20;

        public int SessionsLeft => sessionsLeft;

        public bool Finished => sessionsLeft == 0;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref sessionsLeft, "sessionsLeft", 20);
        }

        public void Init(QualityCategory quality, int sessionsPerColonist)
        {
            int colonists = PawnsFinder.AllMaps_FreeColonists.Count;
            int colonistsAdjusted = (int)Math.Ceiling(Math.Pow(colonists, 0.8));

            int qualityBonus = 1;
            switch (quality)
            {
                case QualityCategory.Awful:
                    qualityBonus = 1;
                    break;
                case QualityCategory.Poor:
                    qualityBonus = 1;
                    break;
                case QualityCategory.Normal:
                    qualityBonus = 1;
                    break;
                case QualityCategory.Good:
                    qualityBonus = 2;
                    break;
                case QualityCategory.Excellent:
                    qualityBonus = 2;
                    break;
                case QualityCategory.Masterwork:
                    qualityBonus = 3;
                    break;
                case QualityCategory.Legendary:
                    qualityBonus = 4;
                    break;
            }

            sessionsLeft = colonistsAdjusted * sessionsPerColonist * qualityBonus;
        }

        public void PlayOnce()
        {
            sessionsLeft--;
        }

        public override string CompInspectStringExtra()
        {
            if (Finished)
            {
                return "Finished.";
            }
            else
            {
                return $"Sessions Left: {sessionsLeft}";
            }
        }
    }
}
