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
    public class FluentPublisherConfig: IEntityTypeConfiguration<Fluent_Publisher>
    {
        public void Configure(EntityTypeBuilder<Fluent_Publisher> builder)
        {
            //Publisher
            builder.HasKey(x => x.Publisher_Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Location).IsRequired();
        }
    }
}
