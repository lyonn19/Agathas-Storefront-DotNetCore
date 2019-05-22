using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using StructureMap;
public static class ObjectFactoryHelper {
  private static readonly Lazy<Container> _containerBuilder =
        new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

  public static IContainer Container {
    get { return _containerBuilder.Value; }
  }

  private static Container defaultContainer() {
    return new Container(x =>   {
      // todo:  5/21/2019 - w.sams. what goes here?
      //x.AddRegistry(new YourRegistry());
    });
  }
}