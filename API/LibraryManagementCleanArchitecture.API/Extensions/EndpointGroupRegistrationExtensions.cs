﻿using System.Reflection;

namespace LibraryManagementCleanArchitecture.API.Extensions
{
    public static class EndpointGroupRegistrationExtensions
    {
        public static void RegisterAllEndpointGroups(this IEndpointRouteBuilder app)
        {
            var endpointGroupTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IEndpointGroup).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in endpointGroupTypes)
            {
                if (Activator.CreateInstance(type) is IEndpointGroup group)
                {
                    group.MapEndpoints(app);
                }
            }
        }
    } 
}
