//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;
//using PolicyWatcher.Domain.Models;

//namespace PolicyWatcher.Infrastructure.Data.Configuration
//{
//    public class TierConfiguration : IEntityTypeConfiguration<Tier>
//    {
//        public void Configure(EntityTypeBuilder<Tier> builder)
//        {
//            builder.HasData
//            (
//                new Tier
//                {
//                    TierId = 1,
//                    Level = "Tier1",
//                    PerTransactionLimit = 20000,
//                    DailyRecieveLimit = 200000,
//                    DailySendLimit = 50000,
//                    MaximumBalance = 500000,
//                },
//                new Tier
//                {
//                    TierId = 2,
//                    Level = "Tier2",
//                    PerTransactionLimit = 1000000,
//                    DailyRecieveLimit = null,
//                    DailySendLimit = 5000000,
//                    MaximumBalance = null,
//                },
//                new Tier
//                {
//                    TierId = 3,
//                    Level = "Tier3",
//                    PerTransactionLimit = null,
//                    DailyRecieveLimit = null,
//                    DailySendLimit = null,
//                    MaximumBalance = null,
//                });
//        }
//    }
//}
