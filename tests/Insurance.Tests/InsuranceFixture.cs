using AutoMapper;
using Insurance.Api.Profiles;
using Insurance.Api.Services;
using Insurance.Api.Services.Interfaces;
using Insurance.Domain.Interfaces;
using Insurance.Infrastructure.Data;
using Insurance.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
namespace Insurance.Tests
{
    public class InsuranceFixture : IDisposable
    {
        private readonly IHost _host;
        private readonly IConfiguration _configuration;
        public  IProductService _productService { get; private set; }
        public IInsuranceService _insuranceService { get; private set; }
        public IMapper _mapper { get; private set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbApiContext _dbApiContext;
        private readonly ISurchargeRepository _surchargeRepo;
        public InsuranceFixture()
        {
            _configuration = new ConfigurationBuilder()
                       .AddJsonFile("appsettingstest.json")
                       .Build();
            var options = new DbContextOptionsBuilder<DbApiContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var configMapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());

            _mapper = configMapper.CreateMapper();
            _dbApiContext = new DbApiContext(options);
            _surchargeRepo = new SurchargeRepository(_dbApiContext);
            _unitOfWork = new UnitOfWork(_dbApiContext, _surchargeRepo);
            _productService = new ProductService(_configuration);
            _insuranceService = new InsuranceService(_unitOfWork);

            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls("http://localhost:5002")
                              .UseStartup<InsuranceStartup>()
                    )
                   .Build();

            _host.Start();
        }
        public void Dispose()
        {
            _host.Dispose();   
        }     
    }
}
