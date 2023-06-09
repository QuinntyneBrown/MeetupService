// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MeetupService.Core;
using MeetupService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {

        services.AddScoped<IMeetupServiceDbContext, MeetupServiceDbContext>();
        services.AddDbContextPool<MeetupServiceDbContext>(options =>
        {
            options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("MeetupService.Infrastructure"))
            .EnableThreadSafetyChecks(false);
        });
    }
}
