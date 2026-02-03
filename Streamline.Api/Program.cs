using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using System.Reflection;
using Streamline.Infrastructure.Persistence.SqlServer.DbContexts;
using Streamline.Infrastructure.Persistence.SqlServer.Repositories;
using Streamline.Application.Commands;
using Streamline.API.Routes;
using Streamline.Application.Interfaces.Repositories;
using Streamline.API.Dtos;
using Streamline.API.Mappings;
using Streamline.API.Factory;

var app = AppFactory.CreateApp(args);

app.MapGet("/", () => "API rodando!");

var api = app.MapGroup("/api/v1");

api.MapOrderRoutes();
api.MapProductRoutes();
api.MapCustomerRoutes();

app.Run();
