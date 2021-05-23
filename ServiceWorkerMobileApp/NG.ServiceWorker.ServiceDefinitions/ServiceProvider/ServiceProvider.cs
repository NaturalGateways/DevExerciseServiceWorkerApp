using System;
using System.Collections.Generic;

using NG.ServiceWorker.ServiceProviderImpl;

namespace NG.ServiceWorker
{
    public static class ServiceProvider
    {
        /// <summary></summary>
        private static Dictionary<Type, ServiceImplementation> s_implementationsByInterfaceType = new Dictionary<Type, ServiceImplementation>();

        /// <summary>Registers an interface implementaation mapping.</summary>
        public static void RegisterService<InterfaceType, ImplementationType>()
        {
            Type interfaceType = typeof(InterfaceType);
            Type implementationType = typeof(ImplementationType);
            RegisterService(interfaceType, implementationType);
        }

        /// <summary>Registers an interface implementaation mapping.</summary>
        public static void RegisterService(Type interfaceType, Type implementationType)
        {
            if (s_implementationsByInterfaceType.ContainsKey(interfaceType))
            {
                throw new Exception($"Cannot register '{interfaceType.FullName}' more than once.");
            }
            s_implementationsByInterfaceType.Add(interfaceType, new ServiceImplementation
            {
                InterfaceType = interfaceType,
                ImplementationType = implementationType
            });
        }

        /// <summary>Getter for a service.</summary>
        public static ServiceType GetService<ServiceType>()
        {
            Type interfaceType = typeof(ServiceType);
            if (s_implementationsByInterfaceType.ContainsKey(interfaceType) == false)
            {
                throw new Exception($"Service '{interfaceType.FullName}' does not exist.");
            }
            ServiceImplementation serviceImpl = s_implementationsByInterfaceType[interfaceType];
            if (serviceImpl.ImplementationObject == null)
            {
                serviceImpl.ImplementationObject = Activator.CreateInstance(serviceImpl.ImplementationType);
            }
            return (ServiceType)serviceImpl.ImplementationObject;
        }
    }
}
