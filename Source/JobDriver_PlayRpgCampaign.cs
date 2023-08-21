using RimWorld;
using Verse.AI;
using Verse;
using System;

namespace BoardgameDev
{
    public class JobDriver_PlayRpgCampaign : JobDriver_SitFacingBuilding
    {
        private static readonly ThoughtDef playedRpgGame = DefDatabase<ThoughtDef>.GetNamed("PlayedRpgGame");

        private Thing Campaign => TargetThingA;

        protected override void ModifyPlayToil(Toil toil)
        {
            toil.AddFinishAction(delegate
            {
                pawn.needs.mood.thoughts.memories.TryGainMemory(
                    ThoughtMaker.MakeThought(playedRpgGame, ChooseThoughtStage())
                );
                Campaign.TryGetComp<RpgCampaignComp>().PlayOnce();
            });
        }

        private int ChooseThoughtStage()
        {
            var quality = Campaign.TryGetComp<CompQuality>();
            int stage = 0;
            switch (quality.Quality)
            {
                case QualityCategory.Awful:
                    stage = Rand.ElementByWeight(0, 0.7f, 1, 0.2f, 2, 0.1f);
                    break;
                case QualityCategory.Poor:
                    stage = Rand.ElementByWeight(0, 0.5f, 1, 0.3f, 2, 0.2f);
                    break;
                case QualityCategory.Normal:
                    stage = Rand.ElementByWeight(0, 0.3f, 1, 0.3f, 2, 0.3f, 3, 0.1f);
                    break;
                case QualityCategory.Good:
                    stage = Rand.ElementByWeight(0, 0.2f, 1, 0.2f, 2, 0.3f, 3, 0.2f, 4, 0.1f);
                    break;
                case QualityCategory.Excellent:
                    stage = Rand.ElementByWeight(0, 0.1f, 1, 0.1f, 2, 0.3f, 3, 0.3f, 4, 0.2f);
                    break;
                case QualityCategory.Masterwork:
                    stage = Rand.ElementByWeight(1, 0.1f, 2, 0.2f, 3, 0.3f, 4, 0.3f, 5, 0.1f);
                    break;
                case QualityCategory.Legendary:
                    stage = Rand.ElementByWeight(2, 0.2f, 3, 0.3f, 4, 0.3f, 5, 0.2f);
                    break;
            }
            if (Campaign.TryGetComp<RpgCampaignComp>().Finished)
            {
                stage = Math.Min(stage, 1);
            }
            return stage;
        }
    }
}
