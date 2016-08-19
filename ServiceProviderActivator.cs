using System;
using Hangfire;

namespace concurrent_queues
{
  public class ServiceProviderActivator : JobActivator
  {
    private readonly IServiceProvider _serviceProvider;

    public ServiceProviderActivator(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public override object ActivateJob(Type type)
    {
      return _serviceProvider.GetService(type);
    }
  }
}
