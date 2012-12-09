using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Dependencies;
using Ninject;

namespace XboxGamesApi.App_Start
{
  public class NinjectResolver : NinjectScope, IDependencyResolver
  {
    private IKernel _kernel;
    public NinjectResolver(IKernel kernel)
      : base(kernel)
    {
      _kernel = kernel;
    }
    public IDependencyScope BeginScope()
    {
      return new NinjectScope(_kernel.BeginBlock());
    }
  }
}
