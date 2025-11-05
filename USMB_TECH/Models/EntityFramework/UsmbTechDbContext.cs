using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace USMB_TECH.Models.EntityFramework;

public partial class UsmbTechDbContext : DbContext
{
    public UsmbTechDbContext()
    {
    }

    public UsmbTechDbContext(DbContextOptions<UsmbTechDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=51.83.36.122;port=5432;Database=usmbTechDB; uid=s213;password=dN8QKrYi;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
