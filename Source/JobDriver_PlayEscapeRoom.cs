using RimWorld;
using Verse.AI;
using Verse;

namespace TabletopDev
{
    public class JobDriver_PlayEscapeRoom : JobDriver_SitFacingBuilding
    {
        private static readonly ThoughtDef playedEscapeRoom = DefDatabase<ThoughtDef>.GetNamed("PlayedEscapeRoom");

        private Thing EscapeRoom => TargetThingA;

        protected override void ModifyPlayToil(Toil toil)
        {
            toil.AddFinishAction(delegate
            {
                pawn.needs.mood.thoughts.memories.TryGainMemory(
                    ThoughtMaker.MakeThought(playedEscapeRoom, ChooseThoughtStage())
                );
                EscapeRoom.TryGetComp<EscapeRoomComp>().Play(pawn);
            });
        }

        private int ChooseThoughtStage()
        {
            var quality = EscapeRoom.TryGetComp<CompQuality>();
            int stage = 0;
            switch (quality.Quality)
            {
                case QualityCategory.Awful:
                    stage = Rand.ElementByWeight(0, 0.7f, 1, 0.3f);
                    break;
                case QualityCategory.Poor:
                    stage = Rand.ElementByWeight(0, 0.5f, 1, 0.5f);
                    break;
                case QualityCategory.Normal:
                    stage = Rand.ElementByWeight(0, 0.3f, 1, 0.7f);
                    break;
                case QualityCategory.Good:
                    stage = Rand.ElementByWeight(0, 0.1f, 1, 0.9f);
                    break;
                case QualityCategory.Excellent:
                    stage = Rand.ElementByWeight(1, 0.75f, 2, 0.25f);
                    break;
                case QualityCategory.Masterwork:
                    stage = Rand.ElementByWeight(1, 0.25f, 2, 0.75f);
                    break;
                case QualityCategory.Legendary:
                    stage = 2;
                    break;
            }
            return stage;
        }
    }
}
