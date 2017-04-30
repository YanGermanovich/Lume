using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class EventMapper
    {
        public static BllEvent ToBll(this DalEvent ev)
        {
            if (ev == null)
                throw new ArgumentNullException(nameof(ev));
            return new BllEvent()
            {
                Id = ev.Id,
                Source = ev.Source,
                Id_DataType = ev.Id_DataType
            };
        }

        public static DalEvent ToDal(this BllEvent ev)
        {
            if (ev == null)
                throw new ArgumentNullException(nameof(ev));
            return new DalEvent()
            {
                Id = ev.Id,
                Source = ev.Source,
                Id_DataType = ev.Id_DataType
            };
        }
    }
}
