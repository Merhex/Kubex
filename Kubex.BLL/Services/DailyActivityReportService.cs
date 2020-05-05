using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services
{
    public class DailyActivityReportService : IDailyActivityReportService
    {
        private readonly IMapper _mapper;
        private readonly IDailyActivityReportRepository _darRepository;

        public DailyActivityReportService(IMapper mapper,
            IDailyActivityReportRepository darRepository)
        {
            _mapper = mapper;
            _darRepository = darRepository;
        }

        public async Task<DailyActivityReportDTO> CreateDAR() 
        {
            var report = new DailyActivityReport { Date = DateTime.Now, Entries = new List<Entry>() };

            _darRepository.Add(report);

            if (await _darRepository.SaveAll()) 
            {
                var reportToReturn = _mapper.Map<DailyActivityReportDTO>(report);

                return reportToReturn;
            }

            throw new ApplicationException("Something went wrong creating a new report.");
        }
    }
}