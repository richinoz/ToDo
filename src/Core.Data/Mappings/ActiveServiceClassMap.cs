//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CampAustralia.Domain.Entities.dbRexa.Core;
//using FluentNHibernate.Mapping;

//namespace Core.Data.Mappings.dbRexa.Core {
//    public class ActiveServiceClassMap : SchemaClassMap<ActiveService> {
//        public ActiveServiceClassMap()
//        {
//            Schema("Core");
//            Table("ActiveServiceList");
//            Id(x => x.ServiceId).GeneratedBy.Identity();
//            Map(x => x.Name);
//            Map(x => x.OshcMobile);
//            Map(x => x.OshcPhone);
//            Map(x => x.Street).Column("StreetLine1");
//            Map(x => x.Suburb).Column("StreetSuburb");
//            Map(x => x.State).Column("StreetState");
//            Map(x => x.Postcode).Column("StreetPCode");
//            Map(x => x.RealName);
//            Map(x => x.ShortName);
//        }
//    }
//}
