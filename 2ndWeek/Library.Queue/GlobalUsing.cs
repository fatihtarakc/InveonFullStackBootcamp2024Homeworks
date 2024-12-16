global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Newtonsoft.Json;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;
global using System.Text;

global using Library.Business.Abstract.Services.Abstract;
global using Library.Core.Constants;
global using Library.Core.Options;
global using Library.Dtos.EmailDtos;
global using Library.Queue.Services.Abstract;
global using Library.Queue.Services.Concrete;