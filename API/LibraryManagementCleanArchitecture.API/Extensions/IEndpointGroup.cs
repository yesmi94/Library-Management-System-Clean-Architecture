// <copyright file="IEndpointGroup.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.API.Extensions
{
    public interface IEndpointGroup
    {
        void MapEndpoints(IEndpointRouteBuilder app);
    }
}
