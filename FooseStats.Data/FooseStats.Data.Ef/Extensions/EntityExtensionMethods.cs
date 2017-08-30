using System;
using System.Collections.Generic;
using System.Text;
using FooseStats.Data.FooseStats.Data.Ef.Entities;

namespace FooseStats.Data.FooseStats.Data.Ef.Extensions
{
    public static class EntityExtensionMethods
    {
        public static void CopyProperties(this Player source, Player copy)
        {
            source.FirstName = copy.FirstName;
            source.LastName = copy.LastName;
            source.NickName = copy.NickName;
            source.UpdateDate = DateTime.Now;
        }
    }
}
