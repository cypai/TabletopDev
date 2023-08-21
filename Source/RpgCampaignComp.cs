using RimWorld;
using Verse;
using System;

namespace TabletopDev
{
    public class RpgCampaignCompProperties : CompProperties
    {
        public int sessionsPerColonist;

        public RpgCampaignCompProperties()
        {
            compClass = typeof(RpgCampaignComp);
        }
    }

    public class RpgCampaignComp : ThingComp
    {
        private int timesPlayed;
        private bool finished;

        private RpgCampaignCompProperties Props => (RpgCampaignCompProperties)props;

        public bool Finished => finished;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref timesPlayed, "timesPlayed", 20);
            Scribe_Values.Look(ref finished, "finished", false);
        }

        private int SessionsLeft()
        {
            int colonists = PawnsFinder.AllMaps_FreeColonists.Count;
            int colonistsAdjusted = (int)Math.Ceiling(Math.Pow(colonists, 0.8));

            int qualityBonus = 1;
            switch (parent.TryGetComp<CompQuality>().Quality)
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

            int totalSessions = colonistsAdjusted * Props.sessionsPerColonist * qualityBonus;
            return totalSessions - timesPlayed;
        }

        public void PlayOnce()
        {
            timesPlayed++;
            if (SessionsLeft() == 0)
            {
                finished = true;
            }
        }

        public override string CompInspectStringExtra()
        {
            if (Finished)
            {
                return "Finished.";
            }
            else
            {
                return $"Sessions Left: {SessionsLeft()}";
            }
        }
    }
}
