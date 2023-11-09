namespace praktikaGal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Activity_Event = new HashSet<Activity_Event>();
            Event_Judges = new HashSet<Event_Judges>();
            Events = new HashSet<Event>();
            Coutries = new HashSet<Coutry>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Photo { get; set; }

        public int? Gender { get; set; }

        public int? Role { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity_Event> Activity_Event { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event_Judges> Event_Judges { get; set; }

        public virtual Gender Gender1 { get; set; }

        public virtual Role Role1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Coutry> Coutries { get; set; }
    }
}
