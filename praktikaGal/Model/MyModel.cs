using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace praktikaGal
{
    public partial class MyModel : DbContext
    {
        public MyModel()
            : base("name=MyModel")
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Activity_Event> Activity_Event { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Coutry> Coutries { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Event_EventType_Direction> Event_EventType_Direction { get; set; }
        public virtual DbSet<Event_Judges> Event_Judges { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Users_Direction> Users_Direction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasMany(e => e.Activity_Event)
                .WithRequired(e => e.Activity)
                .HasForeignKey(e => e.IDActivity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.Event_Judges)
                .WithRequired(e => e.Activity)
                .HasForeignKey(e => e.IDActivity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.City)
                .HasForeignKey(e => e.IDCity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coutry>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Coutries)
                .Map(m => m.ToTable("UserCountry").MapLeftKey("IDCountry").MapRightKey("IDUser"));

            modelBuilder.Entity<Direction>()
                .HasMany(e => e.Event_EventType_Direction)
                .WithRequired(e => e.Direction)
                .HasForeignKey(e => e.IDDirection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Direction>()
                .HasMany(e => e.Users_Direction)
                .WithRequired(e => e.Direction)
                .HasForeignKey(e => e.IDDirection)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Activity_Event)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.IDEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Event_EventType_Direction)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.IDEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Event_Judges)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.IDEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Events)
                .Map(m => m.ToTable("User_Event").MapLeftKey("IDEvent").MapRightKey("IDUser"));

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.Event_EventType_Direction)
                .WithRequired(e => e.EventType)
                .HasForeignKey(e => e.IDEventType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.Events)
                .WithOptional(e => e.EventType)
                .HasForeignKey(e => e.IDEventType);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Gender1)
                .HasForeignKey(e => e.Gender);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Role1)
                .HasForeignKey(e => e.Role);

            modelBuilder.Entity<User>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Activity_Event)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IDModerator);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Event_Judges)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IDJudge)
                .WillCascadeOnDelete(false);
        }
    }
}
