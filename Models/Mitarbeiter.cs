using System;
using System.Collections.Generic;

#nullable disable

namespace BugProjektV1.Models
{
    public partial class Mitarbeiter
    {
        public Mitarbeiter()
        {
            BugEntwicklers = new HashSet<Bug>();
            BugTesters = new HashSet<Bug>();
        }

        public int MitarbeiterId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Bereich { get; set; }

        public virtual ICollection<Bug> BugEntwicklers { get; set; }
        public virtual ICollection<Bug> BugTesters { get; set; }
    }
}
