namespace praktikaGal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Activity_Event
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDActivity { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDEvent { get; set; }

        public int? EventDay { get; set; }

        public TimeSpan? BeginningTime { get; set; }

        public int? IDModerator { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Event Event { get; set; }

        public virtual User User { get; set; }
    }
}
