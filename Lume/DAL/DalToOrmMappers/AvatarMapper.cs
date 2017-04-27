using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using DAL.DTO;

namespace DAL.DalToOrmMappers
{
    public static class AvatarMapper
    {
        public static avatar ToOrm(this DalAvatar avatar)
        {
            if (avatar == null)
                throw new ArgumentNullException(nameof(avatar));
            return new avatar()
            {
                id_Avatars = avatar.Id,
                avatar_src = avatar.Src
            };
        }

        public static DalAvatar ToDal(this avatar avatar)
        {
            if (avatar == null)
                throw new ArgumentNullException(nameof(avatar));
            return new DalAvatar()
            {
                Id = avatar.id_Avatars,
                Src = avatar.avatar_src
            };
        }
    }
}
