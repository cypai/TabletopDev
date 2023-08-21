using RimWorld;
using Verse;

namespace BoardgameDev
{
    public class JoyGiver_EscapeRoom : JoyGiver_WatchBuilding
    {

        protected override bool CanInteractWith(Pawn pawn, Thing t, bool inBed)
        {
            var escapeRoomComp = t.TryGetComp<EscapeRoomComp>();
            return escapeRoomComp != null && base.CanInteractWith(pawn, t, inBed) && escapeRoomComp.CanParticipate(pawn);
        }

    }
}
