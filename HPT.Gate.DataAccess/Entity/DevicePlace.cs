using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPT.Gate.DataAccess.Entity.Entity
{
    public class DevicePlace
    {
        public int PlaceId { get; set; }

        public string PlaceName { get; set; }

        public int ParPlaceId { get; set; }

        public int ImageIndex { get; set; }
    }
}
