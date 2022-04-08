using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Domain.Shared
{
    public static class ProductTypeEnumExtension
    {
        public static string GetString(this ProductTypeEnum pt)
        {
            switch (pt)
            {
                case ProductTypeEnum.Digitalcameras:
                    return "Digital cameras";
                case ProductTypeEnum.Smartphones:
                    return "Smartphones";
                case ProductTypeEnum.Laptops:
                    return "Laptops";
                default:
                    return "None";
            }
        }
    }
}
