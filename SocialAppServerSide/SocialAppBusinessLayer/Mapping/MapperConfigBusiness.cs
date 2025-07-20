using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Mapping
{
    public class MapperConfigBusiness
    {
        private static readonly IMapper _mapper;

        static MapperConfigBusiness()
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
