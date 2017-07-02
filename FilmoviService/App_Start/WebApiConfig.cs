using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using FilmoviService.Models;
using FilmoviService.Models.DTOs;
using FilmoviService.Resolver;
using FilmoviService.Repository;
using Microsoft.Practices.Unity;
using FilmoviService.Repository.Interfaces;

namespace FilmoviService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // AutoMapper
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Film, FilmDTO>(); // automatski će mapirati Author.Name u AuthorName
                //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)); // ako želimo eksplicitno zadati mapranje
                /*cfg.CreateMap<Book, BookDetailDTO>(); // automatski će mapirati Author.Name u AuthorName
                //.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name)); // ako želimo eksplicitno zadati mapiranje*/
                cfg.CreateMap<FilmDTO, Film>()/*.ForMember(dest => dest.Reziser.Ime, opt => opt.MapFrom(src => src.ReziserIme))*/;
            });


            // Unity
            var container = new UnityContainer();
            container.RegisterType<IFilmRepository, FilmRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container); 
        }
    }
}
