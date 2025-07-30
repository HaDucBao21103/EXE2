using BusinessObject;
using Core.Base;
using Repositories.Interfaces;
using Services.Interfaces;
using ViewModels.Response;

namespace Services.Service
{
    public class CampaignsService : ICampaignsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CampaignsService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        #region Async Methods

        public async Task<IEnumerable<Campaigns>> GetAllAsync()
        {
            return await _unitOfWork.GetRepository<Campaigns>().GetAllAsync(x => !x.IsDeleted);
        }

        public async Task<Campaigns?> GetByIdAsync(string id)
        {
            return await _unitOfWork.GetRepository<Campaigns>().GetOneAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<BasePaginatedList<Campaigns>> GetPaginatedListAsync(int pageNumber, int pageSize)
        {
            var items = await _unitOfWork.GetRepository<Campaigns>().GetAllAsync(x => !x.IsDeleted);
            return new BasePaginatedList<Campaigns>(items.ToList(), items.Count(), pageNumber, pageSize);
        }

        public async Task<IEnumerable<Campaigns>> SearchAsync(string keyword)
        {
            var result = _unitOfWork.GetRepository<Campaigns>()
                .Entities
                .Where(x =>
                    !x.IsDeleted &&
                    (x.Title.Contains(keyword) || string.IsNullOrEmpty(keyword)) ||
                    x.Description.Contains(keyword) || string.IsNullOrEmpty(keyword)
                ).AsEnumerable();
            return await Task.FromResult(result.ToList());
        }

        public async Task<bool> CreateAsync(Campaigns campaign)
        {
            await _unitOfWork.GetRepository<Campaigns>().CreateAsync(campaign);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Campaigns campaign)
        {
            await _unitOfWork.GetRepository<Campaigns>().UpdateAsync(campaign);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await _unitOfWork.GetRepository<Campaigns>().GetByIdAsync(id);
            if (item == null) return false;
            item.IsDeleted = true;
            await _unitOfWork.GetRepository<Campaigns>().UpdateAsync(item);
            var result = await _unitOfWork.SaveAsync();
            return result > 0;
        }

        public async Task<List<CampaignTransactionSummaryResponse>> GetCampaignTransactionSummaryAsync()
        {
            var transactionLogs = await _unitOfWork.GetRepository<TransactionLogs>()
                .GetAllAsync(x => x.CampaignId != null);

            var transactionTypes = await _unitOfWork.GetRepository<TransactionTypes>()
                .GetAllAsync(x => true);

            var campaigns = await _unitOfWork.GetRepository<Campaigns>()
                .GetAllAsync(x => !x.IsDeleted);

            var joined = from log in transactionLogs
                         join type in transactionTypes on log.TransactionTypeId equals type.Id
                         group new { log.Amount, type.Category, log.CampaignId } by log.CampaignId into g
                         join camp in campaigns on g.Key equals camp.Id
                         select new CampaignTransactionSummaryResponse
                         {
                             CampaignId = g.Key!,
                             TotalIn = g.Where(x => x.Category == "IN").Sum(x => x.Amount) + (float)camp.RaisedAmount,
                             TotalOut = g.Where(x => x.Category == "OUT").Sum(x => x.Amount),
                             Remaining = g.Where(x => x.Category == "IN").Sum(x => x.Amount) + (float)camp.RaisedAmount
                                       - g.Where(x => x.Category == "OUT").Sum(x => x.Amount),
                             TargetAmount = camp.TargetAmount.GetValueOrDefault()
                         };

            return joined.ToList();
        }

        public async Task<CampaignTransactionSummaryResponse?> GetCampaignTransactionSummaryByIdAsync(string id)
        {
            var transactionLogs = await _unitOfWork.GetRepository<TransactionLogs>()
                .GetAllAsync(x => x.CampaignId != null);

            var transactionTypes = await _unitOfWork.GetRepository<TransactionTypes>()
                .GetAllAsync(x => true);

            var campaigns = await _unitOfWork.GetRepository<Campaigns>()
                .GetAllAsync(x => !x.IsDeleted);

            var joined = from log in transactionLogs
                         join type in transactionTypes on log.TransactionTypeId equals type.Id
                         group new { log.Amount, type.Category, log.CampaignId } by log.CampaignId into g
                         join camp in campaigns on g.Key equals camp.Id
                         select new CampaignTransactionSummaryResponse
                         {
                             CampaignId = g.Key!,
                             TotalIn = g.Where(x => x.Category == "IN").Sum(x => x.Amount) + (float)camp.RaisedAmount,
                             TotalOut = g.Where(x => x.Category == "OUT").Sum(x => x.Amount),
                             Remaining = g.Where(x => x.Category == "IN").Sum(x => x.Amount) + (float)camp.RaisedAmount
                                       - g.Where(x => x.Category == "OUT").Sum(x => x.Amount),
                             TargetAmount = camp.TargetAmount.GetValueOrDefault()
                         };
            return joined.ToList().Where(x => x.CampaignId.Equals(id)).FirstOrDefault();
        }
        #endregion

        #region Non-Async Methods

        public IEnumerable<Campaigns> GetAll()
        {
            return _unitOfWork.GetRepository<Campaigns>().GetAll(x => !x.IsDeleted);
        }

        public Campaigns? GetById(string id)
        {
            return _unitOfWork.GetRepository<Campaigns>().GetOne(x => x.Id == id && !x.IsDeleted);
        }

        public BasePaginatedList<Campaigns> GetPaginatedList(int pageNumber, int pageSize)
        {
            var items = _unitOfWork.GetRepository<Campaigns>().GetAll(x => !x.IsDeleted);
            return new BasePaginatedList<Campaigns>(items.ToList(), items.Count(), pageNumber, pageSize);
        }

        public IEnumerable<Campaigns> Search(string keyword)
        {
            var result = _unitOfWork.GetRepository<Campaigns>()
                .Entities
                .Where(x =>
                    !x.IsDeleted &&
                    (x.Title.Contains(keyword) || string.IsNullOrEmpty(keyword)) ||
                    x.Description.Contains(keyword) || string.IsNullOrEmpty(keyword)
                ).AsEnumerable();
            return result.ToList();
        }

        public bool Create(Campaigns campaign)
        {
            _unitOfWork.GetRepository<Campaigns>().Create(campaign);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Update(Campaigns campaign)
        {
            _unitOfWork.GetRepository<Campaigns>().Update(campaign);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        public bool Delete(string id)
        {
            var item = _unitOfWork.GetRepository<Campaigns>().GetById(id);
            if (item == null) return false;
            item.IsDeleted = true;
            _unitOfWork.GetRepository<Campaigns>().Update(item);
            var result = _unitOfWork.Save();
            return result > 0;
        }

        #endregion
    }
}
