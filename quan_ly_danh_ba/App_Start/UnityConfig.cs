using AutoMapper;
using Configuration;
using quan_ly_danh_ba.Respository.Implements;
using quan_ly_danh_ba.Respository.Interfaces;
using quan_ly_danh_ba.Services.Implements;
using quan_ly_danh_ba.Services.Interfaces;
using System;

using Unity;
using Unity.Injection;

namespace quan_ly_danh_ba
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterFactory<IMapper>(c =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>(); // Thay MappingProfile bằng lớp profile của bạn
                });

                return config.CreateMapper();
            });
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            //Service
            #region
            container.RegisterType<IContactService, ContactService>();
            container.RegisterType<IGroupContactService, GroupContactService>();
            container.RegisterType<IUserService, UserService>();
            #endregion

            //Respository
            #region
            container.RegisterType<IContactRespository, ContactRespository>();
            container.RegisterType<IGroupContactRespository, GroupContactRespository>();
            container.RegisterType<IConnectContact_GroupContactRespository, ConnectContact_GroupContactRespository>();
            container.RegisterType<IUserRespository, UserRespository>();

            #endregion
        }
    }
}