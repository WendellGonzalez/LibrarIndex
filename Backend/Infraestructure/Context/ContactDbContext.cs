using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Context
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        public DbSet<Contact> contacto { get; set; }
        public DbSet<Libro> viewlibros { get; set; }
        public DbSet<Autor> viewautores { get; set; }
    }
}