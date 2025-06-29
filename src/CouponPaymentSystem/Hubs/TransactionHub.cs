using Abp.Dependency;
using Microsoft.AspNet.SignalR;

namespace CouponPaymentSystem.Hubs;

public class TransactionHub : Hub, ITransientDependency { }
