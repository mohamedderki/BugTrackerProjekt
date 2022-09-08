using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BugProjektV1.Models
{
    public partial class Bug
    {
        public int BugId { get; set; }
        public string Titel { get; set; }

        
        public string Beschreibung { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ErfassungDatum { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? BehebungsDatum { get; set; }
        public int? TesterId { get; set; }
        public int? EntwicklerId { get; set; }
        public int? ProjektId { get; set; }

        public virtual Mitarbeiter Entwickler { get; set; }
        public virtual Projekt Projekt { get; set; }
        public virtual Mitarbeiter Tester { get; set; }
    }
}
