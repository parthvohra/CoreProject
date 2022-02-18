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
    public class FluentAuthorConfig: IEntityTypeConfiguration<Fluent_Author>
    {
        public void Configure(EntityTypeBuilder<Fluent_Author> builder)
        {
            //Author
            builder.HasKey(x => x.Author_Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.Location).IsRequired();
            builder.Ignore(x => x.FullName);
        }
    }
}
