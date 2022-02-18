using CoreProject_Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject_DataAccess.FluentConfig
{
    public class FluentBookConfig: IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> builder)
        {
            //Book
            builder.HasKey(x => x.Book_Id);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.ISBN).IsRequired().HasMaxLength(15);
        }
    }
}
