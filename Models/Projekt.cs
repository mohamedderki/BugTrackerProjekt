using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BugProjektV1.Models
{
    public partial class Projekt
    {
        public Projekt()
        {
            Bugs = new HashSet<Bug>();
        }

        public int ProjektId { get; set; }
        public string ProjektName { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDatum { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? EndDatum { get; set; }

        public virtual ICollection<Bug> Bugs { get; set; }
    }
}
