using DAL.DTO;
using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BllToDalMappers
{
    public static class AvatarMapper
    {
        public static BllAvatar ToBll(this DalAvatar avatar)
        {
            if (avatar == null)
                throw new ArgumentNullException(nameof(avatar));
            return new BllAvatar()
            {
                Id = avatar.Id,
                Src = avatar.Src
            };
        }

        public static DalAvatar ToDal(this BllAvatar avatar)
        {
            if (avatar == null)
                throw new ArgumentNullException(nameof(avatar));
            return new DalAvatar()
            {
                Id = avatar.Id,
                Src = avatar.Src
            };
        }
    }
}
