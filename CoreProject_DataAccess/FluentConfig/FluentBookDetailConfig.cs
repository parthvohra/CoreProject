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
    public class FluentBookDetailConfig: IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> builder) 
        {
            //BookDetail
            builder.HasKey(x => x.BookDetail_Id);
            builder.Property(x => x.NumberOfChapters).IsRequired();
        }
    }
}
