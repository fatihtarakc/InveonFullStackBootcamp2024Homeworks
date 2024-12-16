global using Hangfire;
global using Hangfire.Annotations;
global using Hangfire.Dashboard;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using System.Security.Claims;

global using Library.BackgroundJobs.Services.Abstract;
global using Library.BackgroundJobs.Services.Concrete;
global using Library.BackgroundJobs.Schedules;
global using Library.BackgroundJobs.Managers.FireAndForgetJobs;
global using Library.Core.Enums;
global using Library.Core.Options;
global using Library.Queue.Services.Abstract;