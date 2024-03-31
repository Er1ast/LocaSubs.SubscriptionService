using SubscriptionService.Models;
namespace SubscriptionService.DataAccess.Contracts
{
    public interface ISubscriptionRepository
    {
        Task<long> AddAsync(Subscription subscription);
        Task RemoveAsync(long subscriptionId, string userLogin);
        Task<IList<Subscription>> GetUserSubscriptionsAsync(string userLogin);
    }
}
