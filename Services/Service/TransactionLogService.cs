using AutoMapper;
using BusinessObject;
using Core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Dynamic;
using System.Security.Claims;
using ViewModels.Response;

namespace Services.Service
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ILogger<TransactionLogService> _logger;

        public TransactionLogService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService, ILogger<TransactionLogService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<object> GetAllTransactionByYear(int year)
        {
            var result = await _unitOfWork.GetRepository<TransactionLogs>().GetAllAsync(x => !x.IsDeleted
                        && x.CreatedTime.HasValue
                        && x.CreatedTime.Value.Year == year);

            var grouped = result
                .GroupBy(x => x.CreatedTime.Value.Month)
                .Select(monthGroup =>
                {
                    dynamic expando = new ExpandoObject();
                    var dict = (IDictionary<string, object>)expando;

                    dict["Month"] = monthGroup.Key;

                    var campaigns = monthGroup
                        .GroupBy(x => x.Campaign.Title)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Sum(x => x.Amount)
                        );

                    foreach (var campaign in campaigns)
                    {
                        dict[campaign.Key] = campaign.Value;
                    }

                    return expando;
                })
                .OrderBy(g => ((IDictionary<string, object>)g)["Month"])
                .ToList();

            return grouped;
        }

        public async Task<object> GetAllTransactionByYearAndCampaign(int year, string campaignId)
        {
            var result = await _unitOfWork.GetRepository<TransactionLogs>()
                    .GetAllAsync(x => !x.IsDeleted
                                   && x.CreatedTime.HasValue
                                   && x.CreatedTime.Value.Year == year
                                   && x.CampaignId == campaignId);

            var grouped = result
                .GroupBy(x => x.CreatedTime.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .OrderBy(g => g.Month)
                .ToList();

            return grouped;
        }

        public async Task<IEnumerable<TransactionResponse>?> GetAllTransactionLogsAsync()
        {
            try
            {
                var result = await _unitOfWork.GetRepository<TransactionLogs>().GetAllAsync();
                return _mapper.Map<IEnumerable<TransactionResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<TransactionResponse>?> GetAllTransactionLogsByCampaign(string campaginId)
        {
            try
            {
                var result = await _unitOfWork.GetRepository<TransactionLogs>().GetAllAsync(x => x.CampaignId == campaginId && !x.IsDeleted);
                return _mapper.Map<IEnumerable<TransactionResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<BasePaginatedList<TransactionResponse>?> GetAllTransactionWithPaging(int currentPage, int pageSize)
        {
            try
            {
                var items = _unitOfWork.GetRepository<TransactionLogs>().Entities.Where(x => !x.IsDeleted);
                var paging = await _unitOfWork.GetRepository<TransactionLogs>().GetPaggingAsync(items, currentPage, pageSize);
                return _mapper.Map<BasePaginatedList<TransactionResponse>>(paging);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<TransactionResponse>?> GetTransactionLogsOfUser(Guid userId)
        {
            try
            {
                var items = await _unitOfWork.GetRepository<TransactionLogs>().GetAllAsync(x => x.UserId.Equals(userId) && !x.IsDeleted);
                return _mapper.Map<IEnumerable<TransactionResponse>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<BasePaginatedList<TransactionResponse>?> GetTransactionLogsOfUsersPaging(ClaimsPrincipal claim, int currentPage, int pageSize)
        {
            try
            {
                var userId = _tokenService.GetUserIdFromPrincipal(claim);
                var items = _unitOfWork.GetRepository<TransactionLogs>().Entities.Where(x => x.UserId.Equals(Guid.Parse(userId)) && !x.IsDeleted).OrderByDescending(x=>x.CreatedTime);
                var paging = await _unitOfWork.GetRepository<TransactionLogs>().GetPaggingAsync(items, currentPage, pageSize);
                return _mapper.Map<BasePaginatedList<TransactionResponse>>(paging);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
