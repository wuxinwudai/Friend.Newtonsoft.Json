
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Friend.Newtonsoft.Json
{
    public static class Extension
    {
        public static IServiceCollection AddFriendNewtonsoft(this IServiceCollection services)
        {
            return services;
        }
    }
}