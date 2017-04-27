using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class EventMapper
    {
        public static @event ToOrm(this DalEvent ev)
        {
            if (ev == null)
                throw new ArgumentNullException(nameof(ev));
            return new @event()
            {
                id_event = ev.Id,
                Source = ev.Source,
                Type_id_Type = ev.Id_DataType
            };
        }

        public static DalEvent ToDal(this @event ev)
        {
            if (ev == null)
                throw new ArgumentNullException(nameof(ev));
            return new DalEvent()
            {
                Id = ev.id_event,
                Source = ev.Source,
                Id_DataType = ev.Type_id_Type
            };
        }
    }
}
