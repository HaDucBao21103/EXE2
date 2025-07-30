using BusinessObject;
using Core.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ViewModels.Response;

namespace EXE201_SU25.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionLogsController : ControllerBase
    {
        private readonly ITransactionLogService _transactionLogService;
        public TransactionLogsController(ITransactionLogService transactionLogService)
        {
            _transactionLogService = transactionLogService;
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<TransactionResponse>>> Get()
        {
            var result = await _transactionLogService.GetAllTransactionLogsAsync();
            return result != null ?
                BaseResponse<IEnumerable<TransactionResponse>>.OkResponse(result) :
                BaseResponse<IEnumerable<TransactionResponse>>.NotFoundResponse("Không có log nào");
        }

        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<BaseResponse<BasePaginatedList<TransactionResponse>>> GetPaging(int currentPage = 1, int pageSize = 5)
        {
            var result = await _transactionLogService.GetAllTransactionWithPaging(currentPage, pageSize);
            return result != null ?
                BaseResponse<BasePaginatedList<TransactionResponse>>.OkResponse(result) :
                BaseResponse<BasePaginatedList<TransactionResponse>>.NotFoundResponse("Không có log nào");
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Users")]
        public async Task<BaseResponse<List<TransactionResponse>>> GetTransactionLogOfUser(Guid userId)
        {
            var result = await _transactionLogService.GetTransactionLogsOfUser(userId);
            return result != null ?
                BaseResponse<List<TransactionResponse>>.OkResponse(result.ToList()) :
                BaseResponse<List<TransactionResponse>>.NotFoundResponse("Không có log nào");
        }

        [HttpGet("report/{year}")]
        public async Task<BaseResponse<object>> GetAllTransactionByYear(int year)
        {
            var result = await _transactionLogService.GetAllTransactionByYear(year);
            return result != null ?
                BaseResponse<object>.OkResponse(result)
                : BaseResponse<object>.NotFoundResponse("Không có log nào");
        }

        [HttpGet("report/{year}/{campaignId}")]
        public async Task<BaseResponse<object>> GetAllTransactionByYear(int year,string campaignId)
        {
            var result = await _transactionLogService.GetAllTransactionByYearAndCampaign(year,campaignId);
            return result != null ?
                BaseResponse<object>.OkResponse(result)
                : BaseResponse<object>.NotFoundResponse("Không có log nào");
        }

        [HttpGet("user/{currentPage}/{pageSize}")]
        [Authorize(Roles = "User")]
        public async Task<BaseResponse<BasePaginatedList<TransactionResponse>>> GetTransactionLogOfUserWithPagig(int currentPage = 1, int pageSize = 10)
        {
            var result = await _transactionLogService.GetTransactionLogsOfUsersPaging(User, currentPage, pageSize);
            return result != null ?
                BaseResponse<BasePaginatedList<TransactionResponse>>.OkResponse(result) :
                BaseResponse<BasePaginatedList<TransactionResponse>>.NotFoundResponse("không có log nào");
        }
    }
}
