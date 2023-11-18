using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoSoundify.Models.dbModels
{
    public partial class SoundifyContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public SoundifyContext()
        {
        }

        public SoundifyContext(DbContextOptions<SoundifyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anuncio> Anuncios { get; set; } = null!;
        public virtual DbSet<Cancion> Cancions { get; set; } = null!;
        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<Descarga> Descargas { get; set; } = null!;
        public virtual DbSet<Playlist> Playlists { get; set; } = null!;
        public virtual DbSet<PlaylistCancion> PlaylistCancions { get; set; } = null!;
        public virtual DbSet<Reproduccion> Reproduccions { get; set; } = null!;
        

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Anuncio>(entity =>
            {
                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Anuncios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioCreacion");
            });

            modelBuilder.Entity<Cancion>(entity =>
            {
                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Cancions)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Categoria");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Cancions)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CancionUsuario");

                entity.HasMany(d => d.IdUsuarios)
                    .WithMany(p => p.IdCancions)
                    .UsingEntity<Dictionary<string, object>>(
                        "MeGustum",
                        l => l.HasOne<ApplicationUser>().WithMany().HasForeignKey("IdUsuario").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Usuario"),
                        r => r.HasOne<Cancion>().WithMany().HasForeignKey("IdCancion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Cancion"),
                        j =>
                        {
                            j.HasKey("IdCancion", "IdUsuario").HasName("PK_MeGusta_1");

                            j.ToTable("MeGusta");
                        });
            });

            modelBuilder.Entity<Descarga>(entity =>
            {
                entity.HasKey(e => e.IdDescarga)
                    .HasName("PK_MeGusta");

                entity.HasOne(d => d.IdCancionNavigation)
                    .WithMany(p => p.Descargas)
                    .HasForeignKey(d => d.IdCancion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdCancionDescarga");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Descargas)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdUserDescarga");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Playlist_User");
            });

            modelBuilder.Entity<PlaylistCancion>(entity =>
            {
                entity.HasKey(e => new { e.IdPlaylist, e.IdCancion });

                entity.HasOne(d => d.IdCancionNavigation)
                    .WithMany(p => p.PlaylistCancions)
                    .HasForeignKey(d => d.IdCancion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistCancion_Cancion");

                entity.HasOne(d => d.IdPlaylistNavigation)
                    .WithMany(p => p.PlaylistCancions)
                    .HasForeignKey(d => d.IdPlaylist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistCancion_Playlist");
            });

            modelBuilder.Entity<Reproduccion>(entity =>
            {
                entity.HasOne(d => d.IdCancionNavigation)
                    .WithMany(p => p.Reproduccions)
                    .HasForeignKey(d => d.IdCancion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReproduccionCancion");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reproduccions)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReproduccionUsuario");
            });

           

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
