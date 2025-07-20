using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SocialAppDataLayer.Mapping
{
    public static class MapperConfigData
    {
        private static readonly IMapper _mapper;

        static MapperConfigData()
        {
            var config = new MapperConfiguration(cfg =>
            {
             
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            _mapper = config.CreateMapper();
        }

        public static IMapper Mapper => _mapper;
    }
}
