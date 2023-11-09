namespace praktikaGal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            Activity_Event = new HashSet<Activity_Event>();
            Event_EventType_Direction = new HashSet<Event_EventType_Direction>();
            Event_Judges = new HashSet<Event_Judges>();
            Users = new HashSet<User>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventID { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BeginningDate { get; set; }

        public int? DaysQuantity { get; set; }

        public int? IDUser { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCity { get; set; }

        public int? IDEventType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity_Event> Activity_Event { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_EventType_Direction> Event_EventType_Direction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_Judges> Event_Judges { get; set; }

        public virtual EventType EventType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
