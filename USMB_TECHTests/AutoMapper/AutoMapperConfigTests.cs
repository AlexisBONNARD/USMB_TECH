using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USMB_TECH.Mapper;

namespace USMB_TECHTests.AutoMapper
{
    [TestClass]
    public abstract class AutoMapperConfigTests
    {
        protected readonly IMapper _mapper;
        protected readonly MapperConfiguration _config;

        protected AutoMapperConfigTests() 
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperContact>();
                cfg.AddProfile<MapperPrestation>();
            });
            _config.AssertConfigurationIsValid();
            _mapper = _config.CreateMapper();
        }
        [TestMethod]
        public void ConfigurationAutoMapper_IsValid()
        {
            Assert.IsNotNull(_mapper);
        }
    }
}
