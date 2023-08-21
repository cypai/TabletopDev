using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;

namespace BoardgameDev
{
    public class EscapeRoomCompProperties : CompProperties
    {
        public EscapeRoomCompProperties()
        {
            compClass = typeof(EscapeRoomComp);
        }
    }

    public class EscapeRoomComp : ThingComp
    {
        private List<Pawn> participants = new List<Pawn>();

        private EscapeRoomCompProperties Props => (EscapeRoomCompProperties)props;

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Collections.Look(ref participants, "participants", LookMode.Reference);
        }

        private IEnumerable<Pawn> AvailableParticipants()
        {
            return PawnsFinder.AllMaps_FreeColonists.Where(CanParticipate);
        }

        public void Play(Pawn p)
        {
            participants.Add(p);
        }

        public bool CanParticipate(Pawn p)
        {
            return !participants.Contains(p);
        }

        public override string CompInspectStringExtra()
        {
            var available = AvailableParticipants().ToList();
            var count = available.Count();
            if (count == 0)
            {
                return "All colonists have escaped.";
            }
            else if (count <= 3)
            {
                return "Available: " + string.Join(",", available.Select(p => p.Name.ToStringShort));
            }
            else
            {
                return $"Available: {count} colonists";
            }
        }
    }
}
