using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

using Autofac;
using Autofac.Features.ResolveAnything;

using NHibernate;
//using FluentNHibernate.Automapping;
//using FluentNHibernate.Cfg;
//using FluentNHibernate.Cfg.Db;

using Agathas.Storefront.Repository.NHibernate;
using NHRepos = Agathas.Storefront.Repository.NHibernate.Repositories;
using Agathas.Storefront.Controllers.ActionArguments;
using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Domain.Events;
using Agathas.Storefront.Infrastructure.Authentication;
using Agathas.Storefront.Infrastructure.CookieStorage;
using Agathas.Storefront.Infrastructure.Logging;
using Agathas.Storefront.Infrastructure.Payments;
using Agathas.Storefront.Infrastructure.Configuration;
using Agathas.Storefront.Model.Basket;
using Agathas.Storefront.Model.Categories;
using Agathas.Storefront.Model.Customers;
using Agathas.Storefront.Model.Orders;
using Agathas.Storefront.Model.Orders.Events;
using Agathas.Storefront.Model.Products;
using Agathas.Storefront.Model.Shipping;
using Agathas.Storefront.Services.DomainEventHandlers;
using Agathas.Storefront.Services.Implementations;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Cache;
using Agathas.Storefront.Infrastructure.Email;

using log4net;

namespace Agathas.Storefront.API {
  public class AutofacModule : Module {
    protected override void Load(ContainerBuilder builder) {
      // Repositories
      builder.RegisterType<NHRepos.OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
      builder.RegisterType<NHRepos.CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
      builder.RegisterType<NHRepos.BasketRepository>().As<IBasketRepository>().InstancePerLifetimeScope();
      builder.RegisterType<NHRepos.DeliveryOptionRepository>().As<IDeliveryOptionRepository>().InstancePerLifetimeScope();
      builder.RegisterType<NHRepos.CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
      builder.RegisterType<NHRepos.ProductTitleRepository>().As<IProductTitleRepository>().InstancePerLifetimeScope();
      builder.RegisterType<NHRepos.ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
      builder.RegisterType<NHUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

      // Order Service
      builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();

      // Payment
      builder.RegisterType<PayPalPaymentService>().As<IPaymentService>().InstancePerLifetimeScope();

      // Handlers for Domain Events
      //builder.RegisterType<DomainEvents.DomainEventHandlerFactory>().As<IDomainEventHandlerFactory>();
      //builder.RegisterType<DomainEvents.DomainEventHandler>().As<IDomainEventHandler>();


      // Product Catalogue & Category Service with Caching Layer Registration
      builder.RegisterType<ProductCatalogService>().As<IProductCatalogService>().InstancePerLifetimeScope();
      // Uncomment the line below to use the product service caching layer
      //builder.RegisterType<CachedProductCatalogService>().As<IProductCatalogService>().InstancePerLifetimeScope();

      builder.RegisterType<BasketService>().As<IBasketService>().InstancePerLifetimeScope();
      builder.RegisterType<CookieStorageService>().As<ICookieStorageService>().InstancePerLifetimeScope();

      // Application Settings                 
      builder.RegisterType<ApiConfigApplicationSettings>().As<IApplicationSettings>().InstancePerLifetimeScope();

      // Logger
      builder.RegisterType<Log4NetAdapter>().As<Agathas.Storefront.Infrastructure.Logging.ILogger>().InstancePerLifetimeScope();

      // Email Service                 
      builder.RegisterType<TextLoggingEmailService>().As<IEmailService>().InstancePerLifetimeScope();

      builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope().InstancePerLifetimeScope();

      // Authentication
      builder.RegisterType<JanrainAuthenticationService>().As<IExternalAuthenticationService>().InstancePerLifetimeScope();
      builder.RegisterType<AspFormsAuthentication>().As<IFormsAuthentication>().InstancePerLifetimeScope();
      
      // Controller Helpers
      builder.RegisterType<HttpRequestActionArguments>().As<IActionArguments>().InstancePerLifetimeScope();
    }
  }
}