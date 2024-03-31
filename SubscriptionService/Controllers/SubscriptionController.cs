using Microsoft.AspNetCore.Mvc;
using SubscriptionService.DataAccess.Contracts;
using SubscriptionService.Models;
using System.ComponentModel.DataAnnotations;

namespace SubscriptionService.Controllers;

public class SubscriptionController : Controller
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionController(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    [HttpPost("subscribe")]
    public async Task<IActionResult> SubscribeAsync(
        [FromBody][Required] SubscribeRequest request,
        [FromHeader][Required] string userLogin)
    {
        var subscription = request.ToSubscription(userLogin);
        await _subscriptionRepository.AddAsync(subscription);
        return Ok(subscription.Id);
    }

    [HttpDelete("unsubscribe")]
    public async Task<IActionResult> UnsubscribeAsync(
        [FromBody][Required] long subscriptionId,
        [FromHeader][Required] string userLogin)
    {
        try
        {
            await _subscriptionRepository.RemoveAsync(subscriptionId, userLogin);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("subscriptions")]
    public async Task<IActionResult> GetUserSubscruptions(
        [FromHeader][Required] string userLogin)
    {
        var subscriptions = await _subscriptionRepository.GetUserSubscriptionsAsync(userLogin);
        return Ok(subscriptions);
    }
}
